using FaultLens.Abstractions.Interfaces;
using FaultLens.Client.Clients;
using FaultLens.Client.Options;
using FaultLens.Client.Queues;
using FaultLens.Client.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FaultLens.Client.Extensions;

/// <summary>
/// Provides dependency injection registration extensions for FaultLens client services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers FaultLens client services.
    /// </summary>
    public static IServiceCollection AddFaultLensClient(
        this IServiceCollection services,
        Action<FaultLensClientOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configure);

        var options = new FaultLensClientOptions
        {
            ServerUrl = string.Empty
        };

        configure(options);

        if (string.IsNullOrWhiteSpace(options.ServerUrl))
        {
            throw new ArgumentException(
                "FaultLens server URL is required.",
                nameof(configure));
        }

        services.AddSingleton(options);

        services.AddSingleton<IExceptionQueue>(
            _ => new InMemoryExceptionQueue(options.BufferCapacity));

        services.AddHttpClient("FaultLens", client =>
        {
            client.BaseAddress = new Uri(options.ServerUrl);
            client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);

            if (!string.IsNullOrWhiteSpace(options.ApiKey))
            {
                client.DefaultRequestHeaders.Add(
                    "X-FaultLens-ApiKey",
                    options.ApiKey);
            }
        });

        services.AddTransient(provider =>
        {
            var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
            var logger = provider.GetRequiredService<ILogger<FaultLensHttpClient>>();
            var queue = provider.GetService<IExceptionQueue>();

            return new FaultLensHttpClient(
                httpClientFactory.CreateClient("FaultLens"),
                logger,
                options.MaxRetryAttempts,
                queue);
        });

        if (options.EnableAutomaticReporting)
        {
            services.AddHostedService<FaultLensQueueFlushService>();
        }

        return services;
    }
}
