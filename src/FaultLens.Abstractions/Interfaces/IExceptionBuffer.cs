using FaultLens.Abstractions.Contracts;

namespace FaultLens.Abstractions.Interfaces;

/// <summary>
/// Defines a contract for buffering exception ingestion requests locally.
/// </summary>
public interface IExceptionBuffer
{
    /// <summary>
    /// Adds an exception ingestion request to the local buffer.
    /// </summary>
    Task AddAsync(
        ExceptionIngestionRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads buffered exception ingestion requests.
    /// </summary>
    Task<IReadOnlyCollection<ExceptionIngestionRequest>> ReadAsync(
        int limit,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes buffered exception ingestion requests after successful delivery.
    /// </summary>
    Task RemoveAsync(
        IReadOnlyCollection<Guid> exceptionIds,
        CancellationToken cancellationToken = default);
}
