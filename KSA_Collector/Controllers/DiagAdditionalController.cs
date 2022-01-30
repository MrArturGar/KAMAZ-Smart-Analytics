using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using KSA_Collector.Models;

namespace KSA_Collector.Controllers
{
    internal class DiagAdditionalController
    {

        public CommonInfo CommonInfo;
        public GlonassLog GlonassLog;

        public DiagAdditionalController(string Path)
        {
            OpenZIP(Path);
        }

        private void OpenZIP(string path)
        {
            using (ZipArchive archive = ZipFile.OpenRead(path))
            {
                var sample = archive.GetEntry("CommonInfo.log");
                if (sample != null)
                    using (StreamReader sr = new StreamReader(sample.Open()))
                    {
                        LoadCommonInfo(sr.ReadToEnd());
                    }

                sample = archive.GetEntry("GlonassLog.log");
                if (sample != null)
                    using (StreamReader sr = new StreamReader(sample.Open()))
                    {
                        LoadGlonassLog(sr.ReadToEnd());
                    }
            }
        }

        private void LoadCommonInfo(string source)
        {
            CommonInfo = new CommonInfo();
            string[] lines = source.Split('\n');
            for (int i = 0; i< lines.Length; i++)
            {
                string[] parts = lines[i].Split(' ');
                switch (parts[3])
                {
                    case "Model:":
                        {
                            CommonInfo.Model = parts[4];
                            break;
                        }
                    case "VIN:":
                        {
                            CommonInfo.VIN = parts[4];
                            break;
                        }
                    case "VIN_old:":
                        {
                            CommonInfo.VIN_old = parts[4];
                            break;
                        }
                    case "ICCID:":
                        {
                            CommonInfo.ICCID = parts[4];
                            break;
                        }
                    case "User:":
                        {
                            CommonInfo.User = parts[4];
                            break;
                        }
                    case "VCINumber:":
                        {
                            CommonInfo.VCINumber = parts[4];
                            break;
                        }
                    case "Mileage:":
                        {
                            CommonInfo.Mileage = Convert.ToDouble(parts[4]);
                            break;
                        }
                    case "VersionDatabase:":
                        {
                            CommonInfo.VersionDatabase = parts[4];
                            break;
                        }
                }
            }
        }
    
        private void LoadGlonassLog(string source)
        {
            GlonassLog = new GlonassLog();
            GlonassLog.Actions = new List<Models.Action>();
            string[] lines = source.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                int startInd = lines[i].IndexOf("<Action");
                string xml = lines[i].Substring(startInd);

                XmlSerializer serializer = new XmlSerializer(typeof(Models.Action));
                Models.Action tmp = (Models.Action) serializer.Deserialize(new StringReader(xml));

                if (tmp != null)
                    GlonassLog.Actions.Add(tmp);
            }
        }
    }
}
