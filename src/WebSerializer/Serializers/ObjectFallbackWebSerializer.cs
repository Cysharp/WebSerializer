using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Cysharp.Web.Serializers;

internal class ObjectFallbackWebSerializer : IWebSerializer<object>
{
    delegate void SerializeDelegate(ref WebSerializerWriter writer, object value, WebSerializerOptions options);
    static readonly ConcurrentDictionary<Type, SerializeDelegate> nongenericSerializers = new ConcurrentDictionary<Type, SerializeDelegate>();
    static readonly Func<Type, SerializeDelegate> factory = CompileSerializeDelegate;

    public void Serialize(ref WebSerializerWriter writer, object value, WebSerializerOptions options)
    {
        if (value == null) return;

        var type = value.GetType();
        if (type == typeof(object)) return;

        var serializer = nongenericSerializers.GetOrAdd(type, factory);
        serializer.Invoke(ref writer, value, options);
    }

    static SerializeDelegate CompileSerializeDelegate(Type type)
    {
        // Serialize(ref WebSerializerWriter writer, object value, WebSerializerOptions options)
        //   options.GetRequiredSerializer<T>().Serialize(ref writer, (T)value, options)

        var writer = Expression.Parameter(typeof(WebSerializerWriter).MakeByRefType());
        var value = Expression.Parameter(typeof(object));
        var options = Expression.Parameter(typeof(WebSerializerOptions));

        var getRequiredSerializer = typeof(WebSerializerOptions).GetMethod("GetRequiredSerializer", 1, Type.EmptyTypes)!.MakeGenericMethod(type);
        var serialize = typeof(IWebSerializer<>).MakeGenericType(type).GetMethod("Serialize")!;

        var body = Expression.Call(
            Expression.Call(options, getRequiredSerializer),
            serialize,
            writer,
            Expression.Convert(value, type),
            options);

        var lambda = Expression.Lambda<SerializeDelegate>(body, writer, value, options);
        return lambda.Compile();
    }
}