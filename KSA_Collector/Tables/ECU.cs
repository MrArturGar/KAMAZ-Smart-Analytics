using System;
using System.Collections.Generic;

namespace KSA_Collector.Tables
{
    public partial class Ecu
    {
        public int Id { get; set; }
        public string Codifier { get; set; } = null!;
        public int SystemId { get; set; }
    }
}
