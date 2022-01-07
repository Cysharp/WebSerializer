# WebSerializer

design-doc

```csharp
// Request-object to query-string
var q = WebSerializer.ToQueryString(req);

// Method argument to query-string
var q = WebSerializer.ToQueryString(new { foo, bar, baz });

// With the url-base
var url = WebSerializer.ToQueryString("https://foo/search", req);

// For Post, create form-url-encoded HttpContent
var content - WebSerializer.ToHttpContent(req);
```