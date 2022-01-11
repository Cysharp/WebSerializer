using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace WebSerializerTests;

public class CollectionTest
{
    [Fact]
    public void Array()
    {
        var foo = new[] { 10, 20, 30 };
        WebSerializer.ToQueryString(new { foo }).Should().Be("foo=10&foo=20&foo=30");

        var newConfig = WebSerializerOptions.Default with { CollectionSeparator = "," };
        WebSerializer.ToQueryString(new { foo }, newConfig).Should().Be("foo=10,20,30");
    }

    [Fact]
    public void Dict()
    {
        var foo = new Dictionary<string, object>
        {
            {"Yeah", 100 },
            {"Kuooo", "nano yo" },
            {"DEAR", true }
        };

        var newConfig = WebSerializerOptions.Default with
        {
            CultureInfo = CultureInfo.CurrentCulture,
            CollectionSeparator = "  ",
            Provider = WebSerializerProvider.Create(
                new[] { new BoolZeroOneSerializer() },
                new[] { WebSerializerProvider.Default })
        };

        var sb = new StringBuilder();
        foreach (var item in foo)
        {
            if (sb.Length != 0) sb.Append('&');
            sb.Append(item.Key + "=" + UrlEncoder.Default.Encode(item.Value.ToString() ?? ""));
        }

        WebSerializer.ToQueryString(foo, newConfig).Should().Be("Yeah=100&Kuooo=nano%20yo&DEAR=0");
    }


    public class BoolZeroOneSerializer : IWebSerializer<bool>
    {
        public void Serialize(ref WebSerializerWriter writer, bool value, WebSerializerOptions options)
        {
            // true => 0, false => 1
            writer.AppendPrimitive(value ? 0 : 1);
        }
    }
}