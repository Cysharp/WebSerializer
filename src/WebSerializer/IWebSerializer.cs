using System.Globalization;

namespace Cysharp.Web;

public interface IWebSerializer<T>
{
    void Serialize(StringWriter writer, ref T value, WebSerializerOptions options);
}

// TODO: move to serializers/
public sealed class IntWebSerializer : IWebSerializer<int>
{
    readonly CultureInfo? cultureInfo;

    public IntWebSerializer(CultureInfo? cultureInfo)
    {
        this.cultureInfo = cultureInfo;
    }

    public void Serialize(StringWriter writer, ref int value, WebSerializerOptions options)
    {
        var sb = writer.GetStringBuilder();
        if (cultureInfo == null)
        {
            sb.Append(value);
        }
        else
        {
            sb.Append(cultureInfo, $"{cultureInfo}");
        }
    }
}