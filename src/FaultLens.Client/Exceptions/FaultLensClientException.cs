namespace FaultLens.Client.Exceptions;

/// <summary>
/// Represents an error that occurs while using the FaultLens client SDK.
/// </summary>
public sealed class FaultLensClientException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FaultLensClientException"/> class.
    /// </summary>
    /// <param name="message">
    /// The error message.
    /// </param>
    public FaultLensClientException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultLensClientException"/> class.
    /// </summary>
    /// <param name="message">
    /// The error message.
    /// </param>
    /// <param name="innerException">
    /// The inner exception that caused this error.
    /// </param>
    public FaultLensClientException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
