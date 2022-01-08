using Cysharp.Web.Serializers;

namespace Cysharp.Web.Providers;

public sealed class BuiltinWebSerializerProvider : IWebSerializerProvider
{
    public static IWebSerializerProvider Instance { get; } = new BuiltinWebSerializerProvider();

    readonly Dictionary<Type, IWebSerializer> serializers = new Dictionary<Type, IWebSerializer>()
    {
        { typeof(string), new StringWebSerializer() },
        { typeof(Guid), new GuidWebSerializer() },
        { typeof(DateTime), new DateTimeWebSerializer() },
        { typeof(DateTimeOffset), new DateTimeOffsetWebSerializer() },
        { typeof(TimeSpan), new TimeSpanWebSerializer() },
        { typeof(DateOnly), new DateOnlyWebSerializer() },
        { typeof(TimeOnly), new TimeOnlyWebSerializer() },
        { typeof(Uri), new UriWebSerializer() },
    };

    BuiltinWebSerializerProvider()
    {

    }

    public IWebSerializer<T>? GetSerializer<T>()
    {
        if (serializers.TryGetValue(typeof(T), out var value))
        {
            return (IWebSerializer<T>)value;
        }
        return null;
    }
}
