using Cysharp.Web.Serializers;
using System.Reflection;

namespace Cysharp.Web.Providers;

public sealed class AttributeWebSerializerProvider : IWebSerializerProvider
{
    public static IWebSerializerProvider Instance { get; } = new AttributeWebSerializerProvider();

    AttributeWebSerializerProvider()
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
            var attr = type.GetCustomAttribute<WebSerializerAttribute>();
            if (attr != null)
            {
                attr.Validate(type);
                return (IWebSerializer?)Activator.CreateInstance(attr.Type);
            }

            return null;
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