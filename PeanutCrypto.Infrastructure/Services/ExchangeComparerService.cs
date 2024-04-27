using Microsoft.Extensions.DependencyInjection;
using PeanutCrypto.Application.HttpClients;
using PeanutCrypto.Application.Services;
using PeanutCrypto.Domain.Entities;

namespace PeanutCrypto.Infrastructure.Services;

public class ExchangeComparerService : IExchangeComparerService
{
    private readonly IEnumerable<IExchangeClient> _exchangeClients;

    public ExchangeComparerService(IServiceProvider serviceProvider)
    {
        _exchangeClients = serviceProvider.GetServices<IExchangeClient>();
    }
    public async Task<ExchangeResponse?> GetBestExchange(string baseSymbol, string quoteSymbol, double amount)
    {
        var estimateTasks = _exchangeClients
            .Select(e => e.Exchange(baseSymbol, quoteSymbol, amount))
            .ToList();

        await Task.WhenAll(estimateTasks);

        var estimates = estimateTasks.Select(t => t.Result)
            .OrderByDescending(e => e.OutputAmount)
            .ToList();
            

        return estimates.FirstOrDefault();
    }

    public async Task<List<ExchangeResponse>> GetRates(string baseSymbol, string quoteSymbol)
    {
        var rateTasks = _exchangeClients
            .Select(e => e.GetRate(baseSymbol, quoteSymbol))
            .ToList();

        await Task.WhenAll(rateTasks);

        return rateTasks.Select(t => t.Result).ToList();
    }
}
