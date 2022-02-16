using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using KSA_Collector;
using KSA_Collector.Models;
using TableModelLibrary;

namespace KSA_Collector.Controllers
{
    internal class DiagSessionController :IDisposable
    {
        DBController DB;
        Settings.IdentificationSettings IdentificationSettings;
        DataHandler Data;
        Vehicle Vehicle = new();
        Session Session = new();
        List<SessionEcuidentification> sessionEcuidentification = new();
        string AdditionalArchivePath;

        public DiagSessionController()
        {
            GetSettings();
        }
        public void Dispose()
        {
            if (DB != null)
            {
                DB.Dispose();
                DB = null;
            }
        }

        private void GetSettings()
        {
            UpdateServiceCenters();
            IdentificationSettings = Settings.IdentificationSettings.GetSettings();
            DB = new DBController();
            Data = new DataHandler();
        }

        public void Load(string[] files)
        {
            string fileSession = null;

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Contains(".session"))
                    fileSession = files[i];
                else if (files[i].Contains(".zip"))
                    AdditionalArchivePath = files[i];
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
                Ecu[] ecus_tables = new Ecu[ecus.Count()];

                for (int i = 0; i < ecus.Count(); i++)
                {
                    var ecu = ecus[i];
                    string codifier = ecus[i].id;

                    ControlSystem system = new()
                    {
                        Name = Data.GetSystemName(codifier),
                        Domain = Data.GetDomainName(codifier)
                    };
                    system = DB.GetControlSystem(system);

                    ecus_tables[i] = new Ecu()
                    {
                        Codifier = codifier,
                        IdControlSystemNavigation = system
                    };
                    ecus_tables[i] = DB.GetECU(ecus_tables[i]);

                    Composite composite = new Composite()
                    {
                        DesignNumber = session.machine.networks.displayName,
                        IdEcuNavigation = ecus_tables[i]
                    };
                    DB.SaveComposite(composite);

                    SetEcuIdentifications(ecus[i], ecus_tables[i]);

                }
            }
        }

        private void SetEcuIdentifications(sessionMachineNetworksEcus ecuFile, Ecu ecu)
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
                        Identification ident = new Identification()
                        {
                            Name = signalName,
                            Value = identifications[i].value
                        };
                        ident = DB.GetIdentification(ident);

                        EcuIdentification ecu_ident = new EcuIdentification()
                        {
                            IdEcuNavigation = ecu,
                            IdIdentificationsNavigation = ident
                        };
                        ecu_ident = DB.GetEcuIdentification(ecu_ident);
                        sessionEcuidentification.Add(new SessionEcuidentification() { IdEcuidentificationsNavigation = ecu_ident });
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

        private void SetAoglonassReport(Session session, DiagAdditionalController diag)
        {
            var glonassLog = diag.GlonassLog;
            if (glonassLog != null)
            {
                for (int i = 0; i < glonassLog.Action.Length; i++)
                {
                    var action = glonassLog.Action[i];
                    for (int j = 0; j < action.ResponseTable.Length; j++)
                    {
                        AoglonassReport report = new()
                        {
                            IdSessionNavigation = session,
                            DateStart = Data.GetProcedureDate(action.DateTime),
                            Action = action.Type,
                            ParamText = diag.GetGlonassActionResponseParam(action.ResponseTable[j]),
                            Status = diag.GetGlonassActionResponseStatus(action.ResponseTable[j])
                        };
                        DB.SaveAoglonassReport(report);
                    }
                }
            }
        }

        private void SetProcedureReport(Session session, Models.sessionMachineTestResults[] procedures)
        {
            if (procedures != null)
            {
                for (int j = 0; j < procedures.Length; j++)
                {
                    if (procedures[j].Tests != null)
                        for (int i = 0; i < procedures[j].Tests.Length; i++)
                        {
                            string codifier = Data.GetProcedureECUCodifier(procedures[j].id);

                            ControlSystem system = new()
                            {
                                Name = Data.GetSystemName(codifier),
                                Domain = Data.GetDomainName(codifier)
                            };
                            system = DB.GetControlSystem(system);

                            Ecu ecu = new()
                            {
                                Codifier = codifier,
                                IdControlSystemNavigation = system
                            };
                            ecu = DB.GetECU(ecu);

                            string type = Data.GetProcedureECUType(procedures[j].id);
                            var test = procedures[j].Tests[i];
                            ProcedureReport report = new()
                            {
                                IdSessionNavigation = session,
                                IdEcuNavigation = ecu,
                                Type = type,
                                Name = test.name,//////
                                Result = test.result == "true" ? true : false,
                                DateStart = Data.GetProcedureDateOrNull(test.timestampStart),
                                DateEnd = Data.GetProcedureDate(test.timestamp),
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
