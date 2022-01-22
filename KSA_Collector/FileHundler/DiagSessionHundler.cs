using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using KSA_Collector;

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

            session session = new session();
            XmlSerializer serializer = new XmlSerializer(typeof(session));
            try
            {
                session = (session)serializer.Deserialize(new StreamReader(fileSession.FullName));
                ECU_Handler(session);
            }
            catch (Exception ex) { }
        }

        private void ECU_Handler(session _session)
        {
            DataHandler dataHandler = new DataHandler();   
            string design_number = _session.machine.networks.id;
            var ecus = _session.machine.networks.ecus;

            Tables.ECU[] ecus_tables = new Tables.ECU[ecus.Count()];

            for (int i = 0; i < ecus.Count(); i++)
            {
                Tables.ApplicationContext applicationContext = new Tables.ApplicationContext();
                var ecu = ecus[i];
                Tables.System systems = new Tables.System()
                {
                    Name = dataHandler.GetSystemName(ecu.id),
                    Domain = dataHandler.GetSystemName(ecu.id)
                };
                applicationContext.Systems.Add(systems);
                applicationContext.SaveChanges();

                ecus_tables[i] = new Tables.ECU()
                {
                    Codifier = ecu.id,
                    System_id = systems.id
                };
            }


        }
    }
}
