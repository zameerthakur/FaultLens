using FaultLens.Abstractions.Interfaces;
using FaultLens.Abstractions.Models;
using FaultLens.Core.Processing;
using Microsoft.AspNetCore.Mvc;

namespace FaultLens.Server.Controllers;

/// <summary>
/// Provides exception ingestion endpoints for FaultLens clients.
/// </summary>
[ApiController]
[Route("api/exceptions")]
public sealed class ExceptionsController : ControllerBase
{
    private readonly IExceptionProcessingService _processingService;
    private readonly ILogger<ExceptionsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionsController"/> class.
    /// </summary>
    public ExceptionsController(
        IExceptionProcessingService processingService,
        ILogger<ExceptionsController> logger)
    {
        _processingService = processingService;
        _logger = logger;
    }

    /// <summary>
    /// Ingests a single exception record.
    /// </summary>
    /// <param name="exceptionRecord">
    /// The exception record to ingest.
    /// </param>
    /// <returns>
    /// An HTTP response.
    /// </returns>
    [HttpPost]
    public ActionResult Ingest(
        [FromBody] ExceptionRecord exceptionRecord)
    {
        if (exceptionRecord is null)
        {
            return BadRequest();
        }

        ExceptionProcessingResult result =
            _processingService.Process(exceptionRecord);

        _logger.LogInformation(
            "Processed exception {ExceptionId} with fingerprint {Fingerprint}.",
            result.ExceptionId,
            result.Fingerprint);

        return Accepted();
    }

    /// <summary>
    /// Ingests multiple exception records.
    /// </summary>
    /// <param name="exceptionRecords">
    /// The exception records to ingest.
    /// </param>
    /// <returns>
    /// An HTTP response.
    /// </returns>
    [HttpPost("batch")]
    public ActionResult IngestBatch(
        [FromBody] IReadOnlyCollection<ExceptionRecord> exceptionRecords)
    {
        if (exceptionRecords is null || exceptionRecords.Count == 0)
        {
            return BadRequest();
        }

        foreach (ExceptionRecord exceptionRecord in exceptionRecords)
        {
            _processingService.Process(exceptionRecord);
        }

        _logger.LogInformation(
            "Processed batch containing {Count} exception records.",
            exceptionRecords.Count);

        return Accepted();
    }
}
