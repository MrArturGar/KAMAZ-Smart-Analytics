using KSA_Collector.Handlers;

namespace KSA_Collector.Controllers
{
    internal class LicWatcherController
    {
        string PathTempLogFile;
        string PathSourceLogFile;
        string LastLine = "";
        string SessionsPath;//
        static DateTime LastWriteTime;
        ILogger _logger;
        public LicWatcherController(string pathLog, string sessionsPath, ILogger logger)
        {
            PathTempLogFile = AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\tempLog.xml";
            //File.Copy(pathLog, TempFile, true);
            PathSourceLogFile = pathLog;
            SessionsPath = sessionsPath;
            _logger = logger;
        }


        public void ReadNewSessions(ParserController parser)
        {
            string startLine = "";
            string markerLine = @"<log4j:message><![CDATA[persistVehicleSessionFile: request=/flashserver_kamaz/postVehicleSession/";
            using (FileLogHandler getFileReverse = new FileLogHandler(PathTempLogFile))
            {
                string line = "";

                while ((line = getFileReverse.ReadLine()) != null)
                {
                    if (LastLine != "" && line.Contains(LastLine))
                        break;


                    //Запись только первой строки
                    if (!string.IsNullOrEmpty(line) && startLine == "")
                    {
                        if (line.Contains(@"<log4j:event"))
                            startLine = line.Trim();
                    }

                    if (line.Contains(markerLine))
                    {
                        string[] sessionParams = line.Split('/');

                        string fullPath = SessionsPath + sessionParams[3] + "\\" + sessionParams[4] + "\\";
                        parser.LoadSessionFolders(new DirectoryInfo(fullPath));
                    }

                }
                LastLine = startLine;
            }
        }

        public bool LogFileChanged()
        {
            string TempFile = AppDomain.CurrentDomain.BaseDirectory + "Log.temp";

            DateTime lastWriteTimeSource = File.GetLastWriteTime(PathSourceLogFile);

            if (lastWriteTimeSource > LastWriteTime)
            {
                LastWriteTime = lastWriteTimeSource;

                File.Copy(PathSourceLogFile, PathTempLogFile, true);
                return true;
            }
            return false;
        }
    }
}
