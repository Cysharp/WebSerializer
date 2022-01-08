using Cysharp.Web;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Encodings.Web;


var dict = new Dictionary<string, object>
{
    { "hoge", 100 },
    { "tako", "tako yakiu dayoね!" }
};

var array = new[] { 1, 10, 100 };

var tako = WebSerializer.ToQueryString(new { foo = 100, tako = "nan\"yあo", hoge = (string?)null, zako = 99.3 });

var yaki = WebSerializer.ToQueryString(new MyRequest { Id = 11, More = new MoreRequest { Id = 99, Name = "yey" } });

Console.WriteLine(tako);
Console.WriteLine(yaki);


[DataContract(Namespace = "foo.")]
public class MyRequest
{
    [DataMember(Name = "id")]
    public int Id { get; set; } = default!;
    public MoreRequest? More { get; set; }

    public long MyProperty { get; set; }
}

[DataContract(Namespace = "bar.")]
public class MoreRequest
{
    [DataMember(Name = "id")]
    public int Id { get; set; } = default!;
    public string? Name { get; set; }
}

public class MyRequestSerialzier : IWebSerializer<MyRequest>
{
    public void Serialize(ref WebSerializerWriter writer, MyRequest value, WebSerializerOptions options)
    {
        writer.EnterAndValidate(options);

        // foreach(members)
        //   if (options.PrefixName != null) writer.Write(writer, options.PrefixName)
        //   writer.Write(.EncodedName)
        //   writer.Write('=')
        //   provider.GetSerializer<T>().Serialize(writer, options, ref value.Foo);
        var encodedIdPlusEqual = UrlEncoder.Default.Encode("Id") + "=";
        var encodedNamePlusEqual = UrlEncoder.Default.Encode("Name") + "=";

        //var sb = writer.GetStringBuilder();
        var urlEncoder = options.Encoder;


        //writer.AppendNamePrefix();
        //writer.AppendRaw(encodedIdPlusEqual);
        //options.GetRequiredSerializer<int>().Serialize(ref writer, value.Id, options);

        //writer.AppendConcatenate();

        //if (value.Name != null) // ref type or nullable
        //{
        //    if (writer.NamePrefix != null)
        //    {
        //        sb.Append(writer.NamePrefix); // already encoded
        //    }
        //    sb.Append(encodedNamePlusEqual);
        //    options.GetRequiredSerializer<string>().Serialize(ref writer, value.Name, options);
        //}

        writer.Exit();
    }
}
