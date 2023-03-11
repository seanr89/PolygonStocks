
using System.Net.Http.Headers;
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
        var stockName = AnsiConsole.Ask<string>("Enter a [green]stock[/]?");

        List<string> dates = new List<string>();
        for(int i = 0; i <= 5; i++)
        {
            dates.Add(DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd"));
        }

        var date = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a[green] date[/]?")
                .PageSize(5)
                .MoreChoicesText("[grey](Move up and down to reveal more dates)[/]")
                .AddChoices(dates));

        await SearchForStock(stockName, date);
        Thread.Sleep(150);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stock"></param>
    /// <returns></returns>
    async Task SearchForStock(string stock, string date)
    {
        try{
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://api.polygon.io/v1/open-close/{stock.ToUpper()}/{date}?adjusted=true&apiKey=<xxxxxxxxxx>")
            {
            };

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.Timeout = TimeSpan.FromMinutes(1);
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject
                    <StockDaily>(contentStream);

                AnsiConsole.Markup($"{model.ToString()}[/]");
            }
            else{
                AnsiConsole.Markup($"[underline red]Error![/] {httpResponseMessage.StatusCode}[/]");
            }
        }
        catch(System.InvalidOperationException e)
        {
            //TODO: but return
            return;
        }
        
    }
}
