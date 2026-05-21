using FaultLens.Abstractions.Enums;
using FaultLens.Abstractions.Models;

namespace FaultLens.Abstractions.Interfaces;

/// <summary>
/// Defines exception classification behavior.
/// </summary>
public interface IExceptionClassifier
{
    /// <summary>
    /// Classifies the specified exception record.
    /// </summary>
    /// <param name="exceptionRecord">
    /// The exception record to classify.
    /// </param>
    /// <returns>
    /// The operational exception category.
    /// </returns>
    ExceptionCategory Classify(ExceptionRecord exceptionRecord);
}
