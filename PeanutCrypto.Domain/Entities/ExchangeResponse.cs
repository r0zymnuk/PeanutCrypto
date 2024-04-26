using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeanutCrypto.Domain.Entities;

public class ExchangeResponse
{
    public string ExchangeName { get; set; }
    public string BaseSymbol { get; set; }
    public string QuoteSymbol { get; set; }
    public decimal InputAmount { get; set; }
    public decimal OutputAmount { get; set; }
    public decimal Rate { get; set; }
}
