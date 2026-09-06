using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace eCOFIN.Infrastructure.BackgroundJobs
{
    public class LedgerBackgroundService : BackgroundService
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly ILogger<LedgerBackgroundService> _logger;

        public LedgerBackgroundService(
            IBackgroundTaskQueue taskQueue,
            ILogger<LedgerBackgroundService> logger)
        {
            _taskQueue = taskQueue;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Ledger Background Service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var task = await _taskQueue.DequeueAsync(stoppingToken);
                    await task(stoppingToken);
                }
                catch (OperationCanceledException)
                {
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error executing background ledger task.");
                }
            }

            _logger.LogInformation("Ledger Background Service stopped.");
        }
    }
}