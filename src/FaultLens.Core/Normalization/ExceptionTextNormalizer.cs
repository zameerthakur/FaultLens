using System.Text.RegularExpressions;

namespace FaultLens.Core.Normalization;

/// <summary>
/// Provides deterministic normalization for exception text used by core processing workflows.
/// </summary>
public static partial class ExceptionTextNormalizer
{
    /// <summary>
    /// Normalizes exception text by trimming whitespace, reducing repeated whitespace,
    /// and converting the value to a consistent casing format.
    /// </summary>
    /// <param name="value">
    /// The exception text value to normalize.
    /// </param>
    /// <returns>
    /// The normalized exception text.
    /// </returns>
    public static string Normalize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        string normalized = WhitespaceRegex()
            .Replace(value.Trim(), " ");

        return normalized.ToLowerInvariant();
    }

    /// <summary>
    /// Gets the compiled whitespace normalization regular expression.
    /// </summary>
    /// <returns>
    /// A regular expression that matches one or more whitespace characters.
    /// </returns>
    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();
}
