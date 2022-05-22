using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using KSA_Collector;
using KSA_Collector.Handlers;
using KSA_Collector.Models;
using TableModelLibrary;

namespace KSA_Collector.Controllers
{
    internal class DiagSessionController : IDisposable
    {
        ApiController _apiClient;
        Settings.IdentificationSettings IdentificationSettings;
        DataHandler Data;
        Vehicle Vehicle = new();
        Session Session = new();
        List<SessionEcuidentification> sessionEcuidentification = new();
        List<SessionDtc> sessionDtcs = new();
        string AdditionalArchivePath;

        public DiagSessionController()
        {
            GetSettings();
        }
        public void Dispose()
        {
            if (_apiClient != null)
            {
                _apiClient.Dispose();
                _apiClient = null;
            }
        }

        private void GetSettings()
        {
            _apiClient = new ApiController();
            UpdateServiceCenters();
            IdentificationSettings = Settings.IdentificationSettings.GetSettings();
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

                if (!_apiClient.SessionExistenceByName(fileSession.Split("\\").Last().Replace(".session","")))
                {
                    Models.session session;
                    XmlSerializer serializer = new XmlSerializer(typeof(Models.session));

                    session = (Models.session)serializer.Deserialize(new StreamReader(fileSession));
                    LoadECUs(session);
                    LoadVehicle(session);
                    LoadSession(session);
                }
            }
        }

        private void UpdateServiceCenters()
        {
            ServiceCentersControllers controller = new ServiceCentersControllers(_apiClient);

            controller.CheckValidTable();
        }

        private void LoadECUs(Models.session session)
        {
            string design_number = session.machine.networks.id;
            var ecus = session.machine.networks.ecus;

            if (ecus != null)
            {

                for (int i = 0; i < ecus.Count(); i++)
                {
                    var ecu = ecus[i];
                    string codifier = ecus[i].id;

                    ControlSystem system = new()
                    {
                        Name = Data.GetSystemName(codifier),
                        Domain = Data.GetDomainName(codifier)
                    };

                    Ecu ecus_table = new Ecu()
                    {
                        Codifier = codifier,
                        IdControlSystem = _apiClient.GetControlSystemId(system)
                    };
                    ecus_table.Id = _apiClient.GetEcuId(ecus_table);

                    Composite composite = new Composite()
                    {
                        DesignNumber = session.machine.networks.displayName,
                        IdEcu = ecus_table.Id
                    };
                    _apiClient.SaveComposite(composite);

                    SetEcuIdentifications(ecus[i], ecus_table.Id);
                    SetSessionDtcs(ecus[i], ecus_table.Id, session.machine.id);

                }
            }
        }

