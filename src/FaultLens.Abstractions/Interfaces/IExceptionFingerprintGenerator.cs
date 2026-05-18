using FaultLens.Abstractions.Models;

namespace FaultLens.Abstractions.Interfaces;

/// <summary>
/// Defines a contract for generating stable exception fingerprints.
/// </summary>
public interface IExceptionFingerprintGenerator
{
    /// <summary>
    /// Generates a stable fingerprint for the specified exception record.
    /// </summary>
    /// <param name="exceptionRecord">
    /// The exception record used to generate the fingerprint.
    /// </param>
    /// <returns>
    /// A stable fingerprint representing the exception pattern.
    /// </returns>
    string Generate(ExceptionRecord exceptionRecord);
}
