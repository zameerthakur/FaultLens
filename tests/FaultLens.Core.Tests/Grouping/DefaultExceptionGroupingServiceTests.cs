using FaultLens.Abstractions.Enums;
using FaultLens.Abstractions.Models;
using FaultLens.Core.Classification;
using FaultLens.Core.Fingerprinting;
using FaultLens.Core.Grouping;

namespace FaultLens.Core.Tests.Grouping;

/// <summary>
/// Tests deterministic exception grouping behavior.
/// </summary>
public sealed class DefaultExceptionGroupingServiceTests
{
    /// <summary>
    /// Verifies that grouping creates an exception group with expected operational values.
    /// </summary>
    [Fact]
    public async Task GroupAsync_WhenExceptionRecordIsValid_ReturnsExceptionGroup()
    {
        var service = new DefaultExceptionGroupingService(
            new DefaultExceptionFingerprintGenerator(),
            new RuleBasedExceptionClassifier());

        ExceptionRecord exceptionRecord = CreateExceptionRecord();

        ExceptionGroup group = await service.GroupAsync(exceptionRecord);

        Assert.NotEqual(Guid.Empty, group.Id);
        Assert.False(string.IsNullOrWhiteSpace(group.Fingerprint));
        Assert.Equal(exceptionRecord.Message, group.Message);
        Assert.Equal(exceptionRecord.ExceptionType, group.ExceptionType);
        Assert.Equal(exceptionRecord.Severity, group.Severity);
        Assert.Equal(ExceptionCategory.Timeout, group.Category);
        Assert.Equal(1, group.OccurrenceCount);
        Assert.Equal(exceptionRecord.OccurredAtUtc, group.FirstOccurredAtUtc);
        Assert.Equal(exceptionRecord.OccurredAtUtc, group.LastOccurredAtUtc);
    }

    /// <summary>
    /// Verifies that grouping respects cancellation requests.
    /// </summary>
    [Fact]
    public async Task GroupAsync_WhenCancellationIsRequested_ThrowsOperationCanceledException()
    {
        var service = new DefaultExceptionGroupingService(
            new DefaultExceptionFingerprintGenerator(),
            new RuleBasedExceptionClassifier());

        using var cancellationTokenSource = new CancellationTokenSource();
        await cancellationTokenSource.CancelAsync();

        await Assert.ThrowsAsync<OperationCanceledException>(
            () => service.GroupAsync(CreateExceptionRecord(), cancellationTokenSource.Token));
    }

    private static ExceptionRecord CreateExceptionRecord()
    {
        var occurredAtUtc = new DateTime(2026, 5, 18, 12, 0, 0, DateTimeKind.Utc);

        return new ExceptionRecord
        {
            Id = Guid.NewGuid(),
            OccurredAtUtc = occurredAtUtc,
            ApplicationName = "OrderService",
            EnvironmentName = "Production",
            ExceptionType = "TimeoutException",
            Message = "The operation timed out.",
            Severity = ExceptionSeverity.High
        };
    }
}
