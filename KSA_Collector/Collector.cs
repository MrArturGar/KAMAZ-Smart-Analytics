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

            _timer = new System.Timers.Timer(5000);
            _timer.Elapsed += CheckByTimer;
            //_timer.AutoReset = true;
            _timer.Enabled = true;
            _timer.Start();

            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            if (_timer != null) 
            {
                _timer.Stop();
                _timer.Dispose(); 
            }
            return Task.CompletedTask;
        }

        private void OnError(object source, ErrorEventArgs e)
        {
            _logger.LogCritical("Watcher_OnError: " + e.GetException().ToString());
        }
        private void CheckByTimer(object source, ElapsedEventArgs e)
        {
            _timer.Stop();
            ParserController  Parser = new ParserController(_logger);

            try
            {
                string tempContinuePath = AppDomain.CurrentDomain.BaseDirectory + "Temp\\.continue";
                ServiceSettings settings = ServiceSettings.GetSettings();

                var dtNow = DateTime.Now;

                if (dtNow.DayOfWeek == settings.DayOfWeekToCheck && dtNow.Hour == settings.HourOfDayToCheck)
                {
                    var dateFile = File.GetLastWriteTime(tempContinuePath);
                    if (dateFile.Day != dtNow.Day)
                    File.Delete(tempContinuePath);
                }


                if (!File.Exists(tempContinuePath))
                {
                    _logger.LogInformation("CheckByTimer: Started.");
                    Stopwatch swatch = new Stopwatch();
                    swatch.Start();
                    Parser.StartAllInsert();
                    File.Create(tempContinuePath).Dispose();
                    swatch.Stop();
                    _logger.LogInformation("CheckByTimer: " + swatch.Elapsed);
                }

                if (_licWatcher.LogFileChanged())
                {
                    _licWatcher.ReadNewSessions(Parser);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical("CheckByTimer: " + ex.ToString());
            }
            finally
            {
                if (Parser != null)
                {
                    Parser.Dispose();
                }
                _timer.Start();
            }
        }
    }
}