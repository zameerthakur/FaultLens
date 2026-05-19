using FaultLens.Abstractions.Models;

namespace FaultLens.Abstractions.Contracts;

/// <summary>
/// Represents the response returned from an exception search operation.
/// </summary>
public sealed class ExceptionSearchResponse
{
    /// <summary>
    /// Gets or sets the exception records returned by the search operation.
    /// </summary>
    public IReadOnlyCollection<ExceptionRecord> Items { get; set; } = [];

    /// <summary>
    /// Gets or sets the total number of records matching the search criteria.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of records requested.
    /// </summary>
    public int Limit { get; set; }
}
