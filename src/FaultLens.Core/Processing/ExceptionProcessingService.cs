using FaultLens.Abstractions.Enums;
using FaultLens.Abstractions.Interfaces;
using FaultLens.Abstractions.Models;

namespace FaultLens.Core.Processing;

/// <summary>
/// Provides deterministic exception processing workflows.
/// </summary>
public sealed class ExceptionProcessingService
    : IExceptionProcessingService
{
    private readonly IExceptionFingerprintGenerator _fingerprintGenerator;
    private readonly IExceptionClassifier _classifier;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionProcessingService"/> class.
    /// </summary>
    public ExceptionProcessingService(
        IExceptionFingerprintGenerator fingerprintGenerator,
        IExceptionClassifier classifier)
    {
        _fingerprintGenerator = fingerprintGenerator;
        _classifier = classifier;
    }

    /// <inheritdoc />
    public ExceptionProcessingResult Process(ExceptionRecord exceptionRecord)
    {
        ArgumentNullException.ThrowIfNull(exceptionRecord);

        string fingerprint =
            _fingerprintGenerator.Generate(exceptionRecord);

        ExceptionCategory category =
            _classifier.Classify(exceptionRecord);

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
