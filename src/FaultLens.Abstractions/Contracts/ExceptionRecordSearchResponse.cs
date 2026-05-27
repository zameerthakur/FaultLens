using FaultLens.Abstractions.Models;

namespace FaultLens.Abstractions.Contracts;

/// <summary>
/// Represents a paginated response of exception records.
/// </summary>
public sealed class ExceptionRecordSearchResponse
{
    /// <summary>
    /// Gets or sets the returned exception records.
    /// </summary>
    public IReadOnlyCollection<ExceptionRecord> Items { get; set; }
        = [];

    /// <summary>
    /// Gets or sets the total matching record count.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Gets or sets the applied result limit.
    /// </summary>
    public int Limit { get; set; }
}
