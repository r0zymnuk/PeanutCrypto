using PeanutCrypto.Domain.Entities;

namespace PeanutCrypto.Application.HttpClients;

public interface IExchangeClient
{
    Task<ExchangeResponse> GetRate(string baseSymbol, string quoteSymbol);
    Task<ExchangeResponse> Exchange(string baseSymbol, string quoteSymbol, double amount);
}
