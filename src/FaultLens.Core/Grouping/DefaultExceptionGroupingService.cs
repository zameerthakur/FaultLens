using FaultLens.Abstractions.Enums;
using FaultLens.Abstractions.Interfaces;
using FaultLens.Abstractions.Models;
using FaultLens.Core.Classification;

namespace FaultLens.Core.Grouping;

/// <summary>
/// Provides deterministic grouping for exception records.
/// </summary>
public sealed class DefaultExceptionGroupingService : IExceptionGroupingService
{
    private readonly IExceptionFingerprintGenerator _fingerprintGenerator;
    private readonly RuleBasedExceptionClassifier _classifier;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultExceptionGroupingService"/> class.
    /// </summary>
    /// <param name="fingerprintGenerator">
    /// The exception fingerprint generator.
    /// </param>
    /// <param name="classifier">
    /// The rule-based exception classifier.
    /// </param>
    public DefaultExceptionGroupingService(
        IExceptionFingerprintGenerator fingerprintGenerator,
        RuleBasedExceptionClassifier classifier)
    {
        _fingerprintGenerator = fingerprintGenerator;
        _classifier = classifier;
    }

    /// <inheritdoc />
    public Task<ExceptionGroup> GroupAsync(
        ExceptionRecord exceptionRecord,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(exceptionRecord);

        cancellationToken.ThrowIfCancellationRequested();

        string fingerprint = _fingerprintGenerator.Generate(exceptionRecord);
        ExceptionCategory category = _classifier.Classify(exceptionRecord);

        var group = new ExceptionGroup
        {
            Id = Guid.NewGuid(),
            Fingerprint = fingerprint,
            Message = exceptionRecord.Message,
            ExceptionType = exceptionRecord.ExceptionType,
            Severity = exceptionRecord.Severity,
            Category = category,
            OccurrenceCount = 1,
            FirstOccurredAtUtc = exceptionRecord.OccurredAtUtc,
            LastOccurredAtUtc = exceptionRecord.OccurredAtUtc
        };

        return Task.FromResult(group);
    }
}
