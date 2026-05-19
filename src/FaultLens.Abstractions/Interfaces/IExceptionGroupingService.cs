using FaultLens.Abstractions.Models;

namespace FaultLens.Abstractions.Interfaces;

/// <summary>
/// Defines a contract for grouping related exception records.
/// </summary>
public interface IExceptionGroupingService
{
    /// <summary>
    /// Groups the specified exception record into an operational exception group.
    /// </summary>
    /// <param name="exceptionRecord">
    /// The exception record to group.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// The resulting grouped exception pattern.
    /// </returns>
    Task<ExceptionGroup> GroupAsync(
        ExceptionRecord exceptionRecord,
        CancellationToken cancellationToken = default);
}
