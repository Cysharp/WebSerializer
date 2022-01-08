using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;

namespace Cysharp.Web;

public struct WebSerializerWriter
{
    readonly StringBuilder stringBuilder;

    StringWriter? writer;
    StringWriter Writer => writer ??= new StringWriter(stringBuilder);

    int currentDepth;

    internal string? namePrefix;
    public string? NamePrefix
    {
        get => namePrefix;
        init
        {
            if (value != null)
            {
                namePrefix = UrlEncoder.Default.Encode(value);
            }
            else
            {
                namePrefix = null;
            }
        }
    }

    public WebSerializerWriter(StringBuilder stringBuilder)
    {
        this.stringBuilder = stringBuilder;
        this.namePrefix = null;
        this.writer = null;
        this.currentDepth = 0;
    }

    public StringBuilder GetStringBuilder() => stringBuilder;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EnterAndValidate(WebSerializerOptions options)
    {
        currentDepth++;
        if (currentDepth >= options.MaxDepth)
        {
            ThrowReachedMaxDepth(currentDepth);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Exit()
    {
        currentDepth--;
    }

    /// <summary>Append nameprefix.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendNamePrefix()
    {
        if (namePrefix != null)
        {
            stringBuilder.Append(namePrefix);
        }
    }

    /// <summary>Append raw string.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendRaw(string value)
    {
        stringBuilder.Append(value);
    }

    /// <summary>Append '&'.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendConcatenate()
    {
        stringBuilder.Append('&');
    }

    /// <summary>Append encoded string.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(WebSerializerOptions options, string value)
    {
        options.Encoder.Encode(Writer, value);
    }

    /// <summary>Append encoded string.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(WebSerializerOptions options, char[] value, int start, int count)
    {
        options.Encoder.Encode(Writer, value, start, count);
    }

    static void ThrowReachedMaxDepth(int depth)
    {
        throw new InvalidOperationException($"Serializer detects reached max depth:{depth}. Please check the circular reference.");
    }
}