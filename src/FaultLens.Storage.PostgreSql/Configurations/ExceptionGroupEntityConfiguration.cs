using FaultLens.Storage.PostgreSql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaultLens.Storage.PostgreSql.Configurations;

/// <summary>
/// Configures the persisted exception group entity.
/// </summary>
public sealed class ExceptionGroupEntityConfiguration
    : IEntityTypeConfiguration<ExceptionGroupEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ExceptionGroupEntity> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("exception_groups");

        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Fingerprint)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(entity => entity.Message)
            .IsRequired();

        builder.Property(entity => entity.ExceptionType)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasIndex(entity => entity.Fingerprint)
            .IsUnique();

        builder.HasIndex(entity => entity.Category);

        builder.HasIndex(entity => entity.Severity);

        builder.HasIndex(entity => entity.LastOccurredAtUtc);
    }
}
