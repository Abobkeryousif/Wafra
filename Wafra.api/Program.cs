using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Wafra.Application.Custom_Observability_Signals.CustomTraces;
using Wafra.Application.DependencyInjection;
using Wafra.Application.Observability.CustomMetrics;
using Wafra.Application.Observability.CustomTraces;
using Wafra.Core.Common;
using Wafra.Infrastructure.Data;
using Wafra.Infrastructure.DependencyInjection;


const string otel_endpoint_uri = "http://otel-collector-app:4317";


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(
    option => option
        .UseNpgsql(
            builder.Configuration.GetConnectionString("Default"))
        .ConfigureWarnings(warnings =>
            warnings.Ignore(
                RelationalEventId.PendingModelChangesWarning)));   // temporary to avoid Pending model issue, now just fouce with otel, otel col and diffrenet ovserbaility backend

builder.Services.AddSwaggerGen();
builder.Services.InfrastructureConfig(builder.Configuration);
builder.Services.ApplicationConfig();
builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSetting"));

//serilog config
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(
    theme: AnsiConsoleTheme.Code, 
    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.OpenTelemetry(options=>
    {
        options.Endpoint = otel_endpoint_uri;
        options.ResourceAttributes = new Dictionary<string, object>
        {
            ["service.name"] = "Wafra"
        };
    })
    .CreateLogger();

builder.Host.UseSerilog();


//opentelemetry settings and auto instermentation configs
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resourceBuilder => resourceBuilder.AddService("Wafra", "1.3"))
    .WithTracing(traceProvider=>
    {
        traceProvider
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddNpgsql()
                //.AddConsoleExporter()
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri(otel_endpoint_uri);
                });
    })
    .WithMetrics(meterProvider =>
    {
        meterProvider
            .AddMeter(WafraBusinessMetrics.meterName)
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddNpgsqlInstrumentation()
            .AddRuntimeInstrumentation()
            .AddOtlpExporter((exporterOptions, readerOptions) =>
            {
                exporterOptions.Endpoint =
                    new Uri(otel_endpoint_uri);

                readerOptions
                    .PeriodicExportingMetricReaderOptions
                    .ExportIntervalMilliseconds = 20_000;
            });
    });



var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

await app.Services.ApplyMigrationAsync<ApplicationDbContext>();

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
