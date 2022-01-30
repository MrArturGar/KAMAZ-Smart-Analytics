using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSA_Collector.Models
{
    internal class CommonInfo
    {
        public string Model { get; set; }
        public string VIN { get; set; }
        public string VIN_old { get; set; }
        public string ICCID { get; set; }
        public string User { get; set; }
        public string VCINumber { get; set; }
        public double Mileage { get; set; }
        public string VersionDatabase { get; set; }
    }
}
