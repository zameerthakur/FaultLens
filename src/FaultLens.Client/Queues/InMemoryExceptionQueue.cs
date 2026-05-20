using System.Collections.Concurrent;
using FaultLens.Abstractions.Interfaces;
using FaultLens.Abstractions.Models;

namespace FaultLens.Client.Queues;

/// <summary>
/// Provides a bounded in-memory queue for temporary exception buffering.
/// </summary>
public sealed class InMemoryExceptionQueue : IExceptionQueue
{
    private readonly ConcurrentQueue<ExceptionRecord> _queue = new();
    private readonly int _capacity;

    /// <summary>
    /// Initializes a new instance of the <see cref="InMemoryExceptionQueue"/> class.
    /// </summary>
    /// <param name="capacity">
    /// The maximum number of exception records allowed in the queue.
    /// </param>
    public InMemoryExceptionQueue(int capacity)
    {
        _capacity = capacity <= 0 ? 1000 : capacity;
    }

    /// <inheritdoc />
    public int Count => _queue.Count;

    /// <inheritdoc />
    public bool TryEnqueue(ExceptionRecord exceptionRecord)
    {
        ArgumentNullException.ThrowIfNull(exceptionRecord);

        if (_queue.Count >= _capacity)
        {
            return false;
        }

        _queue.Enqueue(exceptionRecord);

        return true;
    }

    /// <inheritdoc />
    public bool TryDequeue(out ExceptionRecord? exceptionRecord)
    {
        return _queue.TryDequeue(out exceptionRecord);
    }
}
