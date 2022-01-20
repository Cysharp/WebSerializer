using System.Runtime.Serialization;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;

namespace WebSerializerTests;

public class StandardTest
{
    [Fact]
    public void ToQueryString()
    {


        var req = new PagingRequest
        {
            SortDirection = SortDirection.Default,
            CurrentPage = 10,
            SortBy = "hoge and 日本語 japanese"
        };

        var nullreq = new PagingRequest
        {
            SortDirection = SortDirection.Asc,
            CurrentPage = 8888,
            SortBy = null
        };



        var one = WebSerializer.ToQueryString(req);
        var two = WebSerializer.ToQueryString("/hogemoge", req);
        var three = WebSerializer.ToQueryString(nullreq);
        var four = WebSerializer.ToQueryString("/hogemoge", nullreq);

        one.Should().Be("CurrentPage=10&SortBy=hoge%20and%20%E6%97%A5%E6%9C%AC%E8%AA%9E%20japanese&SortDirection=Default");
        two.Should().Be("/hogemoge?CurrentPage=10&SortBy=hoge%20and%20%E6%97%A5%E6%9C%AC%E8%AA%9E%20japanese&SortDirection=Default");
        three.Should().Be("CurrentPage=8888&SortDirection=Asc");
        four.Should().Be("/hogemoge?CurrentPage=8888&SortDirection=Asc");
    }

    [Fact]
    public async Task ToHttpContent()
    {
        var req = new PagingRequest
        {
            SortDirection = SortDirection.Default,
            CurrentPage = 10,
            SortBy = "hoge and 日本語 japanese"
        };

        var content = WebSerializer.ToHttpContent(req);

        var str1 = await content.ReadAsStringAsync();

        var form = new FormUrlEncodedContent(new KeyValuePair<string?, string?>[]
        {
            new ("CurrentPage", "10" ),
            new ("SortBy", "hoge and 日本語 japanese" ),
            new ("SortDirection", "Default" ),
        });

        var str2 = (await form.ReadAsStringAsync()).Replace("+", "%20");

        str1.Should().Be(str2);
    }

    [Fact]
    public void CheckRecursive()
    {
        var r = new RecReq();
        r.MyProperty = r;

        Assert.Throws<InvalidOperationException>(() => WebSerializer.ToQueryString(r));
    }

    public class PagingRequest
    {

        [DataMember(Order = 1)]
        public string? SortBy { get; init; }
        [DataMember(Order = 2)]
        public SortDirection SortDirection { get; init; }
        [DataMember(Order = 0)]
        public int CurrentPage { get; init; } = 1;
    }


    public enum SortDirection
    {
        Default,
        Asc,
        Desc
    }

    public class RecReq
    {
        public RecReq? MyProperty { get; set; }
    }
}