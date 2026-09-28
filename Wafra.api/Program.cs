using Microsoft.EntityFrameworkCore;
using Wafra.Application.DependencyInjection;
using Wafra.Core.Common;
using Wafra.Infrastructure.Data;
using Wafra.Infrastructure.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Npgsql;
using OpenTelemetry.Metrics;


var resource = ResourceBuilder.CreateDefault();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddSwaggerGen();
builder.Services.InfrastructureConfig(builder.Configuration);
builder.Services.ApplicationConfig();
builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSetting"));


builder.Services.AddOpenTelemetry()
    .WithTracing(traceProvider=>
    {
        traceProvider
                .SetResourceBuilder(resource)
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddNpgsql()
                .AddConsoleExporter();
    }).WithMetrics(meterProvider=>
    {
        meterProvider
            .SetResourceBuilder(resource)
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddNpgsqlInstrumentation()
            .AddConsoleExporter();
    });


var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
