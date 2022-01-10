using System.Runtime.CompilerServices;
using System.Text;

namespace Cysharp.Web;

public static class WebSerializer
{
    public static string ToQueryString<T>(in T value, WebSerializerOptions? options = default)
    {
        var sb = new StringBuilder();
        ToQueryString<T>(sb, value, options);
        return sb.ToString();
    }

    public static string ToQueryString<T>(string urlBase, in T value, WebSerializerOptions? options = default)
    {
        var sb = new StringBuilder();
        sb.Append(urlBase);
        sb.Append("?");

        var beforeLength = sb.Length;
        ToQueryString<T>(sb, value, options);

        if (sb.Length == beforeLength)
        {
            // trim last '?'
            sb.Remove(sb.Length - 1, 1);
        }

        return sb.ToString();
    }

    public static void ToQueryString<T>(StringBuilder stringBuilder, in T value, WebSerializerOptions? options = default)
    {
        var writer = new WebSerializerWriter(stringBuilder);
        ToQueryString(writer, value, options);
    }

    public static void ToQueryString<T>(in WebSerializerWriter writer, in T value, WebSerializerOptions? options = default)
    {
        options ??= WebSerializerOptions.Default;
        var serializer = options.GetRequiredSerializer<T>();
        serializer.Serialize(ref Unsafe.AsRef(writer), Unsafe.AsRef(value), options);
    }

    public static HttpContent ToHttpContent<T>(in T value, WebSerializerOptions? options = default)
    {
        var stringBuilder = new StringBuilder();
        ToQueryString<T>(stringBuilder, value, options);
        return new WebSerializerFormUrlEncodedContent(stringBuilder);
    }
}