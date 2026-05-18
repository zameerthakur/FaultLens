using FaultLens.Abstractions.Enums;

namespace FaultLens.Abstractions.Models;

/// <summary>
/// Represents a normalized exception record used throughout the FaultLens platform.
/// </summary>
public sealed class ExceptionRecord
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
    /// Gets or sets the application name that generated the exception.
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
    /// Gets or sets the operational severity level.
    /// </summary>
    public ExceptionSeverity Severity { get; set; }

    /// <summary>
    /// Gets or sets the correlation identifier.
    /// </summary>
    public string? CorrelationId { get; set; }

    /// <summary>
    /// Gets or sets additional operational tags.
    /// </summary>
    public IReadOnlyCollection<string> Tags { get; set; } = [];
}
