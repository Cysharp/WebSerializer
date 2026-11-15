using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSerializerTests;

public class NullableTest
{
    [Fact]
    public void Obj()
    {
        var a = new NullableRequest()
        {
            Afoo = 100,
            Bbar = null,
            Ctor = 999
        };

        WebSerializer.ToQueryString(a).Should().Be("Afoo=100&Ctor=999");
    }

    [Fact]
    public void FirstMemberNull()
    {
        var a = new NullableRequest()
        {
            Afoo = null,
            Bbar = 200,
            Ctor = 999
        };

        WebSerializer.ToQueryString(a).Should().Be("Bbar=200&Ctor=999");
        WebSerializer.ToQueryString("/foo", a).Should().Be("/foo?Bbar=200&Ctor=999");

        var b = new NullableRequest()
        {
            Afoo = null,
            Bbar = null,
            Ctor = 999
        };

        WebSerializer.ToQueryString(b).Should().Be("Ctor=999");
    }

    [Fact]
    public void Dict()
    {
        var a = new Dictionary<string, int?>()
        {
            {"Afoo", 100 },
            {"Bbar", null },
            {"Ctor", 999 },
        };

        WebSerializer.ToQueryString(a).Should().Be("Afoo=100&Ctor=999");
    }

    public class NullableRequest
    {

        public int? Afoo { get; set; }
        public int? Bbar { get; set; }
        public int? Ctor { get; set; }
    }
}