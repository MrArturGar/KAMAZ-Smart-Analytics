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
        Tables.KSA_DBContext ApplicationContext;
        Settings.IdentificationSettings IdentificationSettings;

        public DiagSessionHundler(DirectoryInfo _path)
        {
            Load(_path);
            GetSettings();
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

        private void GetSettings()
        {
            Settings.IdentificationSettings identificationSettings = Settings.IdentificationSettings.GetSettings();
        }

        private void ECU_Handler(session _session)
        {
            ApplicationContext = new Tables.KSA_DBContext();
            DataHandler dataHandler = new DataHandler();
            string design_number = _session.machine.networks.id;
            var ecus = _session.machine.networks.ecus;

            Tables.Ecu[] ecus_tables = new Tables.Ecu[ecus.Count()];

            for (int i = 0; i < ecus.Count(); i++)
            {
                var ecu = ecus[i];
                string codifier = ecus[i].id;

                Tables.System system = new Tables.System()
                {
                    Name = dataHandler.GetSystemName(codifier),
                    Domain = dataHandler.GetDomainName(codifier)
                };
                system = SaveSystem(system);


                ecus_tables[i] = new Tables.Ecu()
                {
                    Codifier = codifier,
                    SystemId = system.Id
                };
                ecus_tables[i] = SaveEcu(ecus_tables[i]);

                Tables.Composite composite = new Tables.Composite()
                {
                    DesignNumber = _session.machine.networks.displayName,
                    IdEcu = ecus_tables[i].Id
                };
                composite = SaveComposite(composite);

                Identification_Handler(ecus[i], ecus_tables[i]);

            }
        }

        private void Identification_Handler(sessionMachineNetworksEcus ecuFile, Tables.Ecu ecu)
        {
            var identifications = ecuFile.identifications;
            string[] signals = IdentificationSettings.GetSignalNames(ecuFile.id);

            for (int i = 0; i< identifications.Length;i++)
            {
                string signalName = identifications[i].id.Split('-')[0];

                if (signals.Contains(signalName))
                {
                    Tables.Identification ident = new Tables.Identification()
                    {
                        Name = signalName,
                        Value = identifications[i].value
                    };
                    ident = SaveIdentification(ident);

                    Tables.EcuIdentification ecu_ident = new Tables.EcuIdentification()
                    {
                        IdEcu = ecu.Id,
                        IdIdentifications = ident.Id
                    };
                    ecu_ident = SaveEcuIdentification(ecu_ident);


                }
            }
        }



        private Tables.System SaveSystem(Tables.System system)
        {
            try
            {
                ApplicationContext.Systems.Add(system);
                ApplicationContext.SaveChanges();
                return system;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                return ApplicationContext.Systems
        .Where(c => c.Name == system.Name)
        .Single();
            }
        }

        private Tables.Ecu SaveEcu(Tables.Ecu ecu)
        {
            try
            {
                ApplicationContext.Ecus.Add(ecu);
                ApplicationContext.SaveChanges();
                return ecu;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                return ApplicationContext.Ecus
        .Where(c => c.Codifier == ecu.Codifier)
        .Single();
            }
        }

        private Tables.Composite SaveComposite(Tables.Composite composite)
        {
            try
            {
                ApplicationContext.Composites.Add(composite);
                ApplicationContext.SaveChanges();
                return composite;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                return ApplicationContext.Composites
        .Where(c => c.DesignNumber == composite.DesignNumber && c.IdEcu == composite.IdEcu)
        .Single();
            }
        }

        private Tables.Identification SaveIdentification(Tables.Identification identification)
        {
            /////////////////////////
            Tables.Identification ident = ApplicationContext.Identifications
        .Where(c => c.Name == identification.Name && c.Value == identification.Value)
        .Single();

            if (ident == null) { 
                ApplicationContext.Identifications.Add(identification);
                ApplicationContext.SaveChanges();
                return identification;
            }
            else
            {
                return ident;
            }
        }

        private Tables.EcuIdentification SaveEcuIdentification(Tables.EcuIdentification _ecuIdent)
        {
            /////////////////////////
            Tables.EcuIdentification ecuIdent = ApplicationContext.EcuIdentifications
        .Where(c => c.IdEcu == _ecuIdent.IdEcu && c.IdIdentifications == _ecuIdent.IdIdentifications)
        .Single();

            if (ecuIdent == null)
            {
                ApplicationContext.EcuIdentifications.Add(ecuIdent);
                ApplicationContext.SaveChanges();
                return _ecuIdent;
            }
            else
            {
                return ecuIdent;
            }
        }



    }
}
