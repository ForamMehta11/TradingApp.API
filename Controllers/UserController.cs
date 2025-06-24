using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using TradingApp.API.Helpers;

namespace TradingApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public UserController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpGet("user-profile")]
    public async Task<IActionResult> GetUserProfile([FromHeader] string apiKey, [FromHeader] string accessToken)
    {
        var url = ApiRoutes.KiteApiUrl + "/user/profile";

        // Set headers
        _httpClient.DefaultRequestHeaders.Add("X-Kite-Version", "3");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", $"{apiKey}:{accessToken}");

        // Make the request
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            return Ok(responseBody);
        }

        return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }

    [HttpGet("orders")]
    public async Task<IActionResult> GetOrders([FromHeader] string apiKey, [FromHeader] string accessToken)
    {
        var url = ApiRoutes.KiteApiUrl + "/orders";

        // Set headers
        _httpClient.DefaultRequestHeaders.Add("X-Kite-Version", "3");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", $"{apiKey}:{accessToken}");

        // Make the request
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            return Ok(responseBody);
        }

        return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }

}
