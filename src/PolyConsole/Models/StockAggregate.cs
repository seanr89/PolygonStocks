
namespace PolygonConsole.Models;

public record StockAggregate
{
    public bool adjusted { get; set; }
    public string next_url { get; set; }
    public int queryCount { get; set; }
    public string request_id { get; set; }
    public List<Result> results { get; set; }
    public int resultsCount { get; set; }
    public string status { get; set; }
    public string ticker { get; set; }
}

public record Result
{
    public double c { get; set; }
    public double h { get; set; }
    public double l { get; set; }
    public double n { get; set; }
    public double o { get; set; }
    public object t { get; set; }
    public double v { get; set; }
    public double vw { get; set; }
}