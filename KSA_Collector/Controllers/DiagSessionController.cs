using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using KSA_Collector;
using KSA_Collector.Models;

namespace KSA_Collector.Controllers
{
    internal class DiagSessionController
    {
        DBController DB;
        Settings.IdentificationSettings IdentificationSettings;
        DataHandler Data;
        Tables.Vehicle Vehicle;
        string AdditionalArchivePath;

        public DiagSessionController()
        {
            GetSettings();
        }

        private void GetSettings()
        {
            IdentificationSettings = Settings.IdentificationSettings.GetSettings();
            DB = new DBController(new Tables.KSA_DBContext());
            Data = new DataHandler();
        }

        public void Load(DirectoryInfo _path)
        {

            FileInfo fileSession = _path.GetFiles()[0];
            AdditionalArchivePath = _path.GetFiles()[1].FullName;

            session session = new session();
            XmlSerializer serializer = new XmlSerializer(typeof(session));
            try
            {
                session = (session)serializer.Deserialize(new StreamReader(fileSession.FullName));
                LoadECUs(session);
            }
            catch (Exception ex) { }
        }

        private void LoadECUs(session _session)
        {
            string design_number = _session.machine.networks.id;
            var ecus = _session.machine.networks.ecus;

            Tables.Ecu[] ecus_tables = new Tables.Ecu[ecus.Count()];

            for (int i = 0; i < ecus.Count(); i++)
            {
                var ecu = ecus[i];
                string codifier = ecus[i].id;

                Tables.System system = new Tables.System()
                {
                    Name = Data.GetSystemName(codifier),
                    Domain = Data.GetDomainName(codifier)
                };
                system = DB.GetSystem(system);

                ecus_tables[i] = new Tables.Ecu()
                {
                    Codifier = codifier,
                    System = system
                };
                ecus_tables[i] = DB.GetECU(ecus_tables[i]);

                Tables.Composite composite = new Tables.Composite()
                {
                    DesignNumber = _session.machine.networks.displayName,
                    IdEcuNavigation = ecus_tables[i]
                };
                composite = DB.GetComposite(composite);

                SetIdentifications(ecus[i], ecus_tables[i]);

            }
        }

        private void SetIdentifications(sessionMachineNetworksEcus ecuFile, Tables.Ecu ecu)
        {
            var identifications = ecuFile.identifications;
            string[] signals = IdentificationSettings.GetSignalNames(ecuFile.id);

            for (int i = 0; i < identifications.Length; i++)
            {
                string signalName = identifications[i].id.Split('-')[0];

                if (signals.Contains(signalName))
                {
                    Tables.Identification ident = new Tables.Identification()
                    {
                        Name = signalName,
                        Value = identifications[i].value
                    };
                    ident = DB.GetIdentification(ident);

                    Tables.EcuIdentification ecu_ident = new Tables.EcuIdentification()
                    {
                        IdEcuNavigation = ecu,
                        IdIdentificationsNavigation = ident
                    };
                    ecu_ident = DB.GetEcuIdentification(ecu_ident);
                }
            }
        }

        private Tables.Session LoadSession(session sessionFile, Tables.Vehicle vehicle)
        {
            DiagAdditionalController diag = new DiagAdditionalController(AdditionalArchivePath);
            var session = new Tables.Session()
            {
                IdVehicleNavigation = vehicle,
                SessionsName = sessionFile.id,
                Date = Data.GetSessionDate(sessionFile.id),
                IdServiceCentersNavigation = DB.GetServiceCenter(sessionFile.username),
                VersionDb = diag.CommonInfo.VersionDatabase,
                Vcisn = diag.CommonInfo.VCINumber,
                Mileage = diag.CommonInfo.Mileage
            };
            session = DB.GetSession(session);

            SetProcedureReport(session, sessionFile.machine.testResults);
            SetAoglonassReport(session, diag.GlonassLog);

            return session;
        }

        private void SetAoglonassReport(Tables.Session session, Models.GlonassLog glonassLog)
        {
            for (int i = 0; i < glonassLog.Actions.Count; i++)
            {
                var action = glonassLog.Actions[i];
                Tables.AoglonassReport report = new()
                {
                    IdSessionNavigation = session,
                    DateStart =  Data.GetAOGlonassDate(action.DateTime),
                    Type = action.Type,
                    Request = action.Request,
                    Response = action.ResponseTable
                };
                DB.SaveAoglonassReport(report);
            }
        }

        private void SetProcedureReport(Tables.Session session, sessionMachineTestResults procedures)
        {
            for (int i = 0;i < procedures.Tests.Length;i++)
            {
                var test = procedures.Tests[i];
                Tables.ProcedureReport report = new()
                {
                    IdSessionNavigation = session,
                    Codifier = Data.GetProcedureECUCodifier(procedures.id),
                    Type = Data.GetProcedureECUType(procedures.id),
                    Name = test.name,
                    Result = test.result,
                    DateStart = Data.GetAOGlonassDate(test.timestampStart),
                    DateEnd = Data.GetAOGlonassDate(test.timestamp),
                    UsingVin = String.IsNullOrEmpty(test.VINForFlash)?test.VIN:test.VINForFlash,
                    DataFiles = test.flashDataFile
                };
                DB.SaveProcedureReport(report);
            }
        }

    }
}
