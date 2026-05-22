using FaultLens.Abstractions.Models;
using FaultLens.Storage.PostgreSql.Entities;

namespace FaultLens.Storage.PostgreSql.Mappers;

/// <summary>
/// Maps exception groups between shared models and PostgreSQL storage entities.
/// </summary>
internal static class ExceptionGroupMapper
{
    /// <summary>
    /// Converts an exception group model to a storage entity.
    /// </summary>
    public static ExceptionGroupEntity ToEntity(ExceptionGroup model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new ExceptionGroupEntity
        {
            Id = model.Id == Guid.Empty ? Guid.NewGuid() : model.Id,
            Fingerprint = model.Fingerprint,
            Message = model.Message,
            ExceptionType = model.ExceptionType,
            Severity = model.Severity,
            Category = model.Category,
            OccurrenceCount = model.OccurrenceCount,
            FirstOccurredAtUtc = model.FirstOccurredAtUtc,
            LastOccurredAtUtc = model.LastOccurredAtUtc,
            CreatedAtUtc = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Converts a storage entity to an exception group model.
    /// </summary>
    public static ExceptionGroup ToModel(ExceptionGroupEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new ExceptionGroup
        {
            Id = entity.Id,
            Fingerprint = entity.Fingerprint,
            Message = entity.Message,
            ExceptionType = entity.ExceptionType,
            Severity = entity.Severity,
            Category = entity.Category,
            OccurrenceCount = entity.OccurrenceCount,
            FirstOccurredAtUtc = entity.FirstOccurredAtUtc,
            LastOccurredAtUtc = entity.LastOccurredAtUtc
        };
    }
}
