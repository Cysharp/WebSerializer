using Cysharp.Web;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;


Type type = typeof(int);

var ok = typeof(WebSerializerOptions).GetMethod2("GetRequiredSerializer", 1, Type.EmptyTypes)!.MakeGenericMethod(type);
Console.WriteLine(ok);

var a = WebSerializer.ToQueryString(new MyClass { MyProperty = 100 });
Console.WriteLine(a);

//GCHandle handle = GCHandle.Alloc("", GCHandleType.Pinned);


return;



public class MyClass
{
    public int MyProperty { get; set; }
}

internal static class ReflectionShims
{
    internal static MethodInfo? GetMethod2(this Type type, string name, int genericParameterCount, Type[] types)
    {
        return type.GetMethods().FirstOrDefault(x =>
        {
            var genericArguments = x.GetGenericArguments();

            if (x.Name != name) return false;
            if (genericArguments.Length != genericParameterCount) return false;

            // Empty is ok.
            if (types.Length == 0) return true;

            // currently WebSerializer only uses Type.EmptyTypes so not implement it.
            throw new NotSupportedException();
        });
    }
}

//var foo = WebSerializer.ToQueryString(new { hoge = new[] { 1, 10, 100 }, huga = (1000, 2000, 3000) });
//var foo = WebSerializer.ToQueryString(new { hoge = new[] { 1, 100, 1000 } });
//Console.WriteLine(foo);


////var q = WebSerializer.ToQueryString(req);

//var tweet = new Tweet("foo", DateTimeOffset.Now.ToUnixTimeSeconds());
//var user = new User(1999, "baz");


//// use writer instead of StringBuilder
//var writer = new WebSerializerWriter();

//writer.NamePrefix = "tweet."; // set prefix by writer.
//WebSerializer.ToQueryString(writer, tweet); // serialize to writer

//writer.AppendConcatenate(); // Append '&'

//writer.NamePrefix = "user.";
//WebSerializer.ToQueryString(writer, user);

//var q = writer.GetStringBuilder().ToString(); // get inner stringbuilder.

//// tweet.created=1641816933&tweet.msg=foo&user.id=1999&user.name=baz
//Console.WriteLine(q);





// ----


//public class PagingRequest
//{

//    [DataMember(Order = 1)]
//    public string? SortBy { get; init; }
//    [DataMember(Order = 2)]
//    public SortDirection SortDirection { get; init; }
//    [DataMember(Order = 0)]
//    public int CurrentPage { get; init; } = 1;
//}



//public record Tweet(string? msg, long created);
//public record User(long id, string? name);

////Console.WriteLine(sb.ToString());


////Console.WriteLine(q);


//public class MyRequest
//{
//    [DataMember(Name = "page", Order = 0)]
//    public int Page { get; set; }
//    [DataMember(Name = "direction", Order = 1)]
//    public SortDirection Direction { get; set; }
//    [DataMember(Name = "sortby", Order = 2)]
//    public string? SortBy { get; set; }
//}

//public enum SortDirection
//{
//    [EnumMember(Value = "default")]
//    Default,
//    [EnumMember(Value = "asc")]
//    Asc,
//    [EnumMember(Value = "desc")]
//    Desc
//}


//public class TwitterRequest
//{
//    [DataMember(Name = "tweet")]
//    public Tweet? Tweet { get; set; }
//    [DataMember(Name = "user")]
//    public User? User { get; set; }
//}


