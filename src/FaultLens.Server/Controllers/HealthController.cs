using FaultLens.Abstractions.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FaultLens.Server.Controllers;

/// <summary>
/// Provides operational health endpoints for FaultLens.
/// </summary>
[ApiController]
[Route("api/health")]
public sealed class HealthController : ControllerBase
{
    /// <summary>
    /// Returns the current FaultLens server health status.
    /// </summary>
    /// <returns>
    /// The health response.
    /// </returns>
    [HttpGet]
    [ProducesResponseType<FaultLensHealthResponse>(StatusCodes.Status200OK)]
    public ActionResult<FaultLensHealthResponse> Get()
    {
        return Ok(new FaultLensHealthResponse
        {
            Healthy = true,
            Message = "FaultLens server is operational.",
            Version = "1.0.0",
            CheckedAtUtc = DateTime.UtcNow
        });
    }
}
