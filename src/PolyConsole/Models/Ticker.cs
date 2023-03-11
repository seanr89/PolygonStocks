namespace PolygonConsole.Models;

public record Ticker
{
    public string Name { get; set; }


    #region overrides

    public override string? ToString()
    {
        //return base.ToString();
        return String.Format("Name:{0}}",Name);
    }

    #endregion
}