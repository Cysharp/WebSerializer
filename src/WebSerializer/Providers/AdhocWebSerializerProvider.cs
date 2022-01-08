using System.Collections.Concurrent;

namespace Cysharp.Web.Providers;

public sealed class AdhocWebSerializerProvider : IWebSerializerProvider
{
    readonly IWebSerializer[] serializers;
    readonly ConcurrentDictionary<Type, IWebSerializer?> cache;
    readonly Func<Type, IWebSerializer?> factory;

    public AdhocWebSerializerProvider(IWebSerializer[] serializers)
    {
        this.serializers = serializers;
        this.cache = new ConcurrentDictionary<Type, IWebSerializer?>();
        this.factory = CreateSerializer;
    }

    public IWebSerializer<T>? GetSerializer<T>()
    {
        return (IWebSerializer<T>?)cache.GetOrAdd(typeof(T), factory);
    }

    IWebSerializer? CreateSerializer(Type type)
    {
        foreach (var serializer in serializers)
        {
            var webSerializerType = serializer.GetType().GetImplementedGenericType(typeof(IWebSerializer<>));
            if (webSerializerType != null)
            {
                if (webSerializerType.GenericTypeArguments[0] == type)
                {
                    return serializer;
                }
            }
        }

        return null;
    }
}

