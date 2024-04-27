# Test task for Peanut Trade C# Developer position
---

## How to add new exchanges

In Infrastructure project create implementation of ```IExchangeClient``` following the naming like ```<ExchangeName>Client```.
To "ExchangeConfigurations" section of file ```appsettings.json``` in main WebApi project add section with the name of new exchange that contains:
- "Url" (link to API)
- "Key" (API access key, if one is needed)
