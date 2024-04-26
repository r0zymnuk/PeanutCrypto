using PeanutCrypto.Application.HttpClients;
using PeanutCrypto.Domain.Entities;
using System.Text.Json;

namespace PeanutCrypto.Infrastructure.HttpClients;

public class BinanceClient(HttpClient httpClient) : IExchangeClient
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
        var price = double.Parse(JsonSerializer.Deserialize<JsonElement>(content).GetProperty("price").GetString() ?? throw new ArgumentException("Invalid json"));

        if (inverted)
        {
            price = Math.Pow(price, -1);
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
