namespace FaultLens.Abstractions.Enums;

/// <summary>
/// Represents the processing status of an exception record.
/// </summary>
public enum ProcessingStatus
{
    /// <summary>
    /// The exception has been received but processing has not started.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// The exception is currently being processed.
    /// </summary>
    Processing = 1,

    /// <summary>
    /// The exception has been processed successfully.
    /// </summary>
    Completed = 2,

    /// <summary>
    /// The exception processing failed.
    /// </summary>
    Failed = 3,

    /// <summary>
    /// The exception was moved to a dead-letter workflow.
    /// </summary>
    DeadLettered = 4
}
