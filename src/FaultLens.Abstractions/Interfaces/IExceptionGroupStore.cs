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
}
