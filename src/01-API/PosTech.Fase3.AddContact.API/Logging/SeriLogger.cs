using Serilog;

namespace PosTech.Hackathon.Pacientes.API.Logging
{
    public static class SeriLogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger =>
        (context, configuration) =>
        {
            var elasticUri = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");

            configuration
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.File(
                    path: "logs/log-.txt", // Caminho do arquivo de log
                    rollingInterval: RollingInterval.Day, // Cria um novo arquivo por dia
                    retainedFileCountLimit: 30, // Mantém logs dos últimos 30 dias
                    fileSizeLimitBytes: 10_000_000, // Limite de tamanho do arquivo de log (10 MB)
                    rollOnFileSizeLimit: true // Rolagem quando atingir o tamanho máximo
                    )

                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                .ReadFrom.Configuration(context.Configuration);
        };
    }
}
