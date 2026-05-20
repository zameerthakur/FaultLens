# FaultLens.Client

Lightweight .NET client SDK for reporting operational exceptions to the FaultLens platform.

## Overview

FaultLens.Client provides reusable reporting capabilities for .NET applications.

The package is responsible for:

- exception reporting
- batch delivery
- retry handling
- resilient transport communication
- safe operational delivery workflows

The SDK is intentionally lightweight and infrastructure-independent.

It does NOT include:

- database hosting
- dashboard hosting
- AI model hosting
- Docker infrastructure
- operational storage

Those responsibilities belong to the centralized FaultLens platform deployment.

## Features

- Lightweight HTTP reporting client
- Single and batch exception delivery
- Retry handling
- Result-based safe reporting APIs
- ILogger integration
- Dependency Injection support
- Non-blocking operational workflows
- Configurable retry and buffering options
- Safe host-application integration

## Installation

```powershell
dotnet add package FaultLens.Client
```

## Basic Usage

```csharp
using FaultLens.Abstractions.Models;
using FaultLens.Client.Clients;
using FaultLens.Client.Factories;
using FaultLens.Client.Options;

var options = new FaultLensClientOptions
{
    ServerUrl = "https://localhost:7071/",
    ApiKey = "your-api-key"
};

var client = FaultLensClientFactory.Create(options);

var exceptionRecord = new ExceptionRecord
{
    ApplicationName = "SampleApp",
    ExceptionType = "System.InvalidOperationException",
    Message = "Something failed.",
    StackTrace = "stack trace here",
    CreatedUtc = DateTime.UtcNow
};

await client.ReportAsync(exceptionRecord);
```

## Safe Reporting Example

```csharp
FaultLensReportResult result =
    await client.TryReportAsync(exceptionRecord);

if (!result.IsSuccessful)
{
    Console.WriteLine(result.Message);
}
```

## Dependency Injection Example

```csharp
builder.Services.AddFaultLensClient(options =>
{
    options.ServerUrl = "https://localhost:7071/";
    options.ApiKey = "your-api-key";
});
```

## Configuration Options

| Property | Description | Default |
|---|---|---|
| ServerUrl | FaultLens server URL | required |
| ApiKey | API authentication key | null |
| TimeoutSeconds | HTTP request timeout | 30 |
| MaxRetryAttempts | Retry attempts | 3 |
| EnableAutomaticReporting | Enables automatic reporting | true |
| MaxBatchSize | Maximum batch size | 100 |
| BufferCapacity | In-memory buffer capacity | 1000 |

## Logging

FaultLens.Client integrates with:

```text
Microsoft.Extensions.Logging
```

The SDK does not directly own application logging behavior.

Host applications remain responsible for:

- logging providers
- log destinations
- file logging
- structured logging configuration

## Failure Handling

FaultLens.Client is designed to fail safely.

The SDK:

- avoids crashing host applications
- avoids blocking UI threads
- supports retry workflows
- supports safe background delivery
- supports graceful degradation

If FaultLens becomes temporarily unavailable:

- retries continue
- reporting failures are isolated
- host application execution continues

## Platform Architecture

```text
Application
    ↓
FaultLens.Client
    ↓
FaultLens.Server
    ↓
PostgreSQL + pgvector
    ↓
FaultLens Dashboard
```

## Related Packages

- FaultLens.Abstractions
- FaultLens.Core
- FaultLens.AspNetCore
- FaultLens.Wpf
- FaultLens.WorkerService
- FaultLens.Server

## License

MIT License