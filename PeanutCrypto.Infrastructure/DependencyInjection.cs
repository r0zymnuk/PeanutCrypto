using Microsoft.Extensions.Configuration;
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

        services.AddExchangeClient<BinanceClient>(configuration);

        services.AddExchangeClient<KucoinClient>(configuration);

        /* another for registering exchange client
         * 
        services.AddHttpClient<IExchangeClient, YourExchangeClient>("<Exchange Name>", options =>
        {
            options.BaseAddress = new Uri(configuration.GetValue<string>("<Exchange Name>:Url") ??
                throw new ArgumentNullException("<Exchange Name> API Url", "Api url is required to access it"));

            // Read your exchange's API for detailed guide how to use access key
            options.DefaultRequestHeaders.Add(.....);
        });
        */
        services.AddScoped<IExchangeComparerService, ExchangeComparerService>();
    }

    private static void AddExchangeClient<ExchangeClient>(this IServiceCollection services, IConfiguration configuration) where ExchangeClient : class, IExchangeClient
    {
        string exchangeName = typeof(ExchangeClient).Name[..^6];

        var baseAddress = configuration.GetValue<string>($"{exchangeName}:Url");
        if (string.IsNullOrWhiteSpace(baseAddress))
        {
            Console.WriteLine($"{exchangeName} API Url is not found! Api url is required to access it");
            return;
        }

        services.AddHttpClient<IExchangeClient, ExchangeClient>(exchangeName, options =>
        {
            options.BaseAddress = new Uri(baseAddress);
        }
        );
    }
}
