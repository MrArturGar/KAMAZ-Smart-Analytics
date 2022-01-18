using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using KSA_Collector.Tables;
using KSA_Collector.Settings;

namespace KSA_Collector.FileHundler
{
    internal class DiagSessionHundler
    {
        public DiagSessionHundler(DirectoryInfo _path)
        {
            Load(_path);
        }

        private void Load(DirectoryInfo _path)
        {

            FileInfo fileSession = _path.GetFiles()[0];

            session sessionFile = new session();
            XmlSerializer serializer = new XmlSerializer(typeof(session));
            try
            {
                sessionFile = (session)serializer.Deserialize(new StreamReader(fileSession.FullName));


            }
            catch (Exception ex) { }

        }

        private void LoadTable_ECUs_and_Vehicle(sessionMachineNetworks _network)
        {
            IdentificationSettings settings = IdentificationSettings.GetSettings();

            int ecuCount = _network.ecus.Length;
            ECU[] ecu = new ECU[ecuCount-1];


            for (int i = 0; i < ecuCount; i++)
            {
                ecu[i].Codifier = _network.ecus[i].id;

                string[] signalNames = settings.GetSignalNames(ecu[i].Codifier);

                Identifications[] identies = _network.ecus[i].identifications;

                for (int j = 0; j < identies.Length; j++)
                {
                    string signalName = identies[j].id.Split('-')[0];
                    if (signalNames.Contains(signalName))
                        ecu[i].identifications.Add(signalName, identies[j].value);
                }
            }
        }
    }
}
