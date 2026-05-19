using FaultLens.Abstractions.Enums;
using FaultLens.Abstractions.Models;
using FaultLens.Core.Classification;
using FaultLens.Core.Fingerprinting;
using FaultLens.Core.Processing;

namespace FaultLens.Core.Tests.Processing;

/// <summary>
/// Tests deterministic exception processing behavior.
/// </summary>
public sealed class ExceptionProcessingServiceTests
{
    /// <summary>
    /// Verifies that processing a valid exception returns a completed processing result.
    /// </summary>
    [Fact]
    public void Process_WhenExceptionRecordIsValid_ReturnsCompletedResult()
    {
        var service = new ExceptionProcessingService(
            new DefaultExceptionFingerprintGenerator(),
            new RuleBasedExceptionClassifier());

        ExceptionRecord exceptionRecord = CreateExceptionRecord();

        ExceptionProcessingResult result = service.Process(exceptionRecord);

        Assert.Equal(exceptionRecord.Id, result.ExceptionId);
        Assert.Equal(ProcessingStatus.Completed, result.Status);
        Assert.False(string.IsNullOrWhiteSpace(result.Fingerprint));
        Assert.Contains("Timeout", result.Message);
    }

    private static ExceptionRecord CreateExceptionRecord()
    {
        return new ExceptionRecord
        {
            Id = Guid.NewGuid(),
            OccurredAtUtc = DateTime.UtcNow,
            ApplicationName = "OrderService",
            EnvironmentName = "Production",
            ExceptionType = "TimeoutException",
            Message = "The operation timed out.",
            Severity = ExceptionSeverity.High
        };
    }
}
