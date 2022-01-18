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
        Dictionary<string, string[]> SignalNames = new Dictionary<string, string[]>();
        private IdentificationSettings() : base("IdentificationSettings") { }

        public bool HasChanges()
        {
            return true;
        }


        protected override void AddDefault()
        {
            string[] signals = {"VMSWNumber_0xF188",
                                "VMSWVersionNumber_0xF189",
                                "SSECUSWNumber_0xF194",
                                "SSECUSWVersionNumber_0xF195",
                                "SSECUHWNumber_0xF192",
                                "SSECUHWVersionNumber_0xF193",
                                "VMSWConfigNumber_0xF1A0",
                                "VMSWConfigVersionNumber_0xF1A0" };

            SignalNames.Add("default", signals);
        }

        public static IdentificationSettings GetSettings()
        {
            IdentificationSettings identificationSettings = new IdentificationSettings();
            identificationSettings.Load();
            return (IdentificationSettings) identificationSettings.Base;
        }


        public string[] GetSignalNames(string _codifier)
        {
            if (SignalNames.ContainsKey(_codifier))
                return SignalNames[_codifier];
            else
                return SignalNames["default"];
        }
    }
}
