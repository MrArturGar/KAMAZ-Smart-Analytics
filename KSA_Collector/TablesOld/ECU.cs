using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KSA_Collector.Settings;

namespace KSA_Collector.TablesOld
{
    internal class ECU
    {
        public int id { get; set; }
        public string Codifier { get; set; }
        public int System_id { get; set; }
    }
}
