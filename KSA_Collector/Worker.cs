using KSA_Collector.Settings;
using KSA_Collector.Controllers;

namespace KSA_Collector
{
    public class Worker : IHostedService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}

            ServiceSettings settings = ServiceSettings.GetSettings();

            DirectoryInfo parentPath = new DirectoryInfo(settings.SessionPath);
            DirectoryInfo[] sessionFolders = parentPath.GetDirectories();
            sessionFolders = sessionFolders.OrderByDescending(folder => folder.LastWriteTime).ToArray();

            DateTime lastDate = settings.lastDays != 0 ? DateTime.Now.AddDays(-settings.lastDays) : new DateTime();
            int countFolder = sessionFolders.Length;

            for (int i = 0; i < countFolder; i++)
            {
                if (sessionFolders[i].LastWriteTime < lastDate)
                    break;

                DirectoryInfo[] sessions = sessionFolders[i].GetDirectories();
                sessions = sessions.OrderByDescending(folder => folder.LastWriteTime).ToArray();

                _logger.LogInformation($"{DateTimeOffset.Now.ToString("HH:mm:ss")}: ({i+1}/{countFolder}) {sessionFolders[i].Name} Started...");

                int countSessions = sessions.Length;
                for (int j = 0; j < countSessions; j++)
                {
                    if (sessions[j].LastWriteTime < lastDate)
                        break;

                    System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch();
                    swatch.Start();

                    DiagSessionController files = new();
                    files.Load(sessions[j]);
                    swatch.Stop();

                    _logger.LogInformation($"{DateTimeOffset.Now.ToString("HH: mm:ss")}: {sessionFolders[i].Name} ({j+1}/{countSessions}) Timer: {swatch.Elapsed}");
                }
            }

            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}