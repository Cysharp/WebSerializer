using Cysharp.Web;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Encodings.Web;


var s = WebSerializer.ToQueryString(Tako.dayo);
Console.WriteLine(s);


public enum Tako
{
    Yaki,
    [EnumMember(Value ="oreoreore")]
    dayo
}