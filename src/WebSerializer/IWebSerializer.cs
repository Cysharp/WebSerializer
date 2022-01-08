using System.Globalization;

namespace Cysharp.Web;

public interface IWebSerializer
{
}

public interface IWebSerializer<T> : IWebSerializer
{
    void Serialize(ref WebSerializerWriter writer, T value, WebSerializerOptions options);
}
