using System.Diagnostics.CodeAnalysis;
using PosTech.Hackathon.Pacientes.API.HealthChecks;

namespace PosTech.Hackathon.Pacientes.API.Setup;

[ExcludeFromCodeCoverage]
internal static class HealthCheckSetup
{
    internal static void AddRabbitMQHealthCheck(this IHealthChecksBuilder healthChecks)
    {
        healthChecks.AddCheck<MassTransitRabbitMqHealthCheck>(nameof(MassTransitRabbitMqHealthCheck));
    }
}