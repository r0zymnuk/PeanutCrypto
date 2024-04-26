using Microsoft.AspNetCore.Http;
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

        return Ok(rates);   
    }
}
