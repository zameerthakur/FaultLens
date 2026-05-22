using FaultLens.Abstractions.Interfaces;
using FaultLens.Storage.PostgreSql.Context;
using FaultLens.Storage.PostgreSql.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FaultLens.Storage.PostgreSql.Extensions;

/// <summary>
/// Provides dependency injection registration extensions for PostgreSQL storage.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers FaultLens PostgreSQL storage services.
    /// </summary>
    /// <param name="services">
    /// The service collection.
    /// </param>
    /// <param name="connectionString">
    /// The PostgreSQL connection string.
    /// </param>
    /// <returns>
    /// The updated service collection.
    /// </returns>
    public static IServiceCollection AddFaultLensPostgreSqlStorage(
        this IServiceCollection services,
        string connectionString)
    {
        ArgumentNullException.ThrowIfNull(services);

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException(
                "PostgreSQL connection string is required.",
                nameof(connectionString));
        }

        services.AddDbContext<FaultLensDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IExceptionStore, PostgreSqlExceptionStore>();
        services.AddScoped<IExceptionGroupStore, PostgreSqlExceptionGroupStore>();

        return services;
    }
}
