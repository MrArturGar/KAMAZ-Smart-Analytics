using KSA_Collector;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Collector>();
    })
    .Build();

await host.RunAsync();