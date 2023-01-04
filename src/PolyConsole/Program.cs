using Spectre.Console;

// Build a config object, using env vars and JSON providers.
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

AnsiConsole.Markup("[underline red]Hello[/] World!");

if (!AnsiConsole.Confirm("Run prompt example?"))
{
    AnsiConsole.MarkupLine("Ok... :(");
    return false;
}

return true;