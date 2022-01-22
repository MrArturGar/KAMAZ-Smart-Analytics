using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSA_Collector.Tables
{
    internal class AOGlonassReport
    {
        public int id { get; set; }
        public Session id_session { get; set; }
        public DateTime Date_start { get; set; }
        public string Type { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }

    }
}
