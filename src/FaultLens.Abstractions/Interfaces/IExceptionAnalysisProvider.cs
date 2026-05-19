using FaultLens.Abstractions.Models;

namespace FaultLens.Abstractions.Interfaces;

/// <summary>
/// Defines a contract for analyzing exception records.
/// </summary>
public interface IExceptionAnalysisProvider
{
    /// <summary>
    /// Analyzes the specified exception record.
    /// </summary>
    /// <param name="exceptionRecord">
    /// The exception record to analyze.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// The analysis result produced for the exception.
    /// </returns>
    Task<ExceptionAnalysisResult> AnalyzeAsync(
        ExceptionRecord exceptionRecord,
        CancellationToken cancellationToken = default);
}
