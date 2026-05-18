namespace FaultLens.Abstractions.Options;

/// <summary>
/// Represents configuration options used by the FaultLens client.
/// </summary>
public sealed class FaultLensClientOptions
{
    /// <summary>
    /// Gets or sets the FaultLens ingestion endpoint URL.
    /// </summary>
    public string EndpointUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the API key used to authenticate ingestion requests.
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Gets or sets the application name reported by the client.
    /// </summary>
    public string ApplicationName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the environment name reported by the client.
    /// </summary>
    public string EnvironmentName { get; set; } = "Production";

    /// <summary>
    /// Gets or sets a value indicating whether local buffering is enabled.
    /// </summary>
    public bool EnableLocalBuffering { get; set; } = true;
}
