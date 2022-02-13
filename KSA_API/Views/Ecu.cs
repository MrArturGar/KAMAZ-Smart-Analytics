﻿using System;
using System.Collections.Generic;

namespace KSA_API.Views
{
    public partial class Ecu
    {
        public Ecu()
        {
            Composites = new HashSet<Composite>();
            EcuIdentifications = new HashSet<EcuIdentification>();
            ProcedureReports = new HashSet<ProcedureReport>();
        }

        public int Id { get; set; }
        public string Codifier { get; set; } = null!;
        public int SystemId { get; set; }

        public virtual System System { get; set; } = null!;
        public virtual ICollection<Composite> Composites { get; set; }
        public virtual ICollection<EcuIdentification> EcuIdentifications { get; set; }
        public virtual ICollection<ProcedureReport> ProcedureReports { get; set; }
    }
}
