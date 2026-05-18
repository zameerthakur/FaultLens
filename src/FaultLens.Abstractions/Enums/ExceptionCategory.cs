namespace FaultLens.Abstractions.Enums;

/// <summary>
/// Represents the operational category assigned to an exception.
/// </summary>
public enum ExceptionCategory
{
    /// <summary>
    /// The exception category is unknown or has not been classified.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The exception is related to database connectivity or database operations.
    /// </summary>
    Database = 1,

    /// <summary>
    /// The exception is related to timeout failures.
    /// </summary>
    Timeout = 2,

    /// <summary>
    /// The exception is related to authentication or authorization failures.
    /// </summary>
    Authentication = 3,

    /// <summary>
    /// The exception is related to configuration problems.
    /// </summary>
    Configuration = 4,

    /// <summary>
    /// The exception is related to dependency or external service failures.
    /// </summary>
    Dependency = 5,

    /// <summary>
    /// The exception is related to serialization or data format problems.
    /// </summary>
    Serialization = 6,

    /// <summary>
    /// The exception is related to network connectivity or transport failures.
    /// </summary>
    Network = 7,

    /// <summary>
    /// The exception is related to resource exhaustion such as memory, CPU, disk, or connection limits.
    /// </summary>
    ResourceExhaustion = 8,

    /// <summary>
    /// The exception is related to validation or invalid input data.
    /// </summary>
    Validation = 9
}
