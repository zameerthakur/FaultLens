using FaultLens.Abstractions.Contracts;
using FaultLens.Abstractions.Models;

namespace FaultLens.Abstractions.Interfaces;

/// <summary>
/// Defines a contract for storing and querying exception records.
/// </summary>
public interface IExceptionStore
{
    /// <summary>
    /// Saves the specified exception record.
    /// </summary>
    /// <param name="exceptionRecord">
    /// The exception record to save.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous save operation.
    /// </returns>
    Task SaveAsync(
        ExceptionRecord exceptionRecord,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches exception records using the specified request.
    /// </summary>
    /// <param name="request">
    /// The exception search request.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// The exception search response.
    /// </returns>
    Task<ExceptionSearchResponse> SearchAsync(
        ExceptionSearchRequest request,
        CancellationToken cancellationToken = default);
}
