using FaultLens.Abstractions.Enums;

namespace FaultLens.Abstractions.Models;

/// <summary>
/// Represents a grouped operational exception pattern.
/// </summary>
public sealed class ExceptionGroup
{
    /// <summary>
    /// Gets or sets the unique group identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the stable fingerprint associated with the group.
    /// </summary>
    public string Fingerprint { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the representative exception message.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the representative exception type.
    /// </summary>
    public string ExceptionType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the operational severity assigned to the group.
    /// </summary>
    public ExceptionSeverity Severity { get; set; }

    /// <summary>
    /// Gets or sets the operational category assigned to the group.
    /// </summary>
    public ExceptionCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the number of occurrences associated with the group.
    /// </summary>
    public long OccurrenceCount { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp of the first occurrence.
    /// </summary>
    public DateTime FirstOccurredAtUtc { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp of the most recent occurrence.
    /// </summary>
    public DateTime LastOccurredAtUtc { get; set; }
}
