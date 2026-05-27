using FaultLens.Abstractions.Enums;

namespace FaultLens.Abstractions.Contracts;

/// <summary>
/// Represents a request used to search exception records.
/// </summary>
public sealed class ExceptionSearchRequest
{
    /// <summary>
    /// Gets or sets the optional free-text search query.
    /// </summary>
    public string? Query { get; set; }

    /// <summary>
    /// Gets or sets the optional application name filter.
    /// </summary>
    public string? ApplicationName { get; set; }

    /// <summary>
    /// Gets or sets the optional environment name filter.
    /// </summary>
    public string? EnvironmentName { get; set; }

    /// <summary>
    /// Gets or sets the optional severity filter.
    /// </summary>
    public ExceptionSeverity? Severity { get; set; }

    /// <summary>
    /// Gets or sets the optional category filter.
    /// </summary>
    public ExceptionCategory? Category { get; set; }

    /// <summary>
    /// Gets or sets the optional UTC start date filter.
    /// </summary>
    public DateTime? FromUtc { get; set; }

    /// <summary>
    /// Gets or sets the optional UTC end date filter.
    /// </summary>
    public DateTime? ToUtc { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of results to return.
    /// </summary>
    public int Limit { get; set; } = 100;

    /// <summary>
    /// Gets or sets the number of records to skip.
    /// </summary>
    public int Skip { get; set; }
}
