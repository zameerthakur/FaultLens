namespace FaultLens.Abstractions.Enums;

/// <summary>
/// Represents the search behavior mode used during exception queries.
/// </summary>
public enum SearchMode
{
    /// <summary>
    /// Performs deterministic keyword-based search only.
    /// </summary>
    Keyword = 0,

    /// <summary>
    /// Performs semantic similarity search only.
    /// </summary>
    Semantic = 1,

    /// <summary>
    /// Combines keyword and semantic similarity search.
    /// </summary>
    Hybrid = 2
}
