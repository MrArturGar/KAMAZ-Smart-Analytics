using KSA_Collector.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace KSA_Collector.Controllers
{
    internal class ParserController :IDisposable
    {
        private DateTime? _lastDate;
        private ServiceSettings _settings;
        ILogger _logger;

        public ParserController(ILogger logger)
        {
            _logger = logger;
            _settings = ServiceSettings.GetSettings();
            _lastDate = _settings.CheckLastDaysSession != 0 ? DateTime.Now.AddDays(-_settings.CheckLastDaysSession) : new DateTime();
        }

        internal void StartAllInsert()
        {

            DirectoryInfo[] sessionFolders = new DirectoryInfo(_settings.SessionPath).GetDirectories();
            sessionFolders = sessionFolders.OrderByDescending(folder => folder.LastWriteTime).ToArray();

            int countFolder = sessionFolders.Length;

            for (int i = 0; i < countFolder; i++)
            {
                if (sessionFolders[i].LastWriteTime < _lastDate)
                    break;

                _logger.LogInformation($"SessionDirectory: {countFolder}/{i+1} {sessionFolders[i].Name}");
                LoadSessionFolders(sessionFolders[i]);
            }
        }

        internal void LoadSessionFolders(DirectoryInfo folder)
        {
            DirectoryInfo[] sessions = folder.GetDirectories();
            sessions = sessions.OrderByDescending(folder => folder.LastWriteTime).ToArray();

            int countSessions = sessions.Length;
            for (int j = 0; j < countSessions; j++)
            {
                if (sessions[j].LastWriteTime < _lastDate)
                    break;

                _logger.LogInformation($"LoadSession: {countSessions}/{j+1} {sessions[j].Name}");
                LoadSession(sessions[j].FullName);
            }
        }

        private void LoadSession(string path)
        {
            DiagSessionController files = new();
            files.Load(Directory.GetFiles(path));
            files.Dispose();
        }

        public void Dispose()
        {
            GC.Collect();
            _lastDate = null;
        }
    }

}
