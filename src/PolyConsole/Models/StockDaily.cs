namespace PolygonConsole.Models;

public record StockDaily
{
    public double afterHour { get; set; }
    public double close { get; set; }
    public string from { get; set; }
    public double high { get; set; }
    public double low { get; set; }
    public double open { get; set; }
    public double preMarket { get; set; }
    public string status { get; set; }
    public string symbol { get; set; }
    public long volume { get; set; }


    #region overrides

    public override string? ToString()
    {
        return String.Format("Stock - {0} - Opening value:{1} and Closed:{2} with a High of:{3}",symbol,open,close,high);
    }

    #endregion
}