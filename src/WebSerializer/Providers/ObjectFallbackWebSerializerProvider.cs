using Cysharp.Web.Serializers;

namespace Cysharp.Web.Providers;

public class ObjectFallbackWebSerializerProvider : IWebSerializerProvider
{
    public static IWebSerializerProvider Instance { get; } = new ObjectFallbackWebSerializerProvider();

    ObjectFallbackWebSerializerProvider()
    {

    }

    public IWebSerializer<T>? GetSerializer<T>()
    {
        if (typeof(T) == typeof(object))
        {
            return (IWebSerializer<T>)new ObjectFallbackWebSerializer();
        }

        return null;
    }
}
