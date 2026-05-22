using FaultLens.Abstractions.Enums;

namespace FaultLens.Storage.PostgreSql.Entities;

/// <summary>
/// Represents a persisted exception group.
/// </summary>
public sealed class ExceptionGroupEntity
{
    /// <summary>
    /// Gets or sets the unique group identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the deterministic fingerprint.
    /// </summary>
    public string Fingerprint { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the representative exception message.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the exception type.
    /// </summary>
    public string ExceptionType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the operational severity.
    /// </summary>
    public ExceptionSeverity Severity { get; set; }

    /// <summary>
    /// Gets or sets the operational category.
    /// </summary>
    public ExceptionCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the total occurrence count.
    /// </summary>
    public long OccurrenceCount { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp when the group first occurred.
    /// </summary>
    public DateTime FirstOccurredAtUtc { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp when the group most recently occurred.
    /// </summary>
    public DateTime LastOccurredAtUtc { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp when the group was created.
    /// </summary>
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
