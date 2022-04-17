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

        public string LicLogFile;

        public string ServiceCentersPath;

        /// <summary>
        /// If it is zero, then it checks everything.
        /// </summary>
        public int CheckLastDaysSession;

        public int PeriodicCheckInMinutes;

        public DayOfWeek DayOfWeekToCheck;

        public int HourOfDayToCheck;

        private  ServiceSettings() : base("ServiceSettings") { }

        protected override void AddDefault()
        {
            SessionPath = @"C:\Users\altur\Desktop\КАИ\Диплом\Sessions\";
            LicLogFile = @"C:\Users\altur\Desktop\КАИ\Диплом\Logs\waf.xml";
            ServiceCentersPath = @"C:\Users\altur\Desktop\КАИ\Диплом\Sessions\serviceCenters.csv";
            CheckLastDaysSession = 0;
            PeriodicCheckInMinutes = 5;
            DayOfWeekToCheck = DayOfWeek.Sunday;
            HourOfDayToCheck = 19;
        }


        public static ServiceSettings GetSettings()
        {
            ServiceSettings ServiceSettings = null;
            try
            {
                ServiceSettings = new ServiceSettings();
                ServiceSettings.Load();
                return (ServiceSettings)ServiceSettings.Base;
            }
            finally
            {
                ServiceSettings = null;
            }
        }
    }
}
