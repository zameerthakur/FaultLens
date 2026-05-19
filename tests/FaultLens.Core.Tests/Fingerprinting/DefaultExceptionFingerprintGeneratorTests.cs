using FaultLens.Abstractions.Enums;
using FaultLens.Abstractions.Models;
using FaultLens.Core.Fingerprinting;

namespace FaultLens.Core.Tests.Fingerprinting;

/// <summary>
/// Tests deterministic exception fingerprint generation behavior.
/// </summary>
public sealed class DefaultExceptionFingerprintGeneratorTests
{
    /// <summary>
    /// Verifies that the same exception content produces the same fingerprint.
    /// </summary>
    [Fact]
    public void Generate_WhenSameExceptionContent_ReturnsSameFingerprint()
    {
        var generator = new DefaultExceptionFingerprintGenerator();

        ExceptionRecord first = CreateExceptionRecord();
        ExceptionRecord second = CreateExceptionRecord();

        string firstFingerprint = generator.Generate(first);
        string secondFingerprint = generator.Generate(second);

        Assert.Equal(firstFingerprint, secondFingerprint);
    }

    /// <summary>
    /// Verifies that different exception messages produce different fingerprints.
    /// </summary>
    [Fact]
    public void Generate_WhenExceptionMessageDiffers_ReturnsDifferentFingerprint()
    {
        var generator = new DefaultExceptionFingerprintGenerator();

        ExceptionRecord first = CreateExceptionRecord("Database timeout occurred.");
        ExceptionRecord second = CreateExceptionRecord("Authentication failed.");

        string firstFingerprint = generator.Generate(first);
        string secondFingerprint = generator.Generate(second);

        Assert.NotEqual(firstFingerprint, secondFingerprint);
    }

    /// <summary>
    /// Verifies that fingerprint generation returns a SHA-256 hex string.
    /// </summary>
    [Fact]
    public void Generate_WhenExceptionRecordIsValid_ReturnsSha256HexString()
    {
        var generator = new DefaultExceptionFingerprintGenerator();

        string fingerprint = generator.Generate(CreateExceptionRecord());

        Assert.Equal(64, fingerprint.Length);
        Assert.Matches("^[a-f0-9]{64}$", fingerprint);
    }

    private static ExceptionRecord CreateExceptionRecord(string message = "Database timeout occurred.")
    {
        return new ExceptionRecord
        {
            Id = Guid.NewGuid(),
            OccurredAtUtc = DateTime.UtcNow,
            ApplicationName = "OrderService",
            EnvironmentName = "Production",
            ExceptionType = "TimeoutException",
            Message = message,
            Severity = ExceptionSeverity.High
        };
    }
}
