namespace TradingApp.API.Models.Requests;

public class TokenRequest
{
    public string ApiKey { get; set; }
    public string RequestToken { get; set; }
    public string Checksum { get; set; }
}
