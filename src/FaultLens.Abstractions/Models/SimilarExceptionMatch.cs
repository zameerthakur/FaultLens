namespace FaultLens.Abstractions.Models;

/// <summary>
/// Represents a similar exception match identified by semantic analysis.
/// </summary>
public sealed class SimilarExceptionMatch
{
    /// <summary>
    /// Gets or sets the matched exception identifier.
    /// </summary>
    public Guid ExceptionId { get; set; }

    /// <summary>
    /// Gets or sets the optional matched exception group identifier.
    /// </summary>
    public Guid? ExceptionGroupId { get; set; }

    /// <summary>
    /// Gets or sets the similarity score between the source exception and the matched exception.
    /// </summary>
    public double SimilarityScore { get; set; }

    /// <summary>
    /// Gets or sets the matched exception message.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the matched exception type.
    /// </summary>
    public string ExceptionType { get; set; } = string.Empty;
}
