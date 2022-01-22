using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSA_Collector.Tables
{
    internal class Vehicle
    {
        public int id { get; set; }
        public string VIN { get; set; }
        public string Design_Number { get; set; }
        public string ICCID { get; set; }
        public string ICCIDC { get; set; }
        public string IMEI { get; set; }
        public string Type { get; set; }
    }
}
