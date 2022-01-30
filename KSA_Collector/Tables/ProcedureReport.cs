﻿using System;
using System.Collections.Generic;

namespace KSA_Collector.Tables
{
    public partial class ProcedureReport
    {
        public int Id { get; set; }
        public int IdSession { get; set; }
        public string Codifier { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool? Result { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string? UsingVin { get; set; }
        public string DataFiles { get; set; } = null!;

        public virtual Session IdSessionNavigation { get; set; } = null!;
    }
}
