namespace FaultLens.Abstractions.Interfaces;

/// <summary>
/// Defines a contract for generating vector embeddings from text input.
/// </summary>
public interface IEmbeddingProvider
{
    /// <summary>
    /// Generates an embedding vector for the specified text.
    /// </summary>
    /// <param name="text">
    /// The input text used to generate the embedding.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// The embedding vector generated for the input text.
    /// </returns>
    Task<IReadOnlyList<float>> GenerateEmbeddingAsync(
        string text,
        CancellationToken cancellationToken = default);
}
