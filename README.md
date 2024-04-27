# Test task for Peanut Trade C# Developer position
---

## How to add new exchanges

1. To "ExchangeConfigurations" section of file ```appsettings.json``` in main WebApi project add section with the name of new exchange that contains:
  - "Url" (link to API)
  - "Key" (API access key, if one is needed)

2. In HttpClients folder of Infrastructure project implement ```IExchangeClient```.

3. Inside DependencyInjection class of Infrastructure project, register new exchange in DI container using AddHttpClient method
```cs
services.AddHttpClient<IExchangeClient, YourExchangeClient>("<Exchange Name>", options =>
{
    options.BaseAddress = new Uri(configuration.GetValue<string>("<Exchange Name>:Url") ??
        throw new ArgumentNullException("<Exchange Name> API Url", "Api url is required to access it"));

    // Read exchange API for detailed guide how to use access key
    options.DefaultRequestHeaders.Add(.....);
});
```
