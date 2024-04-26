using PeanutCrypto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeanutCrypto.Application.HttpClients;

public interface IExchangeClient
{
    Task<ExchangeResponse> GetRate(string baseSymbol, string quoteSymbol);
    Task<ExchangeResponse> Exchange(string baseSymbol, string quoteSymbol, decimal amount);
}