        private void SetEcuIdentifications(sessionMachineNetworksEcus ecuFile, int ecuId)
        {
            var identifications = ecuFile.identifications;

            if (ecuFile.dtcs != null)
                Session.HasDtc = true;

            if (identifications != null)
            {
                Session.HasIdentifications = true;
                string[] signals = IdentificationSettings.GetSignalNames(ecuFile.id);
                bool hasTable = IdentificationSettings.HasEcuNameForTables(ecuFile.id);

                Translates translates = new Translates();////////////////////////////////////////////////////DELETE
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
                        ident.Id = _apiClient.GetIdentificationId(ident);
                        translates.LoadPhrase(ident.Name, identifications[i].displayText);////////////////////////////////////////////////////DELETE

                        EcuIdentification ecu_ident = new EcuIdentification()
                        {
                            IdEcu = ecuId,
                            IdIdentifications = ident.Id
                        };
                        ecu_ident.Id = _apiClient.GetEcuIdentificationId(ecu_ident);
                        sessionEcuidentification.Add(new SessionEcuidentification() { IdEcuidentifications = ecu_ident.Id });
                    }

                    if (hasTable)
                    {
                        LoadIdentificationFromTable(ecuFile.id, identifications[i]);
                    }
                }
            }
        }

        private void SetSessionDtcs(sessionMachineNetworksEcus ecuFile, int ecuId, string type)
        {
            var dtcs = ecuFile.dtcs;

            if (dtcs != null)
            {
                Translates translates = new Translates();
                Session.HasDtc = true;
                for (int i = 0; i < dtcs.Length; i++)
                {
                    var dtcFile = dtcs[i];
                    Dtc dtc = new Dtc
                    {
                        IdEcu = ecuId,
                        VehicleType = Data.GetVehicleType(type),///
                        Code = (int)dtcFile.troubleCode,
                        TroubleCode = dtcFile.displayTroubleCode
                    };
                    dtc.Id = _apiClient.GetDtcId(dtc);
                    translates.LoadPhrase($"{dtc.VehicleType}.{dtc.IdEcu}.{dtc.Code}", dtcFile.text);

                    SessionDtc sessionDtc = new SessionDtc
                    {
                        IdDtc = dtc.Id
                    };

                    if (dtcFile.statusText.Contains("Активная"))
                    {
                        sessionDtc.Status = true;
                    }
                    else if (dtcFile.statusText.Contains("Пассивная"))
                    {
                        sessionDtc.Status = false;
                    }
                    else
                    {
                        sessionDtc.Status = null;
                    }
                    sessionDtcs.Add(sessionDtc);


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


            Session.IdVehicle = Vehicle.Id;
            Session.SessionsName = sessionFile.id;
            Session.Date = Data.GetSessionDate(sessionFile.id);
            Session.IdServiceCenters = _apiClient.GetServiceCenterId(username);
            if (diag.CommonInfo != null)
            {
                Session.VersionDb = diag.CommonInfo.VersionDatabase;
                Session.Vcisn = diag.CommonInfo.VCINumber;
                Session.Mileage = diag.CommonInfo.Mileage;
            }
            Session.Id = _apiClient.GetSessionId(Session);


            SetProcedureReport(Session, sessionFile.machine.testResults);
            SetAoglonassReport(Session, diag);
            LoadSessionEcuIdentification();
            LoadSessionDtc();
            _apiClient.SaveSession(Session);
        }

        private void LoadSessionEcuIdentification()
        {
            for (int i = 0; i < sessionEcuidentification.Count; i++)
            {
                sessionEcuidentification[i].IdSession = Session.Id;
                _apiClient.SaveSessionEcuIdentification(sessionEcuidentification[i]);
            }
        }

        private void LoadSessionDtc()
        {
            for (int i = 0; i < sessionDtcs.Count; i++)
            {
                sessionDtcs[i].IdSession = Session.Id;
                _apiClient.SaveSessionDtc(sessionDtcs[i]);
            }
        }

        private void LoadVehicle(Models.session session)
        {
            Vehicle.DesignNumber = session.machine.networks.displayName;
            Vehicle.Type = session.machine.networks.id;

            if (Vehicle.Vin == null)
                Vehicle.Vin = Data.GetSessionVin(session.id);

            Vehicle.Id = _apiClient.GetVehicleId(Vehicle);
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
                            IdSession = session.Id,
                            DateStart = Data.GetProcedureDate(action.DateTime),
                            Action = action.Type,
                            ParamText = diag.GetGlonassActionResponseParam(action.ResponseTable[j]),
                            Status = diag.GetGlonassActionResponseStatus(action.ResponseTable[j])
                        };
                        _apiClient.SaveAoglonassReport(report);
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
                            system.Id = _apiClient.GetControlSystemId(system);

                            Ecu ecu = new()
                            {
                                Codifier = codifier,
                                IdControlSystem = system.Id
                            };
                            ecu.Id = _apiClient.GetEcuId(ecu);

                            string type = Data.GetProcedureECUType(procedures[j].id);
                            var test = procedures[j].Tests[i];
                            ProcedureReport report = new()
                            {
                                IdSession = session.Id,
                                IdEcu = ecu.Id,
                                Type = type,
                                Name = test.name,//////
                                Result = test.result == "true" ? true : false,
                                DateStart = Data.GetProcedureDateOrNull(test.timestampStart),
                                DateEnd = Data.GetProcedureDate(test.timestamp),
                                UsingVin = String.IsNullOrEmpty(test.VINForFlash) ? test.VIN : test.VINForFlash,
                                DataFiles = test.flashDataFile
                            };
                            _apiClient.SaveProcedureReport(report);

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
