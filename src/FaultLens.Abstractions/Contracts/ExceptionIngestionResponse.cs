namespace FaultLens.Abstractions.Contracts;

/// <summary>
/// Represents the response returned after an exception ingestion request is accepted.
/// </summary>
public sealed class ExceptionIngestionResponse
{
    /// <summary>
    /// Gets or sets the unique identifier assigned to the ingested exception.
    /// </summary>
    public Guid ExceptionId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the ingestion request was accepted.
    /// </summary>
    public bool Accepted { get; set; }

    /// <summary>
    /// Gets or sets the optional ingestion status message.
    /// </summary>
    public string? Message { get; set; }
}
