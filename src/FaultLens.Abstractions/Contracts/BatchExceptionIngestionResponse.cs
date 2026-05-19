namespace FaultLens.Abstractions.Contracts;

/// <summary>
/// Represents the response returned after a batch exception ingestion request.
/// </summary>
public sealed class BatchExceptionIngestionResponse
{
    /// <summary>
    /// Gets or sets the ingestion responses returned for each exception in the batch.
    /// </summary>
    public IReadOnlyCollection<ExceptionIngestionResponse> Items { get; set; } = [];

    /// <summary>
    /// Gets or sets the total number of exception records accepted.
    /// </summary>
    public int AcceptedCount { get; set; }

    /// <summary>
    /// Gets or sets the total number of exception records rejected.
    /// </summary>
    public int RejectedCount { get; set; }
}
