using FaultLens.Abstractions.Enums;

namespace FaultLens.Storage.PostgreSql.Entities;

/// <summary>
/// Represents a persisted exception record.
/// </summary>
public sealed class ExceptionRecordEntity
{
    /// <summary>
    /// Gets or sets the unique exception identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp when the exception occurred.
    /// </summary>
    public DateTime OccurredAtUtc { get; set; }

    /// <summary>
    /// Gets or sets the application name.
    /// </summary>
    public string ApplicationName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the environment name.
    /// </summary>
    public string EnvironmentName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the exception type.
    /// </summary>
    public string ExceptionType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the exception message.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the stack trace.
    /// </summary>
    public string? StackTrace { get; set; }

    /// <summary>
    /// Gets or sets the operational severity.
    /// </summary>
    public ExceptionSeverity Severity { get; set; }

    /// <summary>
    /// Gets or sets the correlation identifier.
    /// </summary>
    public string? CorrelationId { get; set; }

    /// <summary>
    /// Gets or sets the serialized JSON representation of exception tags.
    /// </summary>
    public string TagsJson { get; set; } = "[]";

    /// <summary>
    /// Gets or sets the UTC timestamp when the record was stored.
    /// </summary>
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
