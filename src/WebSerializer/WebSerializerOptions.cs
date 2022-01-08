using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;

namespace Cysharp.Web;

public record WebSerializerOptions(IWebSerializerProvider Provider)
{
    public static WebSerializerOptions Default { get; } = new WebSerializerOptions(WebSerializerProvider.Default);

    public CultureInfo? CultureInfo { get; init; }
    public UrlEncoder Encoder { get; init; } = UrlEncoder.Default;
    public int MaxDepth { get; init; } = 64;

    string separator = ",";
    public string CollectionSeparator
    {
        get => separator;
        init
        {
            separator = Encoder.Encode(value);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IWebSerializer<T>? GetSerializer<T>()
    {
        return Provider.GetSerializer<T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IWebSerializer<T> GetRequiredSerializer<T>()
    {
        var serializer = Provider.GetSerializer<T>();
        if (serializer == null) Throw(typeof(T));
        return serializer;
    }

    [DoesNotReturn]
    void Throw(Type type)
    {
        throw new InvalidOperationException($"Type is not found in provider. Type:{type}");
    }
}