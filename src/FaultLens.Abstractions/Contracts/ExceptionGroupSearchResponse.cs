using FaultLens.Abstractions.Models;

namespace FaultLens.Abstractions.Contracts;

/// <summary>
/// Represents a paginated response of grouped exception patterns.
/// </summary>
public sealed class ExceptionGroupSearchResponse
{
    /// <summary>
    /// Gets or sets the returned exception groups.
    /// </summary>
    public IReadOnlyCollection<ExceptionGroup> Items { get; set; }
        = [];

    /// <summary>
    /// Gets or sets the total matching group count.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Gets or sets the applied result limit.
    /// </summary>
    public int Limit { get; set; }
}
