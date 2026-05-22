using FaultLens.Abstractions.Interfaces;
using FaultLens.Core.Classification;
using FaultLens.Core.Fingerprinting;
using FaultLens.Core.Grouping;
using FaultLens.Core.Processing;
using FaultLens.Storage.PostgreSql.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString =
    builder.Configuration.GetConnectionString("FaultLens")
    ?? throw new InvalidOperationException(
        "FaultLens connection string is missing.");

builder.Services.AddFaultLensPostgreSqlStorage(connectionString);

builder.Services.AddSingleton<IExceptionFingerprintGenerator, DefaultExceptionFingerprintGenerator>();
builder.Services.AddSingleton<IExceptionClassifier, RuleBasedExceptionClassifier>();
builder.Services.AddScoped<IExceptionProcessingService, ExceptionProcessingService>();
builder.Services.AddScoped<IExceptionGroupingService, DefaultExceptionGroupingService>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
