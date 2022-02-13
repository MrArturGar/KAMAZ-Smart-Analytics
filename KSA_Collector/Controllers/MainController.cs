using KSA_Collector.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace KSA_Collector.Controllers
{
    internal class MainController :IDisposable
    {

        private string IsFirstTemp = Environment.CurrentDirectory + "\\Temp\\.continue";
        private DateTime? LastDate;

        internal void StartAllInsert()
        {
            ServiceSettings settings = ServiceSettings.GetSettings();
            LastDate = settings.lastDays != 0 ? DateTime.Now.AddDays(-settings.lastDays) : new DateTime();

            DirectoryInfo[] sessionFolders = new DirectoryInfo(settings.SessionPath).GetDirectories();
            sessionFolders = sessionFolders.OrderByDescending(folder => folder.LastWriteTime).ToArray();

            int countFolder = sessionFolders.Length;

            for (int i = 0; i < countFolder; i++)
            {
                if (sessionFolders[i].LastWriteTime < LastDate)
                    break;

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
                if (sessions[j].LastWriteTime < LastDate)
                    break;

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
            IsFirstTemp = null;
            LastDate = null;
        }
    }

}
