using FaultLens.Abstractions.Contracts;

namespace FaultLens.Abstractions.Interfaces;

/// <summary>
/// Defines a client contract for reporting exceptions to FaultLens.
/// </summary>
public interface IFaultLensClient
{
    /// <summary>
    /// Reports an exception ingestion request to FaultLens.
    /// </summary>
    /// <param name="request">
    /// The exception ingestion request to report.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// The ingestion response returned by FaultLens.
    /// </returns>
    Task<ExceptionIngestionResponse> ReportExceptionAsync(
        ExceptionIngestionRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reports a batch exception ingestion request to FaultLens.
    /// </summary>
    /// <param name="request">
    /// The batch exception ingestion request to report.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// The batch ingestion response returned by FaultLens.
    /// </returns>
    Task<BatchExceptionIngestionResponse> ReportExceptionBatchAsync(
        BatchExceptionIngestionRequest request,
        CancellationToken cancellationToken = default);
}
