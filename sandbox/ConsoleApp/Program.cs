using Refit;

var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };
//var api = RestService.For<IMinimumAPI>(client);
//await api.Get(10, "octocat");


var content = Cysharp.Web.WebSerializer.ToHttpContent(new { sortBy = "じゃがいも だより", sortDirection = SortDirection.Asc, currentPage = 10 });
var foo = await client.PostAsync("/products", content);

var response = await foo.Content.ReadAsStringAsync();
Console.WriteLine(response);


public enum SortDirection
{
    Default,
    Asc,
    Desc
}