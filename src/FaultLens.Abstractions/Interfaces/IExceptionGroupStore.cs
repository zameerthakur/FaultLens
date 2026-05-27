using FaultLens.Abstractions.Contracts;
using FaultLens.Abstractions.Models;

namespace FaultLens.Abstractions.Interfaces;

/// <summary>
/// Defines a contract for storing and querying grouped exception patterns.
/// </summary>
public interface IExceptionGroupStore
{
    /// <summary>
    /// Saves the specified exception group.
    /// </summary>
    /// <param name="exceptionGroup">
    /// The exception group to save.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous save operation.
    /// </returns>
    Task SaveAsync(
        ExceptionGroup exceptionGroup,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves a new exception group or updates an existing group with the same fingerprint.
    /// </summary>
    /// <param name="exceptionGroup">
    /// The exception group to save or update.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// The saved or updated exception group.
    /// </returns>
    Task<ExceptionGroup> UpsertAsync(
        ExceptionGroup exceptionGroup,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an exception group by its fingerprint.
    /// </summary>
    /// <param name="fingerprint">
    /// The stable fingerprint used to locate the exception group.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// The matching exception group when found; otherwise, null.
    /// </returns>
    Task<ExceptionGroup?> GetByFingerprintAsync(
        string fingerprint,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches grouped exception patterns.
    /// </summary>
    /// <param name="limit">
    /// The maximum number of groups to return.
    /// </param>
    /// <param name="skip">
    /// The number of groups to skip.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// The matching grouped exception patterns.
    /// </returns>
    Task<ExceptionGroupSearchResponse> SearchAsync(
        int limit = 100,
        int skip = 0,
        CancellationToken cancellationToken = default);
}
