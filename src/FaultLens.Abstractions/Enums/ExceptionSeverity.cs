namespace FaultLens.Abstractions.Enums;

/// <summary>
/// Represents the operational severity level of an exception.
/// </summary>
public enum ExceptionSeverity
{
    /// <summary>
    /// Informational exception with minimal operational impact.
    /// </summary>
    Information = 0,

    /// <summary>
    /// Low-severity operational issue.
    /// </summary>
    Low = 1,

    /// <summary>
    /// Moderate operational issue requiring attention.
    /// </summary>
    Medium = 2,

    /// <summary>
    /// High-severity operational issue affecting functionality.
    /// </summary>
    High = 3,

    /// <summary>
    /// Critical operational issue requiring immediate attention.
    /// </summary>
    Critical = 4
}
