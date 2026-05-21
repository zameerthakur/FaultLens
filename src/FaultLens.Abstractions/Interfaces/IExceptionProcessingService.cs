using FaultLens.Abstractions.Models;

namespace FaultLens.Abstractions.Interfaces;

/// <summary>
/// Defines deterministic exception processing workflows.
/// </summary>
public interface IExceptionProcessingService
{
    /// <summary>
    /// Processes the specified exception record.
    /// </summary>
    /// <param name="exceptionRecord">
    /// The exception record to process.
    /// </param>
    /// <returns>
    /// The exception processing result.
    /// </returns>
    ExceptionProcessingResult Process(ExceptionRecord exceptionRecord);
}
