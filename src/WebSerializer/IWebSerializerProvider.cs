namespace Cysharp.Web;

public interface IWebSerializerProvider
{
    IWebSerializer<T>? GetSerializer<T>();
}
