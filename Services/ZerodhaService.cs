namespace TradingApp.API.Services;

using System.Diagnostics;

public class ZerodhaService
{
    private readonly IConfiguration _config;

    public ZerodhaService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<string> GetLTPAsync(string symbol)
    {
        string scriptPath = _config["PythonScriptPath"];
        var start = new ProcessStartInfo
        {
            FileName = "python",
            Arguments = $"{scriptPath} ltp {symbol}",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(start);
        string result = await process.StandardOutput.ReadToEndAsync();
        return result;
    }
}

