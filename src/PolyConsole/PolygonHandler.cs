
using System.Net.Http.Headers;
using Application;
using Newtonsoft.Json;
using PolygonConsole.Models;
using Spectre.Console;

namespace PolygonConsole;

public class PolygonHandler
{
    private static string[] tickers = {
        "MSFT",
        "AAPL",
        "AMZN",
        "GOOG",
        "GOOGL",
        "FB",
        "INTC",
        "KO",
        "NVDA",
    };
    private readonly IHttpClientFactory _httpClientFactory;

    public PolygonHandler(IHttpClientFactory httpClientFactory) =>
        _httpClientFactory = httpClientFactory;


    public async Task Run(){
        //Console.WriteLine("Running");
        //var stockName = AnsiConsole.Ask<string>("Enter a [green]stock[/]?");

        List<string> dates = new List<string>();
        var weekDays = Utils.WeekDays(DateTime.Now, 5);
        //Console.WriteLine($"day {weekDays.ToArray()[0].ToLongDateString()}");
        // if(weekDays.Any())
        //     Console.WriteLine($"weekDays");

        foreach(var day in weekDays)
        {
            //Console.WriteLine($"day {day.ToLongDateString()}");
            dates.Add(day.ToString("yyyy-MM-dd"));
        }

        var ticker = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a[green] stock[/]?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more stocks)[/]")
                .AddChoices(tickers));

        var date = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a[green] date[/]?")
                .PageSize(5)
                .MoreChoicesText("[grey](Move up and down to reveal more dates)[/]")
                .AddChoices(dates));

        await SearchForStock(ticker, date);
        Thread.Sleep(150);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stock"></param>
    /// <returns></returns>
    async Task SearchForStock(string stock, string date)
    {
        Console.WriteLine("SearchForStock");
        try{
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://api.polygon.io/v1/open-close/{stock.ToUpper()}/{date}?adjusted=true&apiKey=<xxxxxxxxxx>")
            {
            };

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.Timeout = TimeSpan.FromMinutes(1);
            var resp = await httpClient.SendAsync(httpRequestMessage);

            if (resp.IsSuccessStatusCode)
            {
                Console.WriteLine("If");
                var contentStream = await resp.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject
                    <StockDaily>(contentStream);

                AnsiConsole.Markup($"{model.ToString()}[/]");
            }
            // else if(httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized){
            //     Console.WriteLine("Else If");
            //     AnsiConsole.Markup($"[underline red]Error![/] Unknown Error[/]");
            // }
            else{
                Console.WriteLine($"Else {resp.StatusCode}");
                AnsiConsole.Write(new Markup($"Error![/] message: {resp.StatusCode} [/]"));
            }
        }catch(System.InvalidOperationException e)
        {
            //TODO: but return
            // AnsiConsole.Markup($"[underline red]Exception![/] {e.Message}[/]");
            Console.WriteLine($"[underline red]Exception![/] {e.Message}[/]");
            return;
        }
    }
}
