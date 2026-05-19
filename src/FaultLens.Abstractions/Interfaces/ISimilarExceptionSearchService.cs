using FaultLens.Abstractions.Models;

namespace FaultLens.Abstractions.Interfaces;

/// <summary>
/// Defines a contract for finding semantically similar exception records.
/// </summary>
public interface ISimilarExceptionSearchService
{
    /// <summary>
    /// Finds exceptions that are semantically similar to the specified exception record.
    /// </summary>
    /// <param name="exceptionRecord">
    /// The source exception record used for similarity search.
    /// </param>
    /// <param name="limit">
    /// The maximum number of similar matches to return.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// A collection of similar exception matches.
    /// </returns>
    Task<IReadOnlyCollection<SimilarExceptionMatch>> FindSimilarAsync(
        ExceptionRecord exceptionRecord,
        int limit = 10,
        CancellationToken cancellationToken = default);
}
