using System.Configuration;
using Wafra.Application.DependencyInjection;
using Wafra.Core.Common;
using Wafra.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.InfrastructureConfig(builder.Configuration);
builder.Services.ApplicationConfig();
builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSetting"));


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
