using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using PosTech.Hackathon.Pacientes.API.Logging;
using PosTech.Hackathon.Pacientes.API.Setup;
using PosTech.Hackathon.Pacientes.Application.UseCases;
using PosTech.Hackathon.Pacientes.Domain.Interfaces;
using Prometheus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:5011");
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog(SeriLogger.ConfigureLogger);

builder.Services.AddTransient<LoggingDelegatingHandler>();
//builder.Services.AddTransient<ISavePacientePublisher, SavePacientePublisher>();
builder.Services.AddTransient<ISavePacienteUseCase, SavePacienteUseCase>();

builder.Services.AddHealthChecks().AddRabbitMQHealthCheck();

var app = builder.Build();

app.MapHealthChecks("/health"); 
app.MapHealthChecks("/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.UseMetricServer();
app.UseHttpMetrics();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program
{
    protected Program() { }
}