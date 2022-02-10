using System;
using System.Collections.Generic;
using System.Globalization;
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
            if (path != null)
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
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "")
                    continue;

                string[] parts = lines[i].Split(' ');
                switch (parts[3])
                {
                    case "Model:":
                        {
                            CommonInfo.Model = parts[4] != "" ? parts[4] : null;
                            break;
                        }
                    case "VIN:":
                        {
                            CommonInfo.VIN = parts[4] != "" ? parts[4] : null;
                            break;
                        }
                    case "VIN_old:":
                        {
                            CommonInfo.VIN_old = parts[4] != "" ? parts[4] : null;
                            break;
                        }
                    case "ICCID:":
                        {
                            CommonInfo.ICCID = parts[4] != "" ? parts[4] : null;
                            break;
                        }
                    case "User:":
                        {
                            CommonInfo.User = parts[4] != "" ? parts[4] : null;
                            break;
                        }
                    case "VCINumber:":
                        {
                            CommonInfo.VCINumber = parts[4] != "" ? parts[4] : null;
                            break;
                        }
                    case "Mileage:":
                        {
                            if (parts[4] != "")
                                CommonInfo.Mileage = Convert.ToDouble(parts[4], new NumberFormatInfo { NumberDecimalSeparator = "." });
                            else
                                CommonInfo.Mileage = null;
                            break;
                        }
                    case "VersionDatabase:":
                        {
                            CommonInfo.VersionDatabase = parts[4] != "" ? parts[4] : null;
                            break;
                        }
                }
            }
        }

        private void LoadGlonassLog(string source)
        {
            GlonassLog = new GlonassLog();
            string[] lines = source.Split('\n');
            List<Models.GlonassLogAction> action = new();
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "")
                    continue;

                int startInd = lines[i].IndexOf("<Action");
                string xml = lines[i].Substring(startInd);

                XmlSerializer serializer = new XmlSerializer(typeof(Models.GlonassLogAction));
                Models.GlonassLogAction tmp = (Models.GlonassLogAction)serializer.Deserialize(new StringReader(xml));

                if (tmp != null)
                    action.Add(tmp);
            }
            GlonassLog.Action = action.ToArray();
        }

        public string GetGlonassActionResponseParam(GlonassLogActionResponse response)
        {
            if (!string.IsNullOrEmpty(response.Iccid))
                return response.Iccid;

            if (!string.IsNullOrEmpty(response.Vin))
                return response.Vin;

            return response.RequestId;
        }

        public string GetGlonassActionResponseStatus(GlonassLogActionResponse response)
        {
            if (!string.IsNullOrEmpty(response.Status))
                return response.Status;

            return response.RequestProcessingStatus;
        }
    }
}
