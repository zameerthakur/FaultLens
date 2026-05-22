using FaultLens.Abstractions.Interfaces;
using FaultLens.Abstractions.Models;
using FaultLens.Storage.PostgreSql.Context;
using FaultLens.Storage.PostgreSql.Entities;
using FaultLens.Storage.PostgreSql.Mappers;
using Microsoft.EntityFrameworkCore;

namespace FaultLens.Storage.PostgreSql.Stores;

/// <summary>
/// Provides PostgreSQL persistence for grouped exception patterns.
/// </summary>
public sealed class PostgreSqlExceptionGroupStore : IExceptionGroupStore
{
    private readonly FaultLensDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="PostgreSqlExceptionGroupStore"/> class.
    /// </summary>
    public PostgreSqlExceptionGroupStore(FaultLensDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task SaveAsync(
        ExceptionGroup exceptionGroup,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(exceptionGroup);

        ExceptionGroupEntity entity =
            ExceptionGroupMapper.ToEntity(exceptionGroup);

        await _dbContext.ExceptionGroups.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ExceptionGroup> UpsertAsync(
        ExceptionGroup exceptionGroup,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(exceptionGroup);

        ExceptionGroupEntity? existingEntity =
            await _dbContext.ExceptionGroups
                .FirstOrDefaultAsync(
                    group => group.Fingerprint == exceptionGroup.Fingerprint,
                    cancellationToken);

        if (existingEntity is null)
        {
            ExceptionGroupEntity newEntity =
                ExceptionGroupMapper.ToEntity(exceptionGroup);

            await _dbContext.ExceptionGroups.AddAsync(
                newEntity,
                cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return ExceptionGroupMapper.ToModel(newEntity);
        }

        existingEntity.OccurrenceCount += 1;
        existingEntity.LastOccurredAtUtc = exceptionGroup.LastOccurredAtUtc;

        if (exceptionGroup.Severity > existingEntity.Severity)
        {
            existingEntity.Severity = exceptionGroup.Severity;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return ExceptionGroupMapper.ToModel(existingEntity);
    }

    /// <inheritdoc />
    public async Task<ExceptionGroup?> GetByFingerprintAsync(
        string fingerprint,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(fingerprint))
        {
            return null;
        }

        ExceptionGroupEntity? entity = await _dbContext.ExceptionGroups
            .AsNoTracking()
            .FirstOrDefaultAsync(
                group => group.Fingerprint == fingerprint,
                cancellationToken);

        return entity is null
            ? null
            : ExceptionGroupMapper.ToModel(entity);
    }
}
