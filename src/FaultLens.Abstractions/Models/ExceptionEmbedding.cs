namespace FaultLens.Abstractions.Models;

/// <summary>
/// Represents a semantic embedding associated with an exception or exception group.
/// </summary>
public sealed class ExceptionEmbedding
{
    /// <summary>
    /// Gets or sets the unique embedding identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the exception identifier associated with the embedding.
    /// </summary>
    public Guid ExceptionId { get; set; }

    /// <summary>
    /// Gets or sets the optional exception group identifier associated with the embedding.
    /// </summary>
    public Guid? ExceptionGroupId { get; set; }

    /// <summary>
    /// Gets or sets the embedding model identifier.
    /// </summary>
    public string ModelId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the embedding vector values.
    /// </summary>
    public IReadOnlyList<float> Vector { get; set; } = [];

    /// <summary>
    /// Gets or sets the UTC timestamp when the embedding was created.
    /// </summary>
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
