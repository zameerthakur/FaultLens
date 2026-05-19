using FaultLens.Abstractions.Enums;

namespace FaultLens.Abstractions.Models;

/// <summary>
/// Represents the result of processing an exception record.
/// </summary>
public sealed class ExceptionProcessingResult
{
    /// <summary>
    /// Gets or sets the exception identifier.
    /// </summary>
    public Guid ExceptionId { get; set; }

    /// <summary>
    /// Gets or sets the processing status.
    /// </summary>
    public ProcessingStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the generated exception fingerprint.
    /// </summary>
    public string? Fingerprint { get; set; }

    /// <summary>
    /// Gets or sets the optional processing message.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp when processing completed.
    /// </summary>
    public DateTime ProcessedAtUtc { get; set; } = DateTime.UtcNow;
}
