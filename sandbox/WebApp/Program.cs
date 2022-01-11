using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using ZLogger;

var builder = WebApplication.CreateBuilder();
builder.Logging.ClearProviders();
builder.Logging.AddZLoggerConsole();

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

app.Run();

// ----------

public class PagingData
{
    public string? SortBy { get; init; }
    public SortDirection SortDirection { get; init; }
    public int CurrentPage { get; init; } = 1;
}


public enum SortDirection
{
    Default,
    Asc,
    Desc
}

[Route("[controller]")]
public class ProductsController : Controller
{
    readonly ILogger<ProductsController> logger;

    public ProductsController(ILogger<ProductsController> logger)
    {
        this.logger = logger;
    }

    public void Get(int[] foo)
    {
        //return $"SortBy:{pageData.SortBy}, " + $"SortDirection:{pageData.SortDirection}, CurrentPage:{pageData.CurrentPage}";
        foreach (var item in foo)
        {
            logger.ZLogInformation("ITEM:{0}", item);
        }
    }

    //public string Post(PagingData pageData)
    //{
    //    return $"SortBy:{pageData.SortBy}, " + $"SortDirection:{pageData.SortDirection}, CurrentPage:{pageData.CurrentPage}";
    //    //logger.ZLogInformation("SortBy:{0} SortDirection:{1} CurrentPage:{2}", pageData.SortBy, pageData.SortDirection, pageData.CurrentPage);
    //}
}
