using FaultLens.Abstractions.Enums;
using FaultLens.Abstractions.Interfaces;
using FaultLens.Abstractions.Models;
using FaultLens.Core.Classification;

namespace FaultLens.Core.Processing;

/// <summary>
/// Provides deterministic exception processing workflows.
/// </summary>
public sealed class ExceptionProcessingService
{
    private readonly IExceptionFingerprintGenerator _fingerprintGenerator;
    private readonly RuleBasedExceptionClassifier _classifier;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionProcessingService"/> class.
    /// </summary>
    /// <param name="fingerprintGenerator">
    /// The exception fingerprint generator.
    /// </param>
    /// <param name="classifier">
    /// The rule-based exception classifier.
    /// </param>
    public ExceptionProcessingService(
        IExceptionFingerprintGenerator fingerprintGenerator,
        RuleBasedExceptionClassifier classifier)
    {
        _fingerprintGenerator = fingerprintGenerator;
        _classifier = classifier;
    }

    /// <summary>
    /// Processes the specified exception record.
    /// </summary>
    /// <param name="exceptionRecord">
    /// The exception record to process.
    /// </param>
    /// <returns>
    /// The exception processing result.
    /// </returns>
    public ExceptionProcessingResult Process(ExceptionRecord exceptionRecord)
    {
        ArgumentNullException.ThrowIfNull(exceptionRecord);

        string fingerprint = _fingerprintGenerator.Generate(exceptionRecord);
        ExceptionCategory category = _classifier.Classify(exceptionRecord);

        return new ExceptionProcessingResult
        {
            ExceptionId = exceptionRecord.Id,
            Status = ProcessingStatus.Completed,
            Fingerprint = fingerprint,
            Message = $"Exception classified as {category}.",
            ProcessedAtUtc = DateTime.UtcNow
        };
    }
}
