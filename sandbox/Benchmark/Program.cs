using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using Refit;

var config = ManualConfig.CreateMinimumViable()
    .AddDiagnoser(MemoryDiagnoser.Default)
    .AddJob(Job.ShortRun.WithIterationCount(1).WithWarmupCount(1));

BenchmarkDotNet.Running.BenchmarkRunner.Run<PostBenchmark>(config);


public class GetBenchmark : IDisposable
{
    HttpClient client;
    IMinimumAPI api;
    public GetBenchmark()
    {
        client = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };
        api = RestService.For<IMinimumAPI>(client);
    }

    [Benchmark]
    public async Task Refit()
    {
        await api.Get("foo", SortDirection.Asc, 10);
    }

    [Benchmark]
    public async Task WebSerializer()
    {
        var url = Cysharp.Web.WebSerializer.ToQueryString("/products", new { sortBy = "foo", sortDirection = SortDirection.Asc, currentPage = 10 });
        await client.GetAsync(url);
    }

    public void Dispose()
    {
        client.Dispose();
    }
}

public class PostBenchmark : IDisposable
{
    HttpClient client;
    IMinimumAPI api;
    public PostBenchmark()
    {
        client = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };
        api = RestService.For<IMinimumAPI>(client);
    }

    [Benchmark]
    public async Task Refit()
    {
        await api.Post("foo", SortDirection.Asc, 10);
    }

    [Benchmark]
    public async Task WebSerializer()
    {
        var content = Cysharp.Web.WebSerializer.ToHttpContent(new { sortBy = "foo", sortDirection = SortDirection.Asc, currentPage = 10 });
        await client.PostAsync("/products", content);
    }

    [Benchmark]
    public async Task FormUrlEncodedContent()
    {
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            {"sortBy", "foo" },
            {"sortDirection", "asc" },
            {"currentPage", "10" },
        });
        await client.PostAsync("/products", content);
    }

    public void Dispose()
    {
        client.Dispose();
    }
}


public interface IMinimumAPI
{
    [Get("/products")]
    Task Get(string? sortBy, SortDirection sortDirection, int currentPage);

    [Post("/products")]
    Task Post(string? sortBy, SortDirection sortDirection, int currentPage);
}

public enum SortDirection
{
    Default,
    Asc,
    Desc
}