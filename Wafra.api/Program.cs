using Microsoft.EntityFrameworkCore;
using Wafra.Application.DependencyInjection;
using Wafra.Core.Common;
using Wafra.Infrastructure.Data;
using Wafra.Infrastructure.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Npgsql;
using OpenTelemetry.Metrics;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var resource = ResourceBuilder.CreateDefault().AddService("Wafra");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddSwaggerGen();
builder.Services.InfrastructureConfig(builder.Configuration);
builder.Services.ApplicationConfig();
builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSetting"));

//serilog config
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(
    theme: AnsiConsoleTheme.Code, 
    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

builder.Host.UseSerilog();


//opentelemetry config
builder.Services.AddOpenTelemetry()
    .WithTracing(traceProvider=>
    {
        traceProvider
                .SetResourceBuilder(resource)
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddNpgsql()
                //.AddConsoleExporter()
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri("http://otel-collector-app:4317");
                });
    }).WithMetrics(meterProvider=>
    {
        meterProvider
            .SetResourceBuilder(resource)
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddNpgsqlInstrumentation()
            .AddOtlpExporter();
            //.AddConsoleExporter();
    });


var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
