using Cysharp.Web;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Encodings.Web;

var m = new More();
m.WakaMore = m;
var s = WebSerializer.ToQueryString(m);
Console.WriteLine(s);


public enum Tako
{
    Yaki,
    [EnumMember(Value = "oreoreore")]
    dayo
}

public class More
{
    public More? WakaMore { get; set; }
}
