namespace Cysharp.Web.Providers;

public sealed partial class PrimitiveWebSerializerProvider : IWebSerializerProvider
{
    public static IWebSerializerProvider Instance { get; } = new PrimitiveWebSerializerProvider();

    readonly Dictionary<Type, IWebSerializer> serializers = new Dictionary<Type, IWebSerializer>();

    internal partial void InitPrimitives(); // implement from PrimitiveSerializers.cs

    PrimitiveWebSerializerProvider()
    {
        InitPrimitives();
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