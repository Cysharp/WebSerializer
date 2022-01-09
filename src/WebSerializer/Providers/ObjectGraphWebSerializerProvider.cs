using Cysharp.Web.Serializers;

namespace Cysharp.Web.Providers;

public sealed class ObjectGraphWebSerializerProvider : IWebSerializerProvider
{
    public static IWebSerializerProvider Instance { get; } = new ObjectGraphWebSerializerProvider();

    ObjectGraphWebSerializerProvider()
    {
    }

    public IWebSerializer<T>? GetSerializer<T>()
    {
        return Cache<T>.Serializer;
    }

    static IWebSerializer? CreateSerializer(Type type)
    {
        try
        {
            return (IWebSerializer?)Activator.CreateInstance(typeof(CompiledObjectGraphWebSerializer<>).MakeGenericType(type));
        }
        catch (Exception ex)
        {
            return ErrorSerializer.Create(type, ex);
        }
    }

    static class Cache<T>
    {
        public static readonly IWebSerializer<T>? Serializer = (IWebSerializer<T>?)CreateSerializer(typeof(T));
    }
}