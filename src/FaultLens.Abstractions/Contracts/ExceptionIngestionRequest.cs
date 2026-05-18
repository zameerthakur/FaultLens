using FaultLens.Abstractions.Models;

namespace FaultLens.Abstractions.Contracts;

/// <summary>
/// Represents a request to ingest an exception into the FaultLens platform.
/// </summary>
public sealed class ExceptionIngestionRequest
{
    /// <summary>
    /// Gets or sets the exception record payload.
    /// </summary>
    public ExceptionRecord Exception { get; set; } = new();

    /// <summary>
    /// Gets or sets the source application version.
    /// </summary>
    public string? ApplicationVersion { get; set; }

    /// <summary>
    /// Gets or sets the machine or host name.
    /// </summary>
    public string? HostName { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp when the request was created.
    /// </summary>
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
