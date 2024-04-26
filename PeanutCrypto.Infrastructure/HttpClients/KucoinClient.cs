using PeanutCrypto.Application.HttpClients;
using PeanutCrypto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PeanutCrypto.Infrastructure.HttpClients;

public class KucoinClient(HttpClient httpClient) : IExchangeClient
{
    public Task<ExchangeResponse> Exchange(string baseSymbol, string quoteSymbol, decimal amount)
    {
        throw new NotImplementedException();
    }

    public async Task<ExchangeResponse> GetRate(string baseSymbol, string quoteSymbol)
    {
        bool inverted = false;
        var response = await httpClient.GetAsync($"api/v1/market/stats?symbol={baseSymbol}-{quoteSymbol}");

        var content = await response.Content.ReadAsStringAsync();
        var priceProperty = JsonSerializer.Deserialize<JsonElement>(content).GetProperty("data").GetProperty("last");
        if (priceProperty.ValueKind == JsonValueKind.Null)
        {
            response = await httpClient.GetAsync($"api/v1/market/stats?symbol={quoteSymbol}-{baseSymbol}");
            inverted = true;
        }

        var price = Decimal.Parse(priceProperty.GetString() ?? throw new ArgumentException("Invalid json"));
        if (inverted)
        {
            price = (decimal)Math.Pow((double)price, -1);
        }

        return new ExchangeResponse
        {
            BaseSymbol = baseSymbol,
            QuoteSymbol = quoteSymbol,
            ExchangeName = "Kucoin",
            Rate = price,
        };
    }
}