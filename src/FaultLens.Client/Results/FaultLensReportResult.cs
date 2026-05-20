namespace FaultLens.Client.Results;

/// <summary>
/// Represents the result of a FaultLens reporting operation.
/// </summary>
public sealed class FaultLensReportResult
{
    /// <summary>
    /// Gets a successful result instance.
    /// </summary>
    public static FaultLensReportResult Success { get; } =
        new(true, null, null);

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultLensReportResult"/> class.
    /// </summary>
    /// <param name="isSuccessful">
    /// Indicates whether the operation succeeded.
    /// </param>
    /// <param name="message">
    /// The result message.
    /// </param>
    /// <param name="exception">
    /// The related exception.
    /// </param>
    public FaultLensReportResult(
        bool isSuccessful,
        string? message,
        Exception? exception)
    {
        IsSuccessful = isSuccessful;
        Message = message;
        Exception = exception;
    }

    /// <summary>
    /// Gets a value indicating whether the operation succeeded.
    /// </summary>
    public bool IsSuccessful { get; }

    /// <summary>
    /// Gets the result message.
    /// </summary>
    public string? Message { get; }

    /// <summary>
    /// Gets the related exception.
    /// </summary>
    public Exception? Exception { get; }
}
