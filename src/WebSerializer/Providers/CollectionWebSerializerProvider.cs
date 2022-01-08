using Cysharp.Web.Serializers;

namespace Cysharp.Web.Providers;

public sealed class CollectionWebSerializerProvider : IWebSerializerProvider
{
    public static IWebSerializerProvider Instance { get; } = new CollectionWebSerializerProvider();

    CollectionWebSerializerProvider()
    {

    }

    public IWebSerializer<T>? GetSerializer<T>()
    {
        return Cache<T>.Serializer;
    }

    static IWebSerializer? CreateSerializer(Type type)
    {
        if (type.IsGenericType || type.IsArray)
        {
            // Generic Dictionary
            var dictionaryDef = type.GetImplementedGenericType(typeof(IDictionary<,>));
            if (dictionaryDef != null)
            {
                var keyType = dictionaryDef.GenericTypeArguments[0];
                var valueType = dictionaryDef.GenericTypeArguments[1];
                return CreateInstance(typeof(DictionaryWebSerializer<,,>), new[] { type, keyType, valueType });
            }

            // Generic Collections
            var enumerableDef = type.GetImplementedGenericType(typeof(IEnumerable<>));
            if (enumerableDef != null)
            {
                var elementType = enumerableDef.GenericTypeArguments[0];
                return CreateInstance(typeof(EnumerableWebSerializer<,>), new[] { type, elementType });
            }
        }

        return null;
    }

    static IWebSerializer? CreateInstance(Type genericType, Type[] genericTypeArguments, params object[] arguments)
    {
        return (IWebSerializer?)Activator.CreateInstance(genericType.MakeGenericType(genericTypeArguments), arguments);
    }

    static class Cache<T>
    {
        public static readonly IWebSerializer<T>? Serializer = (IWebSerializer<T>?)CreateSerializer(typeof(T));
    }
}
