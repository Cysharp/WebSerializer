namespace Cysharp.Web.Serializers;

public sealed class StringWebSerializer : IWebSerializer<string?>
{
    public void Serialize(ref WebSerializerWriter writer, string? value, WebSerializerOptions options)
    {
        if (value == null) return;
        writer.Append(value, options);
    }
}

public sealed class GuidWebSerializer : IWebSerializer<Guid>
{
    public void Serialize(ref WebSerializerWriter writer, Guid value, WebSerializerOptions options)
    {
        // no need url-encode(guid tostring is safe).
        // using AppendInterpolatedStringHandler is fastest way to write Guid.
#if NET6_0_OR_GREATER
        writer.GetStringBuilder().Append($"{value}");
#else
        writer.GetStringBuilder().Append(value.ToString());
#endif
    }
}

public sealed class DateTimeWebSerializer : IWebSerializer<DateTime>
{
    public void Serialize(ref WebSerializerWriter writer, DateTime value, WebSerializerOptions options)
    {
        writer.Append(value.ToString(options.CultureInfo), options);
    }
}

public sealed class DateTimeOffsetWebSerializer : IWebSerializer<DateTimeOffset>
{
    public void Serialize(ref WebSerializerWriter writer, DateTimeOffset value, WebSerializerOptions options)
    {
        writer.Append(value.ToString(options.CultureInfo), options);
    }
}

public sealed class TimeSpanWebSerializer : IWebSerializer<TimeSpan>
{
    public void Serialize(ref WebSerializerWriter writer, TimeSpan value, WebSerializerOptions options)
    {
        writer.Append(value.ToString(), options);
    }
}

public sealed class UriWebSerializer : IWebSerializer<Uri>
{
    public void Serialize(ref WebSerializerWriter writer, Uri value, WebSerializerOptions options)
    {
        if (value == null) return;
        writer.Append(value.ToString(), options);
    }
}

#if NET6_0_OR_GREATER

public sealed class DateOnlyWebSerializer : IWebSerializer<DateOnly>
{
    public void Serialize(ref WebSerializerWriter writer, DateOnly value, WebSerializerOptions options)
    {
        writer.Append(value.ToString(options.CultureInfo), options);
    }
}


public sealed class TimeOnlyWebSerializer : IWebSerializer<TimeOnly>
{
    public void Serialize(ref WebSerializerWriter writer, TimeOnly value, WebSerializerOptions options)
    {
        writer.Append(value.ToString(options.CultureInfo), options);
    }
}

#endif