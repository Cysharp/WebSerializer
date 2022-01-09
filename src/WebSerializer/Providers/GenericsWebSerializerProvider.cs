using Cysharp.Web.Serializers;
using System.Runtime.CompilerServices;

namespace Cysharp.Web.Providers;

public sealed class GenericsWebSerializerProvider : IWebSerializerProvider
{
    public static IWebSerializerProvider Instance { get; } = new GenericsWebSerializerProvider();

    GenericsWebSerializerProvider()
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
            if (type.IsGenericType)
            {
                // Nullable<T>
                var nullableUnderlying = Nullable.GetUnderlyingType(type);
                if (nullableUnderlying != null)
                {
                    return CreateInstance(typeof(NullableWebSerializer<>), new[] { nullableUnderlying });
                }

                // Tuple/ValueTuple
                if (type.IsAssignableTo(typeof(ITuple)))
                {
                    var serializerType = (type.IsValueType)
                        ? TupleWebSerializer.GetValueTupleWebSerializerType(type.GenericTypeArguments.Length)
                        : TupleWebSerializer.GetTupleWebSerializerType(type.GenericTypeArguments.Length);

                    return CreateInstance(serializerType, type.GetGenericArguments());
                }
            }
            else if (type.IsEnum)
            {
                return CreateInstance(typeof(EnumStringWebSerializer<>), new[] { type });
            }

            return null;
        }
        catch (Exception ex)
        {
            return ErrorSerializer.Create(type, ex);
        }
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