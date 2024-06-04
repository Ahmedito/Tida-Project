using Discord.Interactions;
using Discord.WebSocket;
using Tida.Services;
using Tida.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        // plik konfiiguracji (token bota etc)
        config.AddYamlFile("_config.yml", false);
    })
    .ConfigureServices(services =>
    {
        services.AddSingleton<DiscordSocketClient>();       
        services.AddSingleton<InteractionService>();       
        services.AddHostedService<InteractionHandlingService>();
        services.AddHostedService<DiscordStartupService>(); 
    })
    .Build();

await host.RunAsync();