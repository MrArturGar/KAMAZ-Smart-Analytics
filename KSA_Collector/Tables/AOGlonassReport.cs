using System;
using System.Collections.Generic;

namespace KSA_Collector.Tables
{
    public partial class AoglonassReport
    {
        public int Id { get; set; }
        public int IdSession { get; set; }
        public DateTime DateStart { get; set; }
        public string Type { get; set; } = null!;
        public string Request { get; set; } = null!;
        public string Response { get; set; } = null!;

        public virtual Session IdSessionNavigation { get; set; } = null!;
    }
}
