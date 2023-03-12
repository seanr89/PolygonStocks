using Spectre.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Application;
using Microsoft.Extensions.DependencyInjection;
using PolygonConsole;
using Microsoft.Extensions.Logging;

// Build a config object, using env vars and JSON providers.
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

Console.Clear();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddApplication();
        // services.AddHttpClient("PolyClient", config =>
        // {
        //     config.BaseAddress = new Uri("https://api.polygon.io/v2/aggs/ticker/");
        //     config.Timeout = new TimeSpan(0, 0, 30);
        //     config.DefaultRequestHeaders.Clear();
        // });
        services.AddHttpClient();
        services.AddSingleton<PolygonHandler>();
    })
    .ConfigureLogging((context, logging) => {
        logging.AddConfiguration(context.Configuration.GetSection("Logging"));
        // logging.ClearProviders();
        // logging.AddConsole();
    });


var app = host.Build();

AnsiConsole.Write(
    new FigletText("Polygon Searcher")
        .LeftAligned()
        .Color(Color.Red));

var entry = app.Services.GetService<PolygonHandler>();
while(true)
{
    if(entry == null)
        Environment.Exit(1);
    bool result = await entry.Run();
    if(!result)
        Environment.Exit(100);
}