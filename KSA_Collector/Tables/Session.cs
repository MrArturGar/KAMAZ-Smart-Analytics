using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSA_Collector.Tables
{
    internal class Session
    {
        int id;/// 
        int id_Vehicle;
        string VIN_old;
        DateTime Date;
        int id_serviceCenters;
        string VersionDB;
        string VCISN;
        float Mileage;
        bool Has_Identifications = false;
        bool Has_DTC = false;
        bool Has_Tests = false;
        bool Has_Flash = false;
        string FilePath;
        byte[] Hash;

        public Session(string _path)
        {
            XDocument doc = XDocument.Load(_path);
            XElement node = doc.Root;
            if (node != null && node.Name == "session")
            {
                string username = node.Element("username").Value;

                ServiceCenters serviceCenters = new ServiceCenters(username);
                id_serviceCenters = serviceCenters.GetId_ServiceCenters();

                XElement machine = node.Elements("machine").First();

                GetData(machine);
            }
            else
                throw new Exception("Xml node is NULL or invalid");
        }

        private void GetData(XElement _machine)
        {
            string id_truck = _machine.Element("id").Value;
            string displayName = _machine.Element("displayName").Value;

            XElement[] ecus = _machine.Elements("ecus").ToArray();
            for (int i = 0; i < ecus.Length; i++)
            {
                ECU ecu = new ECU(ecus[i]);
            }
        }
    }
}
