using FaultLens.Abstractions.Enums;
using FaultLens.Abstractions.Interfaces;
using FaultLens.Abstractions.Models;
using FaultLens.Core.Normalization;

namespace FaultLens.Core.Classification;

/// <summary>
/// Provides deterministic rule-based exception classification.
/// </summary>
public sealed class RuleBasedExceptionClassifier : IExceptionClassifier
{
    /// <summary>
    /// Classifies the specified exception record into an operational category.
    /// </summary>
    /// <param name="exceptionRecord">
    /// The exception record to classify.
    /// </param>
    /// <returns>
    /// The operational exception category.
    /// </returns>
    public ExceptionCategory Classify(ExceptionRecord exceptionRecord)
    {
        ArgumentNullException.ThrowIfNull(exceptionRecord);

        string text = string.Join(
            " ",
            ExceptionTextNormalizer.Normalize(exceptionRecord.ExceptionType),
            ExceptionTextNormalizer.Normalize(exceptionRecord.Message),
            ExceptionTextNormalizer.Normalize(exceptionRecord.StackTrace));

        if (ContainsAny(text, "sql", "database", "dbconnection", "entityframework"))
        {
            return ExceptionCategory.Database;
        }

        if (ContainsAny(text, "timeout", "timed out", "taskcanceledexception"))
        {
            return ExceptionCategory.Timeout;
        }

        if (ContainsAny(text, "unauthorized", "forbidden", "authentication", "authorization"))
        {
            return ExceptionCategory.Authentication;
        }

        if (ContainsAny(text, "configuration", "appsettings", "missing configuration"))
        {
            return ExceptionCategory.Configuration;
        }

        if (ContainsAny(text, "httprequestexception", "dependency", "external service"))
        {
            return ExceptionCategory.Dependency;
        }

        if (ContainsAny(text, "json", "serialization", "deserialize", "xml"))
        {
            return ExceptionCategory.Serialization;
        }

        if (ContainsAny(text, "socket", "network", "dns", "connection refused"))
        {
            return ExceptionCategory.Network;
        }

        if (ContainsAny(text, "outofmemory", "memory", "disk", "cpu", "connection pool"))
        {
            return ExceptionCategory.ResourceExhaustion;
        }

        if (ContainsAny(text, "validation", "invalid", "required"))
        {
            return ExceptionCategory.Validation;
        }

        return ExceptionCategory.Unknown;
    }

    /// <summary>
    /// Determines whether the source text contains any of the specified terms.
    /// </summary>
    private static bool ContainsAny(string source, params string[] terms)
    {
        return terms.Any(source.Contains);
    }
}
