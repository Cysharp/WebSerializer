using System.Runtime.CompilerServices;
using System.Text;

namespace Cysharp.Web;

public static class WebSerializer
{
    public static string ToQueryString<T>(in T value)
    {
        var sb = new StringBuilder();
        ToQueryString<T>(sb, value);
        return sb.ToString();
    }

    public static string ToQueryString<T>(string urlBase, in T value)
    {
        var sb = new StringBuilder();
        sb.Append(urlBase);
        sb.Append("?");

        var beforeLength = sb.Length;
        ToQueryString<T>(sb, value);

        if (sb.Length == beforeLength)
        {
            // trim last '?'
            sb.Remove(sb.Length - 1, 1);
        }

        return sb.ToString();
    }

    public static void ToQueryString<T>(StringBuilder stringBuilder, in T value, WebSerializerOptions? options = default)
    {
        using var writer = new StringWriter(stringBuilder);
        ToQueryString(writer, value, options);
    }

    public static void ToQueryString<T>(StringWriter writer, in T value, WebSerializerOptions? options = default)
    {
        options ??= new WebSerializerOptions(null!);
        var serialzier = options.GetRequiredSerializer<T>();
        serialzier.Serialize(writer, ref Unsafe.AsRef(value), options);
    }

    public static HttpContent ToHttpContent<T>(in T value)
    {
        throw new NotImplementedException();
    }
}