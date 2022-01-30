using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KSA_Collector.Settings
{
    public class IdentificationSettings : Settings
    {
        public SignalName[] SignalNames;
        private IdentificationSettings() : base("IdentificationSettings") { }

        protected override void AddDefault()
        {
            SignalNames = new SignalName[1];
            string[] signals = {"VMSWNumber_0xF188",
                                "VMSWVersionNumber_0xF189",
                                "SSECUSWNumber_0xF194",
                                "SSECUSWVersionNumber_0xF195",
                                "SSECUHWNumber_0xF192",
                                "SSECUHWVersionNumber_0xF193",
                                "VMSWConfigNumber_0xF1A0",
                                "VMSWConfigVersionNumber_0xF1A0" };

            SignalNames[0] = new SignalName()
            {
                ECU = "default",
                Signals = signals
            };
        }

        public static IdentificationSettings GetSettings()
        {
            IdentificationSettings identificationSettings = new IdentificationSettings();
            identificationSettings.Load();
            return (IdentificationSettings)identificationSettings.Base;
        }


        public string[] GetSignalNames(string _codifier)
        {
            string[] signals;
            try
            {
                signals = SignalNames.Where(c => c.ECU == _codifier).Single().Signals;
            }
            catch
            {
                signals = SignalNames.Where(c => c.ECU == "default").Single().Signals;
            }

            return signals;
        }
    }

    public class SignalName
    {
        public string ECU { get; set; }
        public string[] Signals { get; set; }
    }

}
