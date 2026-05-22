using FaultLens.Storage.PostgreSql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaultLens.Storage.PostgreSql.Configurations;

/// <summary>
/// Configures the persisted exception record entity.
/// </summary>
public sealed class ExceptionRecordEntityConfiguration
    : IEntityTypeConfiguration<ExceptionRecordEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ExceptionRecordEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("exception_records");

        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.ApplicationName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(entity => entity.EnvironmentName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(entity => entity.ExceptionType)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(entity => entity.Message)
            .IsRequired();

        builder.Property(entity => entity.CorrelationId)
            .HasMaxLength(200);

        builder.Property(entity => entity.TagsJson)
            .HasColumnType("text")
            .IsRequired();

        builder.HasIndex(entity => entity.ApplicationName);

        builder.HasIndex(entity => entity.EnvironmentName);

        builder.HasIndex(entity => entity.ExceptionType);

        builder.HasIndex(entity => entity.OccurredAtUtc);
    }
}
