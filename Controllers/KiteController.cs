using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using TradingApp.API.Helpers;
using TradingApp.API.Models.Requests;

namespace TradingApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KiteController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public KiteController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpPost("generate-token")]
    public async Task<IActionResult> GenerateToken([FromBody] TokenRequest request)
    {
        var url = ApiRoutes.KiteApiUrl + "/session/token";
        var content = new StringContent(
            $"api_key={request.ApiKey}&request_token={request.RequestToken}&checksum={request.Checksum}",
            Encoding.UTF8,
            "application/x-www-form-urlencoded"
        );

        _httpClient.DefaultRequestHeaders.Add("X-Kite-Version", "3");

        var response = await _httpClient.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            return Ok(responseBody);
        }

        return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }

    
    [HttpDelete("invalidate-token")]
    public async Task<IActionResult> InvalidateToken([FromQuery] string apiKey, [FromQuery] string accessToken)
    {
        var url = $"{ApiRoutes.KiteApiUrl}/session/token?api_key={apiKey}&access_token={accessToken}";

        // Set headers
        _httpClient.DefaultRequestHeaders.Add("X-Kite-Version", "3");

        // Make the DELETE request
        var response = await _httpClient.DeleteAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            return Ok(responseBody);
        }

        return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }
}
