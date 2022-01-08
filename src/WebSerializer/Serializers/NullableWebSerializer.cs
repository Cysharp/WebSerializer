namespace Cysharp.Web.Serializers;

public sealed class NullableWebSerializer<T> : IWebSerializer<T?>
    where T : struct
{
    public void Serialize(ref WebSerializerWriter writer, T? value, WebSerializerOptions options)
    {
        if (value == null) return;
        options.GetRequiredSerializer<T>().Serialize(ref writer, value.Value, options);
    }
}