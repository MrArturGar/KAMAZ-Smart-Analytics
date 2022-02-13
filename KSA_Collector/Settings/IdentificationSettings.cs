using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KSA_Collector.Settings
{
    public class IdentificationSettings : Settings
    {
        public ECUSignals[] SignalNames;

        public ECU_For_Tables[] TablesSignals;
        private IdentificationSettings() : base("IdentificationSettings") { }

        protected override void AddDefault()
        {
            SignalNames = new ECUSignals[1];
            string[] signals = {"VMSWNumber_0xF188",
                                "VMSWVersionNumber_0xF189",
                                "SSECUSWNumber_0xF194",
                                "SSECUSWVersionNumber_0xF195",
                                "SSECUHWNumber_0xF192",
                                "SSECUHWVersionNumber_0xF193",
                                "VMSWConfigNumber_0xF1A0",
                                "VMSWConfigVersionNumber_0xF1A0" };

            SignalNames[0] = new ECUSignals()
            {
                ECU = "default",
                Signals = signals
            };

            TablesSignals = new ECU_For_Tables[1];

            TablesSignals[0] = new ECU_For_Tables()
            {
                EcuCodifierPattern = "K_T_TP_015_",
                Signals = new ColumnSignal[]
                    {
                        new ColumnSignal()
                        {
                            TableName = "Vehicles",
                            ColumnName = "Vin",
                            SignalName = "Read_VehicleIdentificationNumber"
                        },
                        new ColumnSignal()
                        {
                            TableName = "Vehicles",
                            ColumnName = "Iccid",
                            SignalName = "Read_SimIdentifier"
                        },
                        new ColumnSignal()
                        {
                            TableName = "Vehicles",
                            ColumnName = "Iccidc",
                            SignalName = "Read_CommSimIdentifier"
                        },
                        new ColumnSignal()
                        {
                            TableName = "Vehicles",
                            ColumnName="Imei",
                            SignalName ="Read_IMEI"
                        }
                    }


            };
        }

        public static IdentificationSettings GetSettings()
        {
            IdentificationSettings identificationSettings = null;
            try
            {
                identificationSettings = new IdentificationSettings();
                identificationSettings.Load();
                return (IdentificationSettings)identificationSettings.Base;
            }
            finally
            {
                identificationSettings.Base = null;
            }
        }


        public string[] GetSignalNames(string _codifier)
        {
            string[] signals;
            var ecu_sginal = SignalNames.Where(c => c.ECU == _codifier).SingleOrDefault();

            if (ecu_sginal != null)
                signals = ecu_sginal.Signals;
            else
                signals = SignalNames.Where(c => c.ECU == "default").Single().Signals;

            return signals;
        }

        public bool HasEcuNameForTables(string _codifier)
        {
            for (int i = 0; i < TablesSignals.Length; i++)
            {
                string name = TablesSignals[i].EcuCodifierPattern;
                if (Regex.IsMatch(_codifier, name))
                    return true;
            }
            return false;
        }

        public ColumnSignal GetColumnSignal(string _codifier, string _signalName)
        {
            for (int i = 0; i < TablesSignals.Length; i++)
            {
                string name = TablesSignals[i].EcuCodifierPattern;
                if (Regex.IsMatch(_codifier, name))
                {
                    var signals = TablesSignals[i].Signals;
                    for (int j = 0; j < signals.Length; j++)
                    {
                        if (signals[j].SignalName == _signalName)
                            return signals[j];
                    }
                    break;
                }
            }
            return null;
        }
    }

    public class ECUSignals
    {
        public string ECU { get; set; }
        public string[] Signals { get; set; }
    }

    public class ECU_For_Tables
    {
        public string EcuCodifierPattern { get; set; }

        public ColumnSignal[] Signals { get; set; }
    }

    public class ColumnSignal
    {
        public string TableName { get; set; }
        public string SignalName { get; set; }
        public string ColumnName { get; set; }
    }
}
