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

            Tables.Ecu[] ecus_tables = new Tables.Ecu[ecus.Count()];

            for (int i = 0; i < ecus.Count(); i++)
            {
                Tables.KSA_DBContext applicationContext = new Tables.KSA_DBContext();
                var ecu = ecus[i];
                string codifier = dataHandler.RemoveDCPrefix(ecus[i].id);

                Tables.System system = new Tables.System()
                {
                    Name = dataHandler.GetSystemName(codifier),
                    Domain = dataHandler.GetDomainName(codifier)
                };

                try
                {
                    applicationContext.Systems.Add(system);
                    applicationContext.SaveChanges();
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
                {
                    system = applicationContext.Systems
            .Where(c => c.Name == system.Name)
            .Single();
                }


                ecus_tables[i] = new Tables.Ecu()
                {
                    Codifier = codifier,
                    SystemId = system.Id
                };

                try
                {
                    applicationContext.Ecus.Add(ecus_tables[i]);
                    applicationContext.SaveChanges();
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
                {
                    ecus_tables[i] = applicationContext.Ecus
            .Where(c => c.Codifier == ecus_tables[i].Codifier)
            .Single();
                }


            }


        }
    }
}
