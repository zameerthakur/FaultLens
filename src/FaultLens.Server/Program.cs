using FaultLens.Abstractions.Interfaces;
using FaultLens.Core.Classification;
using FaultLens.Core.Fingerprinting;
using FaultLens.Core.Grouping;
using FaultLens.Core.Processing;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
