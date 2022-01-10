using System.Buffers;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Cysharp.Web;

public sealed class WebSerializerFormUrlEncodedContent : HttpContent
{
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

    protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context)
    {
        return SerializeToStreamAsync(stream, context, default);
    }

    protected override async Task SerializeToStreamAsync(Stream stream, TransportContext? context, CancellationToken cancellationToken)
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
        var span = buffer.AsSpan();
        foreach (var item in stringBuilder.GetChunks()) // stream.WriteAsync per chunk is slow, encode to buffer all data
        {
            var written = Encoding.Latin1.GetBytes(item.Span, span);
            span = span.Slice(written);
        }
    }

    protected override bool TryComputeLength(out long length)
    {
        length = stringBuilder.Length;
        return true;
    }
}