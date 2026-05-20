using FaultLens.Client.Clients;
using FaultLens.Client.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace FaultLens.Client.Factories;

/// <summary>
/// Creates configured FaultLens HTTP client instances.
/// </summary>
public static class FaultLensClientFactory
{
    /// <summary>
    /// Creates a configured FaultLens HTTP client.
    /// </summary>
    public static FaultLensHttpClient Create(
        FaultLensClientOptions options,
        ILogger<FaultLensHttpClient>? logger = null)
    {
        ArgumentNullException.ThrowIfNull(options);

        if (string.IsNullOrWhiteSpace(options.ServerUrl))
        {
            throw new ArgumentException(
                "FaultLens server URL is required.",
                nameof(options));
        }

        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(options.ServerUrl),
            Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds)
        };

        if (!string.IsNullOrWhiteSpace(options.ApiKey))
        {
            httpClient.DefaultRequestHeaders.Add(
                "X-FaultLens-ApiKey",
                options.ApiKey);
        }

        return new FaultLensHttpClient(
            httpClient,
            logger ?? NullLogger<FaultLensHttpClient>.Instance,
            options.MaxRetryAttempts);
    }
}
