namespace FaultLens.Abstractions.Contracts;

/// <summary>
/// Represents a batch exception ingestion request.
/// </summary>
public sealed class BatchExceptionIngestionRequest
{
    /// <summary>
    /// Gets or sets the exception ingestion requests included in the batch.
    /// </summary>
    public IReadOnlyCollection<ExceptionIngestionRequest> Items { get; set; } = [];

    /// <summary>
    /// Gets or sets the UTC timestamp when the batch was created.
    /// </summary>
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
