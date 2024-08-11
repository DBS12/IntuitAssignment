using IntuitAssignment.Engine.Interfaces;

namespace IntuitAssignment.Startup
{
    public class StartupBackgroundService : BackgroundService
    {
        private readonly ILogger<StartupBackgroundService> _logger;
        IEngineDataParser _engine;

        public StartupBackgroundService(ILogger<StartupBackgroundService> logger, IEngineDataParser engineParser)
        {
            _logger = logger;
            _engine = engineParser;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background service starting.");

            // Perform startup tasks here
            await InitializeTasksAsync(stoppingToken);

            _logger.LogInformation("Background service completed.");
        }

        private async Task InitializeTasksAsync(CancellationToken cancellationToken)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExternalResources", "players.csv");
            await _engine.ParseData(filePath, cancellationToken);
        }
    }
}
