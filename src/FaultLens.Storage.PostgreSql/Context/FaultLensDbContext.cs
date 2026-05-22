using FaultLens.Storage.PostgreSql.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaultLens.Storage.PostgreSql.Context;

/// <summary>
/// Provides Entity Framework database access for FaultLens operational storage.
/// </summary>
public sealed class FaultLensDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FaultLensDbContext"/> class.
    /// </summary>
    /// <param name="options">
    /// The database context options.
    /// </param>
    public FaultLensDbContext(
        DbContextOptions<FaultLensDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets the stored exception records.
    /// </summary>
    public DbSet<ExceptionRecordEntity> ExceptionRecords => Set<ExceptionRecordEntity>();

    /// <summary>
    /// Gets the stored exception groups.
    /// </summary>
    public DbSet<ExceptionGroupEntity> ExceptionGroups => Set<ExceptionGroupEntity>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(FaultLensDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
