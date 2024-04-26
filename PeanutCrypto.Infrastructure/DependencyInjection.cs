﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PeanutCrypto.Application.HttpClients;
using PeanutCrypto.Application.Services;
using PeanutCrypto.Infrastructure.HttpClients;
using PeanutCrypto.Infrastructure.Services;

namespace PeanutCrypto.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        configuration = configuration.GetSection("ExchangeConfigurations") ??
            throw new ArgumentNullException(
                nameof(configuration),
                "Exchage configrations section is required to have access to exchange data");

        services.AddHttpClient<IExchangeClient, BinanceClient>("Binance", options =>
        {
            options.BaseAddress = new Uri(configuration.GetValue<string>("Binance:Url") ?? 
                throw new ArgumentNullException("Binance API Url", "Api url is required to access it"));
        });

        services.AddHttpClient<IExchangeClient, KucoinClient>("Kucoin", options =>
        {
            options.BaseAddress = new Uri(configuration.GetValue<string>("Kucoin:Url") ??
                throw new ArgumentNullException("Kucoin API Url", "Api url is required to access it"));
        });

        services.AddScoped<IExchangeComparerService, ExchangeComparerService>();
    }
}