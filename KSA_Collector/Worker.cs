using KSA_Collector.Settings;
using KSA_Collector.Controllers;
using System.Xml.Serialization;

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
            watch();
            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private void StartCollector()
        {
            try
            {
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

                    _logger.LogInformation($"{DateTimeOffset.Now.ToString("HH:mm:ss")}: ({i + 1}/{countFolder}) {sessionFolders[i].Name} Started...");

                    int countSessions = sessions.Length;
                    for (int j = 0; j < countSessions; j++)
                    {
                        if (sessions[j].LastWriteTime < lastDate)
                            break;

                        _logger.LogInformation($"{DateTimeOffset.Now.ToString("HH:mm:ss")}: ({i + 1}/{countFolder}) - ({j + 1}/{countSessions}) {sessionFolders[i].Name}");

                        System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch();
                        swatch.Start();

                        DiagSessionController files = new();
                        files.Load(sessions[j]);


                        swatch.Stop();
                        _logger.LogInformation($"{DateTimeOffset.Now.ToString("HH:mm:ss")}: Timer: {swatch.Elapsed}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                File.WriteAllText(Environment.CurrentDirectory + DateTime.Now.ToString("yyyyMMddhhmm") + "_FATAL.log", ex.ToString());
            }
        }

        private void watch()
        {
            ServiceSettings settings = ServiceSettings.GetSettings();
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = settings.SessionPath;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            StartCollector();
        }
    }
}