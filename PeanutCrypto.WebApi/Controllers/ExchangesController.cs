using Microsoft.AspNetCore.Mvc;
using PeanutCrypto.Application.Services;

namespace PeanutCrypto.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExchangesController(IExchangeComparerService exchangeComparer) : ControllerBase
{
    private readonly IExchangeComparerService _exchangeComparer = exchangeComparer;

    [HttpGet("rates")]
    public async Task<ActionResult> GetRates(string baseSymbol, string quoteSymbol)
    {
        var rates = await _exchangeComparer.GetRates(baseSymbol, quoteSymbol);

        if (rates is { Count: > 0})
        {
            return Ok(rates);       
        }

        return NotFound("Sorry, but there are currently no available exchange APIs");
    }

    [HttpGet("estimates")]
    public async Task<ActionResult> GetBestEstimated(string baseSymbol, string quoteSymbol, double amount)
    {
        var estimate = await _exchangeComparer.GetBestExchange(baseSymbol, quoteSymbol, amount);

        if (estimate == null)
        {
            return NotFound("Sorry, but there are currently no available exchange APIs");
        }

        return Ok(estimate);
    }
}
