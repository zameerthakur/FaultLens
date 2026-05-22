using FaultLens.Abstractions.Interfaces;
using FaultLens.Abstractions.Models;
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
    private readonly IExceptionGroupingService _groupingService;
    private readonly IExceptionStore _exceptionStore;
    private readonly IExceptionGroupStore _groupStore;
    private readonly ILogger<ExceptionsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionsController"/> class.
    /// </summary>
    public ExceptionsController(
        IExceptionProcessingService processingService,
        IExceptionGroupingService groupingService,
        IExceptionStore exceptionStore,
        IExceptionGroupStore groupStore,
        ILogger<ExceptionsController> logger)
    {
        _processingService = processingService;
        _groupingService = groupingService;
        _exceptionStore = exceptionStore;
        _groupStore = groupStore;
        _logger = logger;
    }

    /// <summary>
    /// Ingests a single exception record.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Ingest(
        [FromBody] ExceptionRecord exceptionRecord,
        CancellationToken cancellationToken)
    {
        if (exceptionRecord is null)
        {
            return BadRequest();
        }

        exceptionRecord.Id = Guid.NewGuid();

        ExceptionProcessingResult result =
            _processingService.Process(exceptionRecord);

        ExceptionGroup group =
            await _groupingService.GroupAsync(
                exceptionRecord,
                cancellationToken);

        await _exceptionStore.SaveAsync(
            exceptionRecord,
            cancellationToken);

        await _groupStore.UpsertAsync(
            group,
            cancellationToken);

        _logger.LogInformation(
            "Stored exception {ExceptionId} with fingerprint {Fingerprint}.",
            result.ExceptionId,
            result.Fingerprint);

        return Accepted();
    }

    /// <summary>
    /// Ingests multiple exception records.
    /// </summary>
    [HttpPost("batch")]
    public async Task<ActionResult> IngestBatch(
        [FromBody] IReadOnlyCollection<ExceptionRecord> exceptionRecords,
        CancellationToken cancellationToken)
    {
        if (exceptionRecords is null || exceptionRecords.Count == 0)
        {
            return BadRequest();
        }

        foreach (ExceptionRecord exceptionRecord in exceptionRecords)
        {
            exceptionRecord.Id = Guid.NewGuid();

            ExceptionProcessingResult result =
                _processingService.Process(exceptionRecord);

            ExceptionGroup group =
                await _groupingService.GroupAsync(
                    exceptionRecord,
                    cancellationToken);

            await _exceptionStore.SaveAsync(
                exceptionRecord,
                cancellationToken);

            await _groupStore.UpsertAsync(
                group,
                cancellationToken);

            _logger.LogInformation(
                "Stored exception {ExceptionId} with fingerprint {Fingerprint}.",
                result.ExceptionId,
                result.Fingerprint);
        }

        return Accepted();
    }
}
