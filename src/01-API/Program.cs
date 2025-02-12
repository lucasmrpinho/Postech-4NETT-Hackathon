using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using PosTech.Hackathon.Pacientes.API.Filters;
using PosTech.Hackathon.Pacientes.API.Logging;
using PosTech.Hackathon.Pacientes.API.PolicyHandler;
using PosTech.Hackathon.Pacientes.API.Setup;
using PosTech.Hackathon.Pacientes.Application.Services.Interfaces;
using PosTech.Hackathon.Pacientes.Application.Services;
using PosTech.Hackathon.Pacientes.Application.UseCases;
using PosTech.Hackathon.Pacientes.Application.Validators;
using PosTech.Hackathon.Pacientes.Domain.Interfaces;
using PosTech.Hackathon.Pacientes.Infrastructure.Config;
using PosTech.Hackathon.Pacientes.Infrastructure.Persistence;
using Prometheus;
using Serilog;
using PosTech.Hackathon.Pacientes.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:5013");

builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);
var configuration = builder.Configuration;

var loggerFactory = LoggerFactory.Create(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.SetMinimumLevel(LogLevel.Debug);
});
var logger = loggerFactory.CreateLogger("Startup");

builder.Services.AddControllers();
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<ExceptionFilter>();
//}); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<PacienteValidator>();

builder.Host.UseSerilog(SeriLogger.ConfigureLogger);

builder.Services.AddTransient<LoggingDelegatingHandler>();
builder.Services.AddTransient<ISavePacienteUseCase, SavePacienteUseCase>();

builder.Services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();

builder.Services.AddValidatorsFromAssemblyContaining<PacienteValidator>();

builder.Services.AddHealthChecks().AddRabbitMQHealthCheck();

builder.Services.AddHttpClient<IAutenticacaoClient, AutenticacaoClient>(c =>
                c.BaseAddress = new Uri(configuration["Autenticacao"]!))
                .AddHttpMessageHandler<LoggingDelegatingHandler>()
                .AddPolicyHandler(PolicyHandler.GetCircuitBreakerPolicy())
                .AddPolicyHandler(PolicyHandler.GetRetryPolicy());

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
    await next.Invoke();
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
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<TokenValidationMiddleware>();

app.Run();

public partial class Program
{
    protected Program() { }
}
