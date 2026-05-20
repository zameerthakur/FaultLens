namespace FaultLens.Client.Internal;

/// <summary>
/// Provides lightweight retry execution for FaultLens client operations.
/// </summary>
internal static class FaultLensRetryPolicy
{
    /// <summary>
    /// Executes the specified operation with retry attempts.
    /// </summary>
    /// <param name="operation">
    /// The asynchronous operation to execute.
    /// </param>
    /// <param name="maxAttempts">
    /// The maximum number of attempts.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    public static async Task ExecuteAsync(
        Func<CancellationToken, Task> operation,
        int maxAttempts,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(operation);

        if (maxAttempts <= 0)
        {
            maxAttempts = 1;
        }

        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                await operation(cancellationToken);
                return;
            }
            catch when (attempt < maxAttempts && !cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(
                    TimeSpan.FromMilliseconds(250 * attempt),
                    cancellationToken);
            }
        }
    }
}
