using Spectre.Console;

AnsiConsole.Markup("[underline red]Hello[/] World!");

if (!AnsiConsole.Confirm("Run prompt example?"))
{
    AnsiConsole.MarkupLine("Ok... :(");
    return false;
}

return true;