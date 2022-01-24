using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSA_Collector.TablesOld
{
    internal class Session
    {
        public int id { get; set; }
        public Vehicle id_Vehicle { get; set; }
        public string SessionsName { get; set; }
        public DateTime Date { get; set; }
        public ServiceCenter id_serviceCenters { get; set; }
        public string VersionDB { get; set; }
        public string VCISN { get; set; }
        public float Mileage { get; set; }
        public bool Has_Identifications { get; set; }
        public bool Has_DTC { get; set; }
        public bool Has_Tests { get; set; }
        public bool Has_Flash { get; set; }
    }
}
