namespace FaultLens.Abstractions.Contracts;

/// <summary>
/// Represents the health status response returned by FaultLens.
/// </summary>
public sealed class FaultLensHealthResponse
{
    /// <summary>
    /// Gets or sets a value indicating whether the service is healthy.
    /// </summary>
    public bool Healthy { get; set; }

    /// <summary>
    /// Gets or sets the optional status message.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp when the health status was checked.
    /// </summary>
    public DateTime CheckedAtUtc { get; set; } = DateTime.UtcNow;
}
