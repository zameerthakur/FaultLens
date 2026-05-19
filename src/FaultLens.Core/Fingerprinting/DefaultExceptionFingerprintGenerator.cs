using System.Security.Cryptography;
using System.Text;
using FaultLens.Abstractions.Interfaces;
using FaultLens.Abstractions.Models;
using FaultLens.Core.Normalization;

namespace FaultLens.Core.Fingerprinting;

/// <summary>
/// Generates deterministic fingerprints for exception records.
/// </summary>
public sealed class DefaultExceptionFingerprintGenerator : IExceptionFingerprintGenerator
{
    /// <inheritdoc />
    public string Generate(ExceptionRecord exceptionRecord)
    {
        ArgumentNullException.ThrowIfNull(exceptionRecord);

        string source = string.Join(
            "|",
            ExceptionTextNormalizer.Normalize(exceptionRecord.ApplicationName),
            ExceptionTextNormalizer.Normalize(exceptionRecord.EnvironmentName),
            ExceptionTextNormalizer.Normalize(exceptionRecord.ExceptionType),
            ExceptionTextNormalizer.Normalize(exceptionRecord.Message));

        byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
        byte[] hashBytes = SHA256.HashData(sourceBytes);

        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }
}
