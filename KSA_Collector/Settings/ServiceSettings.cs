using KSA_Collector.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Xml.Serialization;

namespace KSA_Collector.Settings
{
    public class ServiceSettings :Settings
    {
        public string SessionPath;

        public string ServiceCentersPath;

        public int lastDays;

        private  ServiceSettings() : base("ServiceSettings.cfg") { }

        protected override void AddDefault()
        {
            SessionPath = @"C:\Users\altur\Desktop\КАИ\Диплом\Sessions";
            ServiceCentersPath = @"C:\Users\altur\Desktop\КАИ\Диплом\Sessions\serviceCenters.csv";
            lastDays = 0;
        }


        public static ServiceSettings GetSettings()
        {
            ServiceSettings ServiceSettings = new ServiceSettings();
            ServiceSettings.Load();
            return (ServiceSettings)ServiceSettings.Base;
        }
    }
}
