WebSerializer
===
[![GitHub Actions](https://github.com/Cysharp/WebSerializer/workflows/Build-Debug/badge.svg)](https://github.com/Cysharp/WebSerializer/actions) [![Releases](https://img.shields.io/github/release/Cysharp/WebSerializer.svg)](https://github.com/Cysharp/WebSerializer/releases)

WebSerializer converts object into HTTP/1 QueryString/FormUrlEncodedContent for C# HttpClient REST Request. For response, HttpClient has [ReadFromJsonAsync](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.json.httpcontentjsonextensions.readfromjsonasync) so can map to object directly, however for request, you should build request data(query-string or form-urlencoded-content) manually. [refit](https://github.com/reactiveui/refit) solves that problem, but WebSerializer provides a more lightweight way that focuses on building only the request parameters.

```csharp
// Request-object to query-string
var q = WebSerializer.ToQueryString(req); // foo=aaa&bar=100&baz=zzz

// Method argument to query-string
var q = WebSerializer.ToQueryString(new { foo, bar, baz }); // foo=aaa&bar=100&baz=zzz

// With the url-base
var url = WebSerializer.ToQueryString("https://foo/search", req); // https://foo/search?foo=aaa&bar=100&baz=zzz

// For Post, create form-url-encoded HttpContent
var content - WebSerializer.ToHttpContent(req);
```

Also, the allocations are very low and the performance is very good. It is designed to be on the same level as [MessagePack for C#](https://github.com/neuecc/MessagePack-CSharp), a fast binary serializer by the same author.

Getting Started
---
Supporting platform is .NET 5, .NET 6.

> PM> Install-Package [WebSerializer](https://www.nuget.org/packages/WebSerializer)

You can use `WebSerializer.ToQueryString` or `WebSerializer.ToHttpContent` to build the request parameter.

```csharp
using Cysharp.Web; // namespace

var req = new Request(sortBy: "id", direction: SortDirection.Desc, currentPage: 3)

// sortBy=id&direction=Desc&currentPage=3
var q = WebSerializer.ToQueryString(req);

await httpClient.GetAsync("/sort?"+ q);

// data...
public record Request(string? sortBy, SortDirection direction, int currentPage);

public enum SortDirection
{
    Default,
    Asc,
    Desc
}
```

If you want to build parameter from method argument, use anonymous type is the best way for it.

```csharp
// If value is null, omitted from build parameter.
// For exampe, (null, SortDirection.Asc, 1) => direction=Asc&currentPage=1
public string BuildSearchRequest(string? sortBy, SortDirection direction, int currentPage)
{
    // Pass url in first argument, build url string with `?`
    const string UrlBase = "https://foo/bar/search";
    return WebSerializer.ToQueryString(UrlBase, new { sortBy, direction, currentPage });
}
```

For the Post method, you can use `WebSerializer.ToHttpContent` to build form-url-encoded `HttpContent`.

```csharp
async Task PostMessage(string name, string email, string message)
{
    var content = WebSerializer.ToHttpContent(new { name, email, message });
    await httpClient.PostAsync("/postmsg", content);
}
```

IF you want to build parameters dynamic in application, use `Dictionary<string, object>`(also allows `<TKey, TValue>`) or `IEnumerbale<KeyValuePair<string, object>>`(also allows `<TKey, TValue>`).

```csharp
var req = new Dictionary<string, object>();
req.Add("sortBy", "id");
req.Add("direction", SortDirection.Asc);
req.Add("currentPage", 10);

// sortBy=id&direction=Asc&currentPage=10
var q = WebSerializer.ToQueryString(req);

// also allows IEnumerable<KeyValuePair<TKey, TValue>>

var parameters = new KeyValuePair<string, string>[]
{
    new ("id", "1"),
    new ("name", "tanaka"),
    new ("email", "test@example.com")
};

// https://example.com/user?id=1&name=tanaka&email=test%40example.com
var url = WebSerializer.ToQueryString("https://example.com/user", parameters);
```

`WebSerializerProvider` and `IWebSerialize<T>`
---
To customize behaviour of serialization, you can create `IWebSerializer<T>` and set to `IWebSerializerProvider`.

```csharp
public interface IWebSerializer<T> : IWebSerializer
{
    void Serialize(ref WebSerializerWriter writer, T value, WebSerializerOptions options);
}
```

For example, create custom serializer that convert true/false to 0/1 string.

```csharp
public class BoolZeroOneSerializer : IWebSerializer<bool>
{
    public void Serialize(ref WebSerializerWriter writer, bool value, WebSerializerOptions options)
    {
        // true => 0, false => 1
        writer.AppendPrimitive(value ? 0 : 1);
    }
}
```

`WebSerializerProvider.Create` to composite custom providers.

```csharp
var customProvider = WebSerializerProvider.Create(
    new[] { new BoolZeroOneSerializer() },
    new[] { WebSerializerProvider.Default });
```

Finally, set it to `WebSerializerOptions` and use it for serializing.

```csharp
// use C# 9.0 with expression for build WebSerializerOptions
var customOptions = WebSerializerOptions.Default with
{
    Provider = customProvider
};

var q = WebSerializer.ToQueryString(request, customOptions);
```

Advanced note, in default, `WebSerializerProvider.Default` is composited there internal providers in this order.

```csharp
IWebSerializerProvider[] providers = new[]
{
    PrimitiveWebSerializerProvider.Instance, // int, double, etc...
    BuiltinWebSerializerProvider.Instance, // string, DateTime, Uri, etc...
    AttributeWebSerializerProvider.Instance, // [WebSerializer] custom serializer
    GenericsWebSerializerProvider.Instance, // Nullable<T>, Enum, Tuple, ValueTuple
    CollectionWebSerializerProvider.Instance, // T[], IEnumerable<T>, IDictionary<TKey, TValue>
    ObjectFallbackWebSerializerProvider.Instance, // object -> <T> serializer
    ObjectGraphWebSerializerProvider.Instance // T
};
```

Configure serialized name
---
Configure serialized name, you can use `DataMember/EnumMember` attributes to customize it.

```csharp
var req = new MyRequest { Page = 10, Direction = SortDirection.Asc, SortBy = "id" };
// page=10&direction=asc&sortby=id
var q = WebSerializer.ToQueryString(req);

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
```

`Order` is optional, default is ordered by name.

Nested type and NamePrefix
---
Currently WebSerializer does not flatten value when type is nested. You should use `ToQueryString(StringBuilder)` to append multiple times on your own. And if you want to add name-prefix to there type, you can use `[DataContract(Namespace = )]` to set it.

```csharp

var tweet = new Tweet { Message = "foo", PostTime = DateTimeOffset.Now.ToUnixTimeSeconds() };
var user = new User { Id = 1999, UserName = "baz" };

var sb = new StringBuilder();
WebSerializer.ToQueryString(sb, tweet); // serialize to stringbuilder
sb.Append("&");
WebSerializer.ToQueryString(sb, user);

// tweet.created=1641816420&tweet.msg=foo&user.id=1999&user.name=baz
Console.WriteLine(sb.ToString());

// ----

[DataContract(Namespace = "tweet.")]
public class Tweet
{
    [DataMember(Name = "msg")]
    public string? Message { get; set; }
    [DataMember(Name = "created")]
    public long PostTime { get; set; }
}

[DataContract(Namespace = "user.")]
public class User
{
    [DataMember(Name = "id")]
    public long Id { get; set; }
    [DataMember(Name = "name")]
    public string? UserName { get; set; }
}
```

If type has not `DataContract(Namespace)` or add diffrent namespaces per request type, use `WebSerializerWriter` to configure it.


```csharp
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
```

For Post method, you can use `WebSerializerFormUrlEncodedContent(WebSerializerWriter)` to create `HttpContent`.

```csharp
var content = new WebSerializerFormUrlEncodedContent(writer);
```

WebSerializerAttribute
---
Register to `WebSerializerProvider` affects all type. If you want to configure per member or create own custom serializer to type, use `WebSerializerAttribute`.

```csharp
public class MyRequest
{
    // Timestamp is serialized by UnixSecondsSerializer
    [WebSerializer(typeof(UnixSecondsSerializer))]
    public DateTime Timestamp { get; set; }

    public string? Name { get; set; }
}

public class UnixSecondsSerializer : IWebSerializer<DateTime>
{
    public void Serialize(ref WebSerializerWriter writer, DateTime value, WebSerializerOptions options)
    {
        writer.AppendPrimitive(((DateTimeOffset)(value)).ToUnixTimeSeconds());
    }
}
```

Add attribute to class/struct/enum declaration, use custom serializer instead of default behaviour. `options.GetRequiredSerializer<T>` is useful to get `IWebSerialzier<T>`.

```csharp
[WebSerializer(typeof(CustomRequestSerialzier))]
public class Request
{
    public int MyProperty1 { get; set; }
    public int MyProperty2 { get; set; }
}

public class CustomRequestSerialzier : IWebSerializer<Request>
{
    public void Serialize(ref WebSerializerWriter writer, Request value, WebSerializerOptions options)
    {
        // begin
        writer.EnterAndValidate(options); // recommend to use Enter to detect circular reference.
        writer.AppendNamePrefix(); // recommend to use AppendNamePrefix to check nameprefix option

        // foreach members
        writer.Append("mp1", options);
        writer.AppendEqual(); // write '='
        options.GetRequiredSerializer<int>(ref writer, value.MyProperty1 * 10, options);

        writer.Append("mp2", options);
        writer.AppendEqual(); // write '='
        options.GetRequiredSerializer<int>(ref writer, value.MyProperty2 * 20, options);
        
        // end
        writer.Exit(); // If use Enter, should use Exit by pair
    }
}
```

WebSerializerOptions
---
WebSerializerOptions is immutable record, you can configure new options uses C# 9.0 `with` expression.

```csharp
// CultureInfo: Used by converting number or DateTime, etc. Default is null.
// CollectionSeparator: Separator of converting array, etc. Default is null(concatenate as name=value&name=value...)
// Encoder: Url encoder of serialization, Default is custom encoder(UrlEncoder.Default with ';' and '@' Encode)
// MaxDepth: Detect circular reference, Default is 64.
// Provider: Setup custom serializer by type. Default is `WebSerializerProvider.Default`.
var newConfig = WebSerializerOptions.Default with
{
    CultureInfo = CultureInfo.InvariantCulture,
    CollectionSeparator = ",",
    Encoder = UrlEncoder.Default,
    MaxDepth = 32,
    Provider = WebSerializerProvider.Create(
        new[] { new BoolZeroOneSerializer() },
        new[] { WebSerializerProvider.Default })
};
```

And also `GetSerializer<T>` and `GetRequiredSerializer<T>` to get serializer from provider.

WebSerializerWriter
---
WebSerializerWriter is writing state of serialization. It can configure `NamePrefix` and has some Append helper methods.

```csharp
string? NamePrefix { get; set; }

// Require to create custom-serializer of object beginning.
EnterAndValidate(WebSerializerOptions options)
Exit();

// Write NamePrefix if NamePrefix is not null
AppendNamePrefix()

// Append raw string(not encoded), useful when string is already encoded
AppendRaw(string value)
// Append '&'
AppendConcatenate()
// Append '='
AppendEqual()
// Append string value with url-encoded
Append(string value, WebSerializerOptions options)
Append(char[] value, int start, int count, WebSerializerOptions options)

// Append primitive values
AppendPrimitive(bool, byte, sbyte, int, etc...)
```

Deserialize
---
WebSerializer has no deserialize method because ASP.NET Core has model binding in controller.

License
---
This library is licensed under the MIT License.