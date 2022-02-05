using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using KSA_Collector;

namespace KSA_Collector.Controllers
{
    internal class DiagSessionController
    {
        DBController DB;
        Settings.IdentificationSettings IdentificationSettings;
        DataHandler Data;
        Tables.Vehicle Vehicle = new();
        Tables.Session Session = new();
        List<Tables.SessionEcuidentification> sessionEcuidentification = new();
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
            UpdateServiceCenters();
            FileInfo[] files = _path.GetFiles();
            string fileSession = null;

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].FullName.Contains(".session"))
                    fileSession = files[i].FullName;
                else if (files[i].FullName.Contains(".zip"))
                    AdditionalArchivePath = files[i].FullName;
            }

            if (fileSession != null)
            {
                Models.session session;
                XmlSerializer serializer = new XmlSerializer(typeof(Models.session));

                session = (Models.session)serializer.Deserialize(new StreamReader(fileSession));
                LoadECUs(session);
                LoadVehicle(session);
                LoadSession(session);
            }
        }

        private void UpdateServiceCenters()
        {
            ServiceCentersControllers controller = new ServiceCentersControllers();

            controller.CheckValidTable();
        }

        private void LoadECUs(Models.session session)
        {
            string design_number = session.machine.networks.id;
            var ecus = session.machine.networks.ecus;

            if (ecus != null)
            {
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
                        DesignNumber = session.machine.networks.displayName,
                        IdEcuNavigation = ecus_tables[i]
                    };
                    DB.SaveComposite(composite);

                    SetEcuIdentifications(ecus[i], ecus_tables[i]);

                }
            }
        }

        private void SetEcuIdentifications(Models.sessionMachineNetworksEcus ecuFile, Tables.Ecu ecu)
        {
            var identifications = ecuFile.identifications;

            if (ecuFile.dtcs != null)
                Session.HasDtc = true;

            if (identifications != null)
            {
                Session.HasIdentifications = true;
                string[] signals = IdentificationSettings.GetSignalNames(ecuFile.id);
                bool hasTable = IdentificationSettings.HasEcuNameForTables(ecuFile.id);

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
                        sessionEcuidentification.Add(new Tables.SessionEcuidentification() { IdEcuidentificationsNavigation = ecu_ident });
                    }

                    if (hasTable)
                    {
                        LoadIdentificationFromTable(ecuFile.id, identifications[i]);
                    }
                }
            }
        }

        private void LoadIdentificationFromTable(string codifier, Models.Identification identification)
        {
            var signal = IdentificationSettings.GetColumnSignal(codifier, identification.id.Split('-')[0]);

            if (signal != null)
            {
                if (signal.TableName == "Vehicles")
                {
                    Type examType = Vehicle.GetType();
                    PropertyInfo pinfo = examType.GetProperty(signal.ColumnName);
                    if (pinfo != null)
                    {
                        pinfo.SetValue(Vehicle, Convert.ChangeType(identification.value, pinfo.PropertyType));
                    }
                }
                else if (signal.TableName == "Sessions")
                {
                    Type examType = Session.GetType();
                    PropertyInfo pinfo = examType.GetProperty(signal.ColumnName);
                    if (pinfo != null)
                    {
                        pinfo.SetValue(Vehicle, Convert.ChangeType(identification.value, pinfo.PropertyType));
                    }
                }
            }
        }

        private void LoadSession(Models.session sessionFile)
        {
            DiagAdditionalController diag = new DiagAdditionalController(AdditionalArchivePath);

            string username;
            if (sessionFile.username != null)
                username = sessionFile.username;
            else if (sessionFile.sessionInfo != null)
                username = sessionFile.sessionInfo.username;
            else
                username = "";


            Session.IdVehicleNavigation = Vehicle;
            Session.SessionsName = sessionFile.id;
            Session.Date = Data.GetSessionDate(sessionFile.id);
            Session.IdServiceCentersNavigation = DB.GetServiceCenter(username);
            if (diag.CommonInfo != null)
            {
                Session.VersionDb = diag.CommonInfo.VersionDatabase;
                Session.Vcisn = diag.CommonInfo.VCINumber;
                Session.Mileage = diag.CommonInfo.Mileage;
            }
            Session = DB.GetSession(Session);


            SetProcedureReport(Session, sessionFile.machine.testResults);
            SetAoglonassReport(Session, diag);

            if (sessionEcuidentification.Count != 0)
                LoadSessionEcuIdentification();
            else
                DB.SaveSession(Session);
        }

        private void LoadSessionEcuIdentification()
        {
            for (int i = 0; i < sessionEcuidentification.Count; i++)
            {
                sessionEcuidentification[i].IdSessionNavigation = Session;
                DB.SaveSessionEcuIdentification(sessionEcuidentification[i]);
            }
        }

        private void LoadVehicle(Models.session session)
        {
            Vehicle.DesignNumber = session.machine.networks.displayName;
            Vehicle.Type = session.machine.networks.id;

            if (Vehicle.Vin == null)
                Vehicle.Vin = Data.GetSessionVin(session.id);

            Vehicle = DB.GetVehicle(Vehicle);
        }

        private void SetAoglonassReport(Tables.Session session, DiagAdditionalController diag)
        {
            var glonassLog = diag.GlonassLog;
            if (glonassLog != null)
            {
                for (int i = 0; i < glonassLog.Action.Length; i++)
                {
                    var action = glonassLog.Action[i];
                    for (int j = 0; j < action.ResponseTable.Length; j++)
                    {
                        Tables.AoglonassReport report = new()
                        {
                            IdSessionNavigation = session,
                            DateStart = Data.GetAOGlonassDate(action.DateTime),
                            Action = action.Type,
                            ParamText = diag.GetGlonassActionResponseParam(action.ResponseTable[j]),
                            Status = diag.GetGlonassActionResponseStatus(action.ResponseTable[j])
                        };
                        DB.SaveAoglonassReport(report);
                    }
                }
            }
        }

        private void SetProcedureReport(Tables.Session session, Models.sessionMachineTestResults[] procedures)
        {
            if (procedures != null)
            {
                for (int j = 0; j < procedures.Length; j++)
                {
                    if (procedures[j].Tests != null)
                        for (int i = 0; i < procedures[j].Tests.Length; i++)
                        {
                            string codifier = Data.GetProcedureECUCodifier(procedures[j].id);

                            Tables.System system = new()
                            {
                                Name = Data.GetSystemName(codifier),
                                Domain = Data.GetDomainName(codifier)
                            };
                            system = DB.GetSystem(system);

                            Tables.Ecu ecu = new()
                            {
                                Codifier = codifier,
                                System = system
                            };
                            ecu = DB.GetECU(ecu);

                            string type = Data.GetProcedureECUType(procedures[j].id);
                            var test = procedures[j].Tests[i];
                            Tables.ProcedureReport report = new()
                            {
                                IdSessionNavigation = session,
                                IdEcuNavigation = ecu,
                                Type = type,
                                Name = test.name,//////
                                Result = test.result == "true" ? true : false,
                                DateStart = Data.GetAOGlonassDate(test.timestampStart),
                                DateEnd = Data.GetAOGlonassDate(test.timestamp),
                                UsingVin = String.IsNullOrEmpty(test.VINForFlash) ? test.VIN : test.VINForFlash,
                                DataFiles = test.flashDataFile
                            };
                            DB.SaveProcedureReport(report);

                            if (type == "Flash")
                                Session.HasFlash = true;
                            else if (type == "Test")
                                Session.HasTests = true;
                        }
                }
            }
        }

    }
}
