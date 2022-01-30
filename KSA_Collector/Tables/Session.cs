using System;
using System.Collections.Generic;

namespace KSA_Collector.Tables
{
    public partial class Session
    {
        public Session()
        {
            AoglonassReports = new HashSet<AoglonassReport>();
            ProcedureReports = new HashSet<ProcedureReport>();
            SessionEcus = new HashSet<SessionEcu>();
        }

        public int Id { get; set; }
        public int IdVehicle { get; set; }
        public string SessionsName { get; set; } = null!;
        public DateTime Date { get; set; }
        public int IdServiceCenters { get; set; }
        public string VersionDb { get; set; } = null!;
        public string Vcisn { get; set; } = null!;
        public double Mileage { get; set; }
        public bool? HasIdentifications { get; set; }
        public bool? HasDtc { get; set; }
        public bool? HasTests { get; set; }
        public bool? HasFlash { get; set; }

        public virtual ServiceCenter IdServiceCentersNavigation { get; set; } = null!;
        public virtual Vehicle IdVehicleNavigation { get; set; } = null!;
        public virtual ICollection<AoglonassReport> AoglonassReports { get; set; }
        public virtual ICollection<ProcedureReport> ProcedureReports { get; set; }
        public virtual ICollection<SessionEcu> SessionEcus { get; set; }
    }
}
