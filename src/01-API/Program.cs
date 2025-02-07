using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using PosTech.Hackathon.Pacientes.API.Logging;
using PosTech.Hackathon.Pacientes.API.Setup;
using PosTech.Hackathon.Pacientes.Application.UseCases;
using PosTech.Hackathon.Pacientes.Domain.Interfaces;
using PosTech.Hackathon.Pacientes.Infrastructure.Config;
using PosTech.Hackathon.Pacientes.Infrastructure.Persistence;
using Prometheus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:5011");
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);
var configuration = builder.Configuration;

var loggerFactory = LoggerFactory.Create(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.SetMinimumLevel(LogLevel.Debug);
});
var logger = loggerFactory.CreateLogger("Startup");
logger.LogInformation("Application is starting...");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog(SeriLogger.ConfigureLogger);

builder.Services.AddTransient<LoggingDelegatingHandler>();
//builder.Services.AddTransient<ISavePacientePublisher, SavePacientePublisher>();
builder.Services.AddTransient<ISavePacienteUseCase, SavePacienteUseCase>();

builder.Services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();

builder.Services.AddHealthChecks().AddRabbitMQHealthCheck();

var corsPolicy = "_myCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.Use(async (context, next) =>
{
    logger.LogInformation("Incoming request: {Method} {Path}", context.Request.Method, context.Request.Path);
    await next.Invoke();
    logger.LogInformation("Response status code: {StatusCode}", context.Response.StatusCode);
});

app.UseCors(corsPolicy);
app.MapHealthChecks("/health");
app.MapHealthChecks("/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.UseMetricServer();
app.UseHttpMetrics();
app.UseSwagger();
app.UseSwaggerUI();
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

logger.LogInformation("Application is running on port 5011...");
app.Run();

public partial class Program
{
    protected Program() { }
}
