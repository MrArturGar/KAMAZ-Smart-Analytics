using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSA_Collector.Tables
{
    internal class ProcedureReport
    {
        public int id { get; set; }
        public int id_session { get; set; }
        public string Procedure_name { get; set; }
        public bool Result { get; set; }
        public DateTime Date_start { get; set; }
        public DateTime Date_end { get; set; }
        public string Using_VIN { get; set; }
        public string DataFiles { get; set; }

    }
}
