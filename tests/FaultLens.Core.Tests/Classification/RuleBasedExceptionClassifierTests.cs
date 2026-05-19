using FaultLens.Abstractions.Enums;
using FaultLens.Abstractions.Models;
using FaultLens.Core.Classification;

namespace FaultLens.Core.Tests.Classification;

/// <summary>
/// Tests deterministic rule-based exception classification behavior.
/// </summary>
public sealed class RuleBasedExceptionClassifierTests
{
    /// <summary>
    /// Verifies that database-related exceptions are classified correctly.
    /// </summary>
    [Fact]
    public void Classify_WhenDatabaseException_ReturnsDatabaseCategory()
    {
        ExceptionCategory category = Classify("SqlException", "Database connection failed.");

        Assert.Equal(ExceptionCategory.Database, category);
    }

    /// <summary>
    /// Verifies that timeout-related exceptions are classified correctly.
    /// </summary>
    [Fact]
    public void Classify_WhenTimeoutException_ReturnsTimeoutCategory()
    {
        ExceptionCategory category = Classify("TimeoutException", "The operation timed out.");

        Assert.Equal(ExceptionCategory.Timeout, category);
    }

    /// <summary>
    /// Verifies that authentication-related exceptions are classified correctly.
    /// </summary>
    [Fact]
    public void Classify_WhenAuthenticationException_ReturnsAuthenticationCategory()
    {
        ExceptionCategory category = Classify("UnauthorizedAccessException", "Authentication failed.");

        Assert.Equal(ExceptionCategory.Authentication, category);
    }

    /// <summary>
    /// Verifies that unknown exceptions return the unknown category.
    /// </summary>
    [Fact]
    public void Classify_WhenExceptionIsUnknown_ReturnsUnknownCategory()
    {
        ExceptionCategory category = Classify("CustomException", "Unexpected application state.");

        Assert.Equal(ExceptionCategory.Unknown, category);
    }

    private static ExceptionCategory Classify(string exceptionType, string message)
    {
        var classifier = new RuleBasedExceptionClassifier();

        var exceptionRecord = new ExceptionRecord
        {
            Id = Guid.NewGuid(),
            OccurredAtUtc = DateTime.UtcNow,
            ApplicationName = "OrderService",
            EnvironmentName = "Production",
            ExceptionType = exceptionType,
            Message = message,
            Severity = ExceptionSeverity.Medium
        };

        return classifier.Classify(exceptionRecord);
    }
}
