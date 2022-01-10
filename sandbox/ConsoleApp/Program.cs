using Cysharp.Web;
using Refit;
using System.Net.Http.Json;
using System.Runtime.Serialization;
using System.Text;

var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };
//var api = RestService.For<IMinimumAPI>(client);
//await api.Get(10, "octocat");




//var q = WebSerializer.ToQueryString(req);

var tweet = new Tweet("foo", DateTimeOffset.Now.ToUnixTimeSeconds());
var user = new User(1999, "baz");


// use writer instead of StringBuilder
var writer = new WebSerializerWriter();

writer.NamePrefix = "tweet."; // set prefix by writer.
WebSerializer.ToQueryString(writer, tweet); // serialize to writer

writer.AppendConcatenate(); // Append '&'

writer.NamePrefix = "user.";
WebSerializer.ToQueryString(writer, user);

var q = writer.GetStringBuilder().ToString(); // get inner stringbuilder.

// tweet.created=1641816933&tweet.msg=foo&user.id=1999&user.name=baz
Console.WriteLine(q);





// ----

public record Tweet(string? msg, long created);
public record User(long id, string? name);

//Console.WriteLine(sb.ToString());


//Console.WriteLine(q);


public class MyRequest
{
    [DataMember(Name = "page", Order = 0)]
    public int Page { get; set; }
    [DataMember(Name = "direction", Order = 1)]
    public SortDirection Direction { get; set; }
    [DataMember(Name = "sortby", Order = 2)]
    public string? SortBy { get; set; }
}

public enum SortDirection
{
    [EnumMember(Value = "default")]
    Default,
    [EnumMember(Value = "asc")]
    Asc,
    [EnumMember(Value = "desc")]
    Desc
}


public class TwitterRequest
{
    [DataMember(Name = "tweet")]
    public Tweet? Tweet { get; set; }
    [DataMember(Name = "user")]
    public User? User { get; set; }
}


