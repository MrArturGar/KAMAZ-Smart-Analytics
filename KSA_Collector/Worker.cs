using KSA_Collector.Settings;
using KSA_Collector.Controllers;
using System.Xml.Serialization;
using Microsoft.Extensions.Hosting;

namespace KSA_Collector
{
    public class Worker : IHostedService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }
        FileSystemWatcher watcher;
        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Wait API-Server (5 sec)");
            Thread.Sleep(5000);

            ServiceSettings settings = ServiceSettings.GetSettings();
            watcher = new FileSystemWatcher();
            watcher.Path = settings.SessionPath;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.IncludeSubdirectories = true;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Error += new ErrorEventHandler(OnError);
            watcher.EnableRaisingEvents = true;
            FirstStart();

            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            if (watcher != null)
            {
                watcher.Dispose();
            }
            return Task.CompletedTask;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            MainController main = null;
            try
            {
                System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                _logger.LogInformation("+ch: " + e.Name + "\t" + swatch.Elapsed);
                main = new MainController();
                main.LoadSessionFolders(new DirectoryInfo(e.FullPath));
                swatch.Stop();
                _logger.LogInformation("-ch: " + e.Name + "\t" + swatch.Elapsed);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
            }
            finally
            {
                if (main != null)
                    main.Dispose();
            }

        }
        private void OnError(object source, ErrorEventArgs e)
        {
            _logger.LogCritical(e.GetException().ToString());
        }

        private void FirstStart()
        {
            MainController main = null;
            try
            {
                string tempPath = Environment.CurrentDirectory + "\\Temp\\.continue";

                if (!File.Exists(tempPath))
                {
                    System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch();
                    swatch.Start();
                    _logger.LogInformation("+First: " + "\t" + swatch.Elapsed);
                    main = new MainController();
                    main.StartAllInsert();
                    File.Create(tempPath);
                    swatch.Stop();
                    _logger.LogInformation("-First: " + "\t" + swatch.Elapsed);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
            }
            finally
            {
                if (main != null)
                    main.Dispose();
            }
        }
    }
}