using KSA_Collector.Settings;
using KSA_Collector.Controllers;
using System.Xml.Serialization;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Timers;

namespace KSA_Collector
{
    public class Collector : IHostedService
    {
        private readonly ILogger<Collector> _logger;
        FileSystemWatcher _watcher;
        LicWatcherController _licWatcher;
        System.Timers.Timer _timer;

        public Collector(ILogger<Collector> logger)
        {
            _logger = logger;
        }
        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Wait API-Server (5 sec)");
            Thread.Sleep(5000);

            ServiceSettings settings = ServiceSettings.GetSettings();
            _licWatcher = new LicWatcherController(settings.LicLogFile, settings.SessionPath, _logger);

            FileInfo licLog = new FileInfo(settings.LicLogFile);
            _watcher = new FileSystemWatcher();
            _watcher.Path = licLog.DirectoryName;
            _watcher.NotifyFilter = NotifyFilters.LastWrite;
            _watcher.IncludeSubdirectories = true;
            _watcher.Filter = "*.*";
            _watcher.Changed += new FileSystemEventHandler(OnChanged);
            _watcher.Error += new ErrorEventHandler(OnError);
            _watcher.EnableRaisingEvents = true;

            _timer = new System.Timers.Timer(5000);
            _timer.Elapsed += CheckByTimer;
            //_timer.AutoReset = true;
            _timer.Enabled = true;

            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            if (_watcher != null)
            {
                _watcher.Dispose();
            }

            if (_timer != null) 
            {
                _timer.Stop();
                _timer.Dispose(); 
            }
            return Task.CompletedTask;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            _logger.LogInformation($"OnChange: Log file changed.");
            try
            {
                Stopwatch swatch = new Stopwatch();
                swatch.Start();
                _licWatcher.ReadNewSessions();
                swatch.Stop();
                _logger.LogInformation("OnChange: " + e.Name + "\t" + swatch.Elapsed);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("OnChange: " + ex.ToString());
            }

        }
        private void OnError(object source, ErrorEventArgs e)
        {
            _logger.LogCritical("Watcher_OnError: " + e.GetException().ToString());
        }
        private void CheckByTimer(object source, ElapsedEventArgs e)
        {
            _timer.Stop();
            ParserController main = null;

            try
            {
                string tempPath = AppDomain.CurrentDomain.BaseDirectory + "Temp\\.continue";
                ServiceSettings settings = ServiceSettings.GetSettings();

                var dtNow = DateTime.Now;

                if (dtNow.DayOfWeek == settings.DayOfWeekToCheck && dtNow.Hour == settings.HourOfDayToCheck)
                {
                    var dateFile = File.GetLastWriteTime(tempPath);
                    if (dateFile.Day != dtNow.Day)
                    File.Delete(tempPath);
                }


                if (!File.Exists(tempPath))
                {
                    _logger.LogInformation("CheckByTimer: Started.");
                    Stopwatch swatch = new Stopwatch();
                    swatch.Start();
                    main = new ParserController(_logger);
                    main.StartAllInsert();
                    File.Create(tempPath).Dispose();
                    swatch.Stop();
                    _logger.LogInformation("CheckByTimer: " + swatch.Elapsed);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical("CheckByTimer: " + ex.ToString());
            }
            finally
            {
                if (main != null)
                {
                    main.Dispose();
                }
                _timer.Start();
            }
        }
    }
}