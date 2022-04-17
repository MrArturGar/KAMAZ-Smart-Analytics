using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KSA_Collector.Controllers
{
    internal class LicWatcherController
    {
        string TempFile;
        string LastLine="";
        string SessionsPath;
        ILogger _logger;
        public LicWatcherController(string pathLog, string sessionsPath, ILogger logger)
        {
            TempFile = AppDomain.CurrentDomain.BaseDirectory + "\\Temp\\tempLog.xml";
            File.Copy(pathLog, TempFile, true);
            SessionsPath = sessionsPath;
            _logger = logger;
        }


        public void ReadNewSessions()
        {
            using (StreamReader sr = new StreamReader(TempFile))
            {
                ParserController main = new ParserController(_logger);
                string line;
                string startLine = "";
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(LastLine) == false|| LastLine=="")
                    {
                        string searchText = @"[CDATA[persistVehicleSessionFile: request=/flashserver_kamaz/postVehicleSession/";

                        if (line.Contains(searchText))
                        {
                            string[] sessionParams = line.Split(searchText)[1]
                                .Split('/');

                            string fullPath = SessionsPath + sessionParams[0] + "\\" + sessionParams[1] + "\\";

                            if (Directory.Exists(fullPath))
                            {////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                main.LoadSessionFolders(new DirectoryInfo(fullPath));
                                _logger.LogInformation("ReadNewSessions: " + sessionParams[1]);
                            }
                        }


                        if (startLine == "")
                        {
                            startLine = line.Trim();
                        }
                    }
                    else
                    {
                        LastLine = startLine;
                        break;
                    }
                }
            }
        }
    }
}
