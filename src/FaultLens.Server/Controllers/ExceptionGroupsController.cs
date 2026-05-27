using FaultLens.Abstractions.Contracts;
using FaultLens.Abstractions.Interfaces;
using FaultLens.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;

namespace FaultLens.Server.Controllers;

/// <summary>
/// Provides operational query endpoints for grouped exception patterns.
/// </summary>
[ApiController]
[Route("api/groups")]
public sealed class ExceptionGroupsController : ControllerBase
{
    private readonly IExceptionGroupStore _groupStore;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionGroupsController"/> class.
    /// </summary>
    public ExceptionGroupsController(IExceptionGroupStore groupStore)
    {
        _groupStore = groupStore;
    }

    /// <summary>
    /// Gets grouped exception patterns.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ExceptionGroupSearchResponse>> GetGroups(
        [FromQuery] int limit = 100,
        CancellationToken cancellationToken = default)
    {
        ExceptionGroupSearchResponse response =
            await _groupStore.SearchAsync(limit, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Gets a grouped exception pattern by fingerprint.
    /// </summary>
    [HttpGet("{fingerprint}")]
    public async Task<ActionResult<ExceptionGroup>> GetByFingerprint(
        string fingerprint,
        CancellationToken cancellationToken)
    {
        ExceptionGroup? group =
            await _groupStore.GetByFingerprintAsync(
                fingerprint,
                cancellationToken);

        if (group is null)
        {
            return NotFound();
        }

        return Ok(group);
    }
}
