using FaultLens.Abstractions.Contracts;
using FaultLens.Abstractions.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FaultLens.Server.Controllers;

/// <summary>
/// Provides operational search endpoints for exception records.
/// </summary>
[ApiController]
[Route("api/exceptions/search")]
public sealed class ExceptionSearchController : ControllerBase
{
    private readonly IExceptionStore _exceptionStore;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionSearchController"/> class.
    /// </summary>
    public ExceptionSearchController(IExceptionStore exceptionStore)
    {
        _exceptionStore = exceptionStore;
    }

    /// <summary>
    /// Searches persisted exception records.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ExceptionSearchResponse>> Search(
        [FromBody] ExceptionSearchRequest request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return BadRequest();
        }

        ExceptionSearchResponse response =
            await _exceptionStore.SearchAsync(
                request,
                cancellationToken);

        return Ok(response);
    }
}
