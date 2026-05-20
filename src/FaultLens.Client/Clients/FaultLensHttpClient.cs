using System.Net.Http.Json;
using FaultLens.Abstractions.Interfaces;
using FaultLens.Abstractions.Models;
using FaultLens.Client.Exceptions;
using FaultLens.Client.Internal;
using FaultLens.Client.Results;
using Microsoft.Extensions.Logging;

namespace FaultLens.Client.Clients;

/// <summary>
/// Provides HTTP-based communication with the FaultLens ingestion API.
/// </summary>
public sealed class FaultLensHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<FaultLensHttpClient> _logger;
    private readonly int _maxRetryAttempts;
    private readonly IExceptionQueue? _queue;

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultLensHttpClient"/> class.
    /// </summary>
    /// <param name="httpClient">
    /// The underlying HTTP client.
    /// </param>
    /// <param name="logger">
    /// The logger used for SDK diagnostics.
    /// </param>
    /// <param name="maxRetryAttempts">
    /// The maximum number of retry attempts.
    /// </param>
    /// <param name="queue">
    /// The optional exception queue used for background reporting.
    /// </param>
    public FaultLensHttpClient(
        HttpClient httpClient,
        ILogger<FaultLensHttpClient> logger,
        int maxRetryAttempts = 3,
        IExceptionQueue? queue = null)
    {
        _httpClient = httpClient;
        _logger = logger;
        _maxRetryAttempts = maxRetryAttempts;
        _queue = queue;
    }

    /// <summary>
    /// Sends a single exception record to the FaultLens ingestion API.
    /// </summary>
    /// <param name="exceptionRecord">
    /// The exception record to send.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    public async Task ReportAsync(
        ExceptionRecord exceptionRecord,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(exceptionRecord);

        await FaultLensRetryPolicy.ExecuteAsync(
            async token =>
            {
                try
                {
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
                        "api/exceptions",
                        exceptionRecord,
                        token);

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogWarning(
                            "FaultLens ingestion failed with status code {StatusCode}.",
                            (int)response.StatusCode);

                        throw new FaultLensClientException(
                            $"FaultLens ingestion failed with status code {(int)response.StatusCode}.");
                    }
                }
                catch (FaultLensClientException)
                {
                    throw;
                }
                catch (Exception exception)
                {
                    _logger.LogWarning(
                        exception,
                        "Failed to send exception record to FaultLens.");

                    throw new FaultLensClientException(
                        "Failed to send exception record to FaultLens.",
                        exception);
                }
            },
            _maxRetryAttempts,
            cancellationToken);
    }

    /// <summary>
    /// Sends multiple exception records to the FaultLens ingestion API.
    /// </summary>
    /// <param name="exceptionRecords">
    /// The exception records to send.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    public async Task ReportBatchAsync(
        IReadOnlyCollection<ExceptionRecord> exceptionRecords,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(exceptionRecords);

        await FaultLensRetryPolicy.ExecuteAsync(
            async token =>
            {
                try
                {
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
                        "api/exceptions/batch",
                        exceptionRecords,
                        token);

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogWarning(
                            "FaultLens batch ingestion failed with status code {StatusCode}.",
                            (int)response.StatusCode);

                        throw new FaultLensClientException(
                            $"FaultLens batch ingestion failed with status code {(int)response.StatusCode}.");
                    }
                }
                catch (FaultLensClientException)
                {
                    throw;
                }
                catch (Exception exception)
                {
                    _logger.LogWarning(
                        exception,
                        "Failed to send exception batch to FaultLens.");

                    throw new FaultLensClientException(
                        "Failed to send exception batch to FaultLens.",
                        exception);
                }
            },
            _maxRetryAttempts,
            cancellationToken);
    }

    /// <summary>
    /// Attempts to send a single exception record to the FaultLens ingestion API without throwing SDK exceptions.
    /// </summary>
    /// <param name="exceptionRecord">
    /// The exception record to send.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// The reporting result.
    /// </returns>
    public async Task<FaultLensReportResult> TryReportAsync(
        ExceptionRecord exceptionRecord,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await ReportAsync(exceptionRecord, cancellationToken);
            return FaultLensReportResult.Success;
        }
        catch (Exception exception)
        {
            _logger.LogWarning(
                exception,
                "FaultLens exception reporting failed.");

            return new FaultLensReportResult(
                false,
                "FaultLens exception reporting failed.",
                exception);
        }
    }

    /// <summary>
    /// Attempts to send multiple exception records to the FaultLens ingestion API without throwing SDK exceptions.
    /// </summary>
    /// <param name="exceptionRecords">
    /// The exception records to send.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// The reporting result.
    /// </returns>
    public async Task<FaultLensReportResult> TryReportBatchAsync(
        IReadOnlyCollection<ExceptionRecord> exceptionRecords,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await ReportBatchAsync(exceptionRecords, cancellationToken);
            return FaultLensReportResult.Success;
        }
        catch (Exception exception)
        {
            _logger.LogWarning(
                exception,
                "FaultLens batch exception reporting failed.");

            return new FaultLensReportResult(
                false,
                "FaultLens batch exception reporting failed.",
                exception);
        }
    }

    /// <summary>
    /// Attempts to enqueue an exception record for background reporting.
    /// </summary>
    /// <param name="exceptionRecord">
    /// The exception record to enqueue.
    /// </param>
    /// <returns>
    /// True if the exception record was queued; otherwise, false.
    /// </returns>
    public bool TryQueue(ExceptionRecord exceptionRecord)
    {
        ArgumentNullException.ThrowIfNull(exceptionRecord);

        if (_queue is null)
        {
            _logger.LogWarning(
                "FaultLens exception queue is not configured.");

            return false;
        }

        bool queued = _queue.TryEnqueue(exceptionRecord);

        if (!queued)
        {
            _logger.LogWarning(
                "FaultLens exception queue is full. Exception record was not queued.");
        }

        return queued;
    }
}
