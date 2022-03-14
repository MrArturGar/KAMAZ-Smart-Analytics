using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableModelLibrary.Table;

namespace TableModelLibrary.Web
{
    public class ProcedureReportWeb
    {
        public int Id { get; set; }
        public int IdEcu { get; set; }
        public string Codifier { get; set; }
        public int IdSession { get; set; }
        public string Type { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool? Result { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string? UsingVin { get; set; }
        public string? DataFiles { get; set; }
    }
}
