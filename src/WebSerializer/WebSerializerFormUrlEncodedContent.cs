using System.Buffers;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Cysharp.Web;

public sealed class WebSerializerFormUrlEncodedContent : HttpContent
{
    static readonly Encoding Latin1Encoding =
#if (NETSTANDARD2_0 || NETSTANDARD2_1)
        Encoding.GetEncoding(28591);
#else
        Encoding.Latin1;
#endif

    readonly StringBuilder stringBuilder;

    // avoid to create byte[] so don't use FormUrlEncodedContent, ByteArrayContent.
    public WebSerializerFormUrlEncodedContent(StringBuilder stringBuilder)
    {
        this.stringBuilder = stringBuilder;
        Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
    }

    public WebSerializerFormUrlEncodedContent(WebSerializerWriter webSerializerWriter)
        : this(webSerializerWriter.GetStringBuilder())
    {
    }

#if !(NETSTANDARD2_0 || NETSTANDARD2_1)

    protected override void SerializeToStream(Stream stream, TransportContext? context, CancellationToken cancellationToken)
    {
        var buffer = ArrayPool<byte>.Shared.Rent(stringBuilder.Length);
        try
        {
            EncodeToBuffer(buffer, stringBuilder);
            stream.Write(buffer, 0, stringBuilder.Length);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }

#endif

    protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context)
    {
        return SerializeToStreamAsync(stream, context, default);
    }

#if !(NETSTANDARD2_0 || NETSTANDARD2_1)
    protected override 
#endif
    async Task SerializeToStreamAsync(Stream stream, TransportContext? context, CancellationToken cancellationToken)
    {
        var buffer = ArrayPool<byte>.Shared.Rent(stringBuilder.Length);
        try
        {
            EncodeToBuffer(buffer, stringBuilder);
            await stream.WriteAsync(buffer, 0, stringBuilder.Length, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }

    static void EncodeToBuffer(byte[] buffer, StringBuilder stringBuilder)
    {
#if (NETSTANDARD2_0 || NETSTANDARD2_1)
        Latin1Encoding.GetBytes(stringBuilder.ToString()).CopyTo(buffer.AsSpan());
#else
        var span = buffer.AsSpan();
        foreach (var item in stringBuilder.GetChunks()) // stream.WriteAsync per chunk is slow, encode to buffer all data
        {
            var written = Latin1Encoding.GetBytes(item.Span, span);
            span = span.Slice(written);
        }
#endif
    }

    protected override bool TryComputeLength(out long length)
    {
        length = stringBuilder.Length;
        return true;
    }
}