using FaultLens.Abstractions.Models;

namespace FaultLens.Abstractions.Interfaces;

/// <summary>
/// Defines a contract for temporarily buffering exception records.
/// </summary>
public interface IExceptionQueue
{
    /// <summary>
    /// Attempts to enqueue an exception record.
    /// </summary>
    /// <param name="exceptionRecord">
    /// The exception record to enqueue.
    /// </param>
    /// <returns>
    /// True if the record was queued successfully; otherwise, false.
    /// </returns>
    bool TryEnqueue(ExceptionRecord exceptionRecord);

    /// <summary>
    /// Attempts to dequeue an exception record.
    /// </summary>
    /// <param name="exceptionRecord">
    /// The dequeued exception record when available.
    /// </param>
    /// <returns>
    /// True if a record was dequeued successfully; otherwise, false.
    /// </returns>
    bool TryDequeue(out ExceptionRecord? exceptionRecord);

    /// <summary>
    /// Gets the current number of queued exception records.
    /// </summary>
    int Count { get; }
}
