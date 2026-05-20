using FaultLens.Abstractions.Interfaces;
using FaultLens.Abstractions.Models;
using FaultLens.Client.Clients;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FaultLens.Client.Services;

/// <summary>
/// Provides background delivery for queued exception records.
/// </summary>
public sealed class FaultLensQueueFlushService : BackgroundService
{
    private readonly IExceptionQueue _queue;
    private readonly FaultLensHttpClient _client;
    private readonly ILogger<FaultLensQueueFlushService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultLensQueueFlushService"/> class.
    /// </summary>
    public FaultLensQueueFlushService(
        IExceptionQueue queue,
        FaultLensHttpClient client,
        ILogger<FaultLensQueueFlushService> logger)
    {
        _queue = queue;
        _client = client;
        _logger = logger;
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await FlushAsync(stoppingToken);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(
                    exception,
                    "FaultLens queue flush failed.");
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }

    private async Task FlushAsync(CancellationToken cancellationToken)
    {
        var items = new List<ExceptionRecord>();

        while (items.Count < 100 && _queue.TryDequeue(out ExceptionRecord? exceptionRecord))
        {
            if (exceptionRecord is not null)
            {
                items.Add(exceptionRecord);
            }
        }

        if (items.Count == 0)
        {
            return;
        }

        await _client.TryReportBatchAsync(items, cancellationToken);
    }
}
