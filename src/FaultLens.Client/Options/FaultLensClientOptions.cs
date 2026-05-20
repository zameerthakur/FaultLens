namespace FaultLens.Client.Options;

/// <summary>
/// Represents configuration options for the FaultLens client SDK.
/// </summary>
public sealed class FaultLensClientOptions
{
    /// <summary>
    /// Gets or sets the FaultLens server base URL.
    /// </summary>
    /// <example>
    /// https://localhost:7071/
    /// </example>
    public required string ServerUrl { get; set; }

    /// <summary>
    /// Gets or sets the API key used for authentication.
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Gets or sets the HTTP request timeout in seconds.
    /// </summary>
    /// <remarks>
    /// Default value is 30 seconds.
    /// </remarks>
    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Gets or sets the maximum retry attempts for failed requests.
    /// </summary>
    /// <remarks>
    /// Default value is 3 attempts.
    /// </remarks>
    public int MaxRetryAttempts { get; set; } = 3;

    /// <summary>
    /// Gets or sets a value indicating whether automatic exception reporting is enabled.
    /// </summary>
    /// <remarks>
    /// Default value is true.
    /// </remarks>
    public bool EnableAutomaticReporting { get; set; } = true;

    /// <summary>
    /// Gets or sets the maximum number of exception records allowed in a batch request.
    /// </summary>
    /// <remarks>
    /// Default value is 100.
    /// </remarks>
    public int MaxBatchSize { get; set; } = 100;

    /// <summary>
    /// Gets or sets the bounded in-memory queue size used for temporary buffering.
    /// </summary>
    /// <remarks>
    /// Default value is 1000.
    /// </remarks>
    public int BufferCapacity { get; set; } = 1000;
}
