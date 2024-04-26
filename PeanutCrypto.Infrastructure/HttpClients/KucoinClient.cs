using PeanutCrypto.Application.HttpClients;
using PeanutCrypto.Domain.Entities;
using System.Text.Json;

namespace PeanutCrypto.Infrastructure.HttpClients;

public class KucoinClient(HttpClient httpClient) : IExchangeClient
{
    public async Task<ExchangeResponse> Exchange(string baseSymbol, string quoteSymbol, double amount)
    {
        var exchangeResponse = await GetRate(baseSymbol, quoteSymbol);

        exchangeResponse.InputAmount = amount;
        exchangeResponse.OutputAmount = amount * exchangeResponse.Rate;

        return exchangeResponse;
    }

    public async Task<ExchangeResponse> GetRate(string baseSymbol, string quoteSymbol)
    {
        bool inverted = false;
        var response = await httpClient.GetAsync($"api/v1/market/stats?symbol={baseSymbol}-{quoteSymbol}");

        var content = await response.Content.ReadAsStringAsync();
        var priceProperty = JsonSerializer.Deserialize<JsonElement>(content)
            .GetProperty("data")
            .GetProperty("last");
        if (priceProperty.ValueKind == JsonValueKind.Null)
        {
            response = await httpClient.GetAsync($"api/v1/market/stats?symbol={quoteSymbol}-{baseSymbol}");
            inverted = true;

            content = await response.Content.ReadAsStringAsync();
            priceProperty = JsonSerializer.Deserialize<JsonElement>(content)
                .GetProperty("data")
                .GetProperty("last");
        }

        var price = double.Parse(priceProperty.GetString()
            ?? throw new ArgumentException("Invalid json"));

        if (inverted)
        {
            price = Math.Pow(price, -1);
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