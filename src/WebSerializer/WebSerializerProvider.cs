using Cysharp.Web.Providers;
using System.Collections.Concurrent;

namespace Cysharp.Web;

public interface IWebSerializerProvider
{
    IWebSerializer<T>? GetSerializer<T>();
}

public static class WebSerializerProvider
{
    public static IWebSerializerProvider Default { get; } = new DefaultWebSerializerProvider();

    public static IWebSerializerProvider Create(params IWebSerializerProvider[] providers)
    {
        return new CompositeSerializerProvider(providers);
    }

    public static IWebSerializerProvider Create(IWebSerializer[] serializers, IWebSerializerProvider[] providers)
    {
        var adhocProvider = new AdhocWebSerializerProvider(serializers);
        return new CompositeSerializerProvider(providers.Prepend(adhocProvider).ToArray());
    }
}

internal class DefaultWebSerializerProvider : IWebSerializerProvider
{
    static readonly IWebSerializerProvider[] providers = new[]
    {
        PrimitiveWebSerializerProvider.Instance,
        BuiltinWebSerializerProvider.Instance,
        AttributeWebSerializerProvider.Instance,
        GenericsWebSerializerProvider.Instance,
        CollectionWebSerializerProvider.Instance,
        ObjectFallbackWebSerializerProvider.Instance,
        ObjectGraphWebSerializerProvider.Instance
    };

    public IWebSerializer<T>? GetSerializer<T>()
    {
        return Cache<T>.Serializer;
    }

    static class Cache<T>
    {
        public static readonly IWebSerializer<T>? Serializer;

        static Cache()
        {
            foreach (var provider in providers)
            {
                var serializer = provider.GetSerializer<T>();
                if (serializer != null)
                {
                    Serializer = serializer;
                    return;
                }
            }
        }
    }
}

internal class CompositeSerializerProvider : IWebSerializerProvider
{
    readonly IWebSerializerProvider[] providers;
    readonly ConcurrentDictionary<Type, IWebSerializer?> cache;

    public CompositeSerializerProvider(IWebSerializerProvider[] providers)
    {
        this.providers = providers;
        this.cache = new ConcurrentDictionary<Type, IWebSerializer?>();
    }

    public IWebSerializer<T>? GetSerializer<T>()
    {
        if (!cache.TryGetValue(typeof(T), out var serializer))
        {
            serializer = CreateSerializer<T>();
            if (!cache.TryAdd(typeof(T), serializer))
            {
                serializer = cache[typeof(T)];
            }
        }

        return (IWebSerializer<T>?)serializer;
    }

    IWebSerializer? CreateSerializer<T>()
    {
        foreach (var provider in providers)
        {
            var serializer = provider.GetSerializer<T>();
            if (serializer != null)
            {
                return serializer;
            }
        }

        return null;
    }
}