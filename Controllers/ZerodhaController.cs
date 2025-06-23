using Microsoft.AspNetCore.Mvc;
using TradingApp.API.Services;

namespace TradingApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ZerodhaController : ControllerBase
{
    private readonly ZerodhaService _zerodhaService;

    public ZerodhaController(ZerodhaService zerodhaService)
    {
        _zerodhaService = zerodhaService;
    }

    [HttpGet("ltp/{symbol}")]
    public async Task<IActionResult> GetLTP(string symbol)
    {
        var price = await _zerodhaService.GetLTPAsync(symbol);
        return Ok(new { symbol, price });
    }
}
