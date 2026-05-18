namespace FaultLens.Abstractions.Models;

/// <summary>
/// Represents the analysis result produced after processing an exception.
/// </summary>
public sealed class ExceptionAnalysisResult
{
    /// <summary>
    /// Gets or sets the exception identifier associated with the analysis result.
    /// </summary>
    public Guid ExceptionId { get; set; }

    /// <summary>
    /// Gets or sets the deterministic fingerprint generated for the exception.
    /// </summary>
    public string Fingerprint { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the operational category assigned to the exception.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the concise operational summary.
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// Gets or sets the similarity score produced by semantic analysis.
    /// </summary>
    public double? SimilarityScore { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp when the analysis was completed.
    /// </summary>
    public DateTime AnalyzedAtUtc { get; set; } = DateTime.UtcNow;
}
