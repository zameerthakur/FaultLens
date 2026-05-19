using FaultLens.Core.Normalization;

namespace FaultLens.Core.Tests.Normalization;

/// <summary>
/// Tests deterministic exception text normalization behavior.
/// </summary>
public sealed class ExceptionTextNormalizerTests
{
    /// <summary>
    /// Verifies that null input returns an empty string.
    /// </summary>
    [Fact]
    public void Normalize_WhenValueIsNull_ReturnsEmptyString()
    {
        string result = ExceptionTextNormalizer.Normalize(null);

        Assert.Equal(string.Empty, result);
    }

    /// <summary>
    /// Verifies that whitespace-only input returns an empty string.
    /// </summary>
    [Fact]
    public void Normalize_WhenValueIsWhitespace_ReturnsEmptyString()
    {
        string result = ExceptionTextNormalizer.Normalize("   ");

        Assert.Equal(string.Empty, result);
    }

    /// <summary>
    /// Verifies that text is trimmed, whitespace-normalized, and lowercased.
    /// </summary>
    [Fact]
    public void Normalize_WhenValueContainsExtraWhitespace_ReturnsNormalizedText()
    {
        string result = ExceptionTextNormalizer.Normalize("  SQL   Timeout   Exception  ");

        Assert.Equal("sql timeout exception", result);
    }
}
