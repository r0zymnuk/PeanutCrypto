﻿namespace PeanutCrypto.Domain.Entities;

public class ExchangeResponse
{
    public string ExchangeName { get; set; }
    public string BaseSymbol { get; set; }
    public string QuoteSymbol { get; set; }
    public double InputAmount { get; set; } = 0;
    public double OutputAmount { get; set; } = 0;
    public double Rate { get; set; }
}
