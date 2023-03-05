﻿using Spectre.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Application;
using Microsoft.Extensions.DependencyInjection;

// Build a config object, using env vars and JSON providers.
// IConfiguration config = new ConfigurationBuilder()
//     .AddJsonFile("appsettings.json")
//     .AddEnvironmentVariables()
//     .Build();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddApplication();
        services.AddHttpClient("PolyClient", config =>
    {
        config.BaseAddress = new Uri("https://localhost:5001/api/");
        config.Timeout = new TimeSpan(0, 0, 30);
        config.DefaultRequestHeaders.Clear();
    });
        // services.AddSingleton<MyInjectedClass>();
        // services.AddSingleton<Main>();
        // services.AddHostedService<MyHostedService>();
    });

var app = host.Build();

AnsiConsole.Write(
    new FigletText("Polygon Searcher")
        .LeftAligned()
        .Color(Color.Red));

// var logger = host.Services.GetRequiredService<ILogger<Program>>();
// logger.LogDebug("Host created.");
// host.Services.GetService<MyInjectedClass>().Execute().Wait();

// AnsiConsole.Markup("[underline red]Hello[/] World!");
AnsiConsole.Write(new Markup("[bold yellow]Hello[/] [red]World![/]"));

// if (!AnsiConsole.Confirm("Run prompt example?"))
// {
//     AnsiConsole.MarkupLine("Ok... :(");
//     return;
// }

//var name = AnsiConsole.Ask<string>("What's your [green]name[/]?");
// return name;

while(true)
{

}

AnsiConsole.MarkupLine("App complete!! :)");

Environment.Exit(0);