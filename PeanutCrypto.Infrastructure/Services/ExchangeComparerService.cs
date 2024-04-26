using Microsoft.Extensions.DependencyInjection;
using PeanutCrypto.Application.HttpClients;
using PeanutCrypto.Application.Services;
using PeanutCrypto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeanutCrypto.Infrastructure.Services;

public class ExchangeComparerService : IExchangeComparerService
{
    private readonly IEnumerable<IExchangeClient> _exchangeClients;

    public ExchangeComparerService(IServiceProvider serviceProvider)
    {
        _exchangeClients = serviceProvider.GetServices<IExchangeClient>();
    }
    public Task<List<object>> GetBestExchange(string baseSymbol, string quoteSymbol, decimal amount)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ExchangeResponse>> GetRates(string baseSymbol, string quoteSymbol)
    {
        var rates = _exchangeClients.Select(e => e.GetRate(baseSymbol, quoteSymbol)).ToList();

        await Task.WhenAll(rates);

        return rates.Select(t => t.Result).ToList();
    }
}
