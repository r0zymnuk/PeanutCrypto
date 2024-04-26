using PeanutCrypto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeanutCrypto.Application.Services;

public interface IExchangeComparerService
{
    Task<List<ExchangeResponse>> GetRates(string baseSymbol, string quoteSymbol);
    Task<List<object>> GetBestExchange(string baseSymbol, string quoteSymbol, decimal amount);
}
