using FaultLens.Abstractions.Models;
using FaultLens.Storage.PostgreSql.Entities;
using System.Text.Json;

namespace FaultLens.Storage.PostgreSql.Mappers;

/// <summary>
/// Maps exception records between shared models and PostgreSQL storage entities.
/// </summary>
internal static class ExceptionRecordMapper
{
    /// <summary>
    /// Converts an exception record model to a storage entity.
    /// </summary>
    public static ExceptionRecordEntity ToEntity(ExceptionRecord model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new ExceptionRecordEntity
        {
            Id = model.Id == Guid.Empty ? Guid.NewGuid() : model.Id,
            OccurredAtUtc = model.OccurredAtUtc,
            ApplicationName = model.ApplicationName,
            EnvironmentName = model.EnvironmentName,
            ExceptionType = model.ExceptionType,
            Message = model.Message,
            StackTrace = model.StackTrace,
            Severity = model.Severity,
            CorrelationId = model.CorrelationId,
            TagsJson = JsonSerializer.Serialize(model.Tags),
            CreatedAtUtc = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Converts a storage entity to an exception record model.
    /// </summary>
    public static ExceptionRecord ToModel(ExceptionRecordEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new ExceptionRecord
        {
            Id = entity.Id,
            OccurredAtUtc = entity.OccurredAtUtc,
            ApplicationName = entity.ApplicationName,
            EnvironmentName = entity.EnvironmentName,
            ExceptionType = entity.ExceptionType,
            Message = entity.Message,
            StackTrace = entity.StackTrace,
            Severity = entity.Severity,
            CorrelationId = entity.CorrelationId,
            Tags = JsonSerializer.Deserialize<string[]>(entity.TagsJson)
                ?? []
        };
    }
}
