using FaultLens.Abstractions.Contracts;
using FaultLens.Abstractions.Interfaces;
using FaultLens.Abstractions.Models;
using FaultLens.Storage.PostgreSql.Context;
using FaultLens.Storage.PostgreSql.Entities;
using FaultLens.Storage.PostgreSql.Mappers;
using Microsoft.EntityFrameworkCore;

namespace FaultLens.Storage.PostgreSql.Stores;

/// <summary>
/// Provides PostgreSQL persistence for exception records.
/// </summary>
public sealed class PostgreSqlExceptionStore : IExceptionStore
{
    private readonly FaultLensDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="PostgreSqlExceptionStore"/> class.
    /// </summary>
    /// <param name="dbContext">
    /// The FaultLens database context.
    /// </param>
    public PostgreSqlExceptionStore(FaultLensDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task SaveAsync(
        ExceptionRecord exceptionRecord,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(exceptionRecord);

        ExceptionRecordEntity entity =
            ExceptionRecordMapper.ToEntity(exceptionRecord);

        await _dbContext.ExceptionRecords.AddAsync(
            entity,
            cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ExceptionSearchResponse> SearchAsync(
        ExceptionSearchRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        IQueryable<ExceptionRecordEntity> query =
            _dbContext.ExceptionRecords.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.ApplicationName))
        {
            query = query.Where(
                entity => entity.ApplicationName == request.ApplicationName);
        }

        if (!string.IsNullOrWhiteSpace(request.EnvironmentName))
        {
            query = query.Where(
                entity => entity.EnvironmentName == request.EnvironmentName);
        }

        if (request.Severity.HasValue)
        {
            query = query.Where(
                entity => entity.Severity == request.Severity.Value);
        }

        if (request.FromUtc.HasValue)
        {
            query = query.Where(
                entity => entity.OccurredAtUtc >= request.FromUtc.Value);
        }

        if (request.ToUtc.HasValue)
        {
            query = query.Where(
                entity => entity.OccurredAtUtc <= request.ToUtc.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Query))
        {
            query = query.Where(
                entity =>
                    entity.Message.Contains(request.Query) ||
                    entity.ExceptionType.Contains(request.Query));
        }

        int totalCount = await query.CountAsync(cancellationToken);

        List<ExceptionRecord> items = await query
            .OrderByDescending(entity => entity.OccurredAtUtc)
            .Take(request.Limit)
            .Select(entity => ExceptionRecordMapper.ToModel(entity))
            .ToListAsync(cancellationToken);

        return new ExceptionSearchResponse
        {
            Items = items,
            TotalCount = totalCount,
            Limit = request.Limit
        };
    }
}
