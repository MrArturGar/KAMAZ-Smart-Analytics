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

            for (int i = 0; i < sessionFolders.Length; i++)
            {
                if (sessionFolders[i].LastWriteTime < lastDate)
                    break;

                DirectoryInfo[] sessions = sessionFolders[i].GetDirectories();
                sessionFolders = sessionFolders.OrderByDescending(folder => folder.LastWriteTime).ToArray();

                for (int j = 0; j < sessions.Length; j++)
                {
                    if (sessions[j].LastWriteTime < lastDate)
                        break;

                    DiagSessionController files = new();
                    files.Load(sessions[j]);
                    //await
                    //break;////
                }
                break;////

            }

            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}