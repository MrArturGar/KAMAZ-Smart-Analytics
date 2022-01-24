using System;
using System.Collections.Generic;

namespace KSA_Collector.Tables
{
    public partial class Composite
    {
        public int Id { get; set; }
        public string DesignNumber { get; set; } = null!;
        public int IdEcu { get; set; }
    }
}
