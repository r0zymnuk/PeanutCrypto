using PeanutCrypto.Domain.Entities;

namespace PeanutCrypto.Application.Services;

public interface IExchangeComparerService
{
    Task<List<ExchangeResponse>> GetRates(string baseSymbol, string quoteSymbol);
    Task<ExchangeResponse?> GetBestExchange(string baseSymbol, string quoteSymbol, double amount);
}
