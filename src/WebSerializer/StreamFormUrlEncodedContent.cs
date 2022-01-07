using System.Buffers;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Cysharp.Web;

internal sealed class StreamFormUrlEncodedContent : HttpContent
{
    readonly StringBuilder stringBuilder;

    // avoid to create byte[] so don't use FormUrlEncodedContent, ByteArrayContent.
    public StreamFormUrlEncodedContent(StringBuilder stringBuilder)
    {
        this.stringBuilder = stringBuilder;
        Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
    }

    protected override void SerializeToStream(Stream stream, TransportContext? context, CancellationToken cancellationToken)
    {
        foreach (var item in stringBuilder.GetChunks())
        {
            var buffer = ArrayPool<byte>.Shared.Rent(item.Length);
            try
            {
                Encoding.Latin1.GetBytes(item.Span, buffer);
                stream.Write(buffer.AsSpan(0, item.Length));
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }
    }

    protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context)
    {
        return SerializeToStreamAsync(stream, context, default);
    }

    protected override async Task SerializeToStreamAsync(Stream stream, TransportContext? context, CancellationToken cancellationToken)
    {
        foreach (var item in stringBuilder.GetChunks())
        {
            var buffer = ArrayPool<byte>.Shared.Rent(item.Length);
            try
            {
                Encoding.Latin1.GetBytes(item.Span, buffer);
                await stream.WriteAsync(buffer, 0, item.Length, cancellationToken);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }
    }

    protected override bool TryComputeLength(out long length)
    {
        length = stringBuilder.Length;
        return true;
    }
}