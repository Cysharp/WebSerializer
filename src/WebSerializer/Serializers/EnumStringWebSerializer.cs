using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Encodings.Web;

namespace Cysharp.Web.Serializers;

public sealed class EnumStringWebSerializer<T> : IWebSerializer<T>
    where T : Enum
{
    static readonly ConcurrentDictionary<T, string> stringCache = new();
    static readonly Func<T, string> toStringFactory = EnumToString;


    public void Serialize(ref WebSerializerWriter writer, T value, WebSerializerOptions options)
    {
        var str = stringCache.GetOrAdd(value, toStringFactory);
        writer.AppendRaw(str);
    }

    static string EnumToString(T value)
    {
        var str = value.ToString();
        var field = value.GetType().GetField(str);
        if (field != null)
        {
            var enumMember = field.GetCustomAttribute<EnumMemberAttribute>();
            if (enumMember != null && enumMember.Value != null)
            {
                str = enumMember.Value;
            }
        }

        // not use WebSerializerOptions.Encoder for performance
        return UrlEncoder.Default.Encode(str);
    }
}

public sealed class EnumValueWebSerializer<T> : IWebSerializer<T>
    where T : Enum
{
    static readonly ConcurrentDictionary<T, string> stringCache = new();
    static readonly Func<T, string> toStringFactory = EnumToString;

    public void Serialize(ref WebSerializerWriter writer, T value, WebSerializerOptions options)
    {
        var str = stringCache.GetOrAdd(value, toStringFactory);
        writer.AppendRaw(str);
    }

    static string EnumToString(T value)
    {
        return Convert.ChangeType(value, Enum.GetUnderlyingType(typeof(T))).ToString()!;
    }
}