using PeanutCrypto.Application.HttpClients;
using PeanutCrypto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace PeanutCrypto.Infrastructure.HttpClients;

public class BinanceClient(HttpClient httpClient) : IExchangeClient
{
    public Task<ExchangeResponse> Exchange(string baseSymbol, string quoteSymbol, decimal amount)
    {
        httpClient.GetAsync("");

        throw new NotImplementedException();
    }

    public async Task<ExchangeResponse> GetRate(string baseSymbol, string quoteSymbol)
    {
        bool inverted = false;
        var response = await httpClient.GetAsync($"api/v3/ticker/price?symbol={baseSymbol}{quoteSymbol}");
        if (!response.IsSuccessStatusCode)
        {
            response = await httpClient.GetAsync($"api/v3/ticker/price?symbol={quoteSymbol}{baseSymbol}");
            inverted = true;
        }
        if (!response.IsSuccessStatusCode)
        {
            throw new ArgumentException("Quote or base symbol is not supported by Binance API");
        }

        var content = await response.Content.ReadAsStringAsync();
        var price = Decimal.Parse(JsonSerializer.Deserialize<JsonElement>(content).GetProperty("price").GetString() ?? throw new ArgumentException("Invalid json"));

        if (inverted)
        {
            price = (decimal)Math.Pow((double)price, -1);
        }

        return new ExchangeResponse
        {
            BaseSymbol = baseSymbol,
            QuoteSymbol = quoteSymbol,
            ExchangeName = "Binance",
            Rate = price,
        };
    }
}
