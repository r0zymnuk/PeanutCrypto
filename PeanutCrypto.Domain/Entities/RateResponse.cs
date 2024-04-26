using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeanutCrypto.Domain.Entities;

public class RateResponse
{
    public string ExchangeName { get; set; }
    public string BaseSymbol { get; set; }
    public string QuoteSymbol { get; set; }
    //public decimal rate
}
