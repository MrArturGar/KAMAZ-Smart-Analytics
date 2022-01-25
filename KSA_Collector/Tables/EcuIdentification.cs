using System;
using System.Collections.Generic;

namespace KSA_Collector.Tables
{
    public partial class EcuIdentification
    {
        public EcuIdentification()
        {
            VehiclesEcus = new HashSet<VehiclesEcu>();
        }

        public int Id { get; set; }
        public int IdEcu { get; set; }
        public int IdIdentifications { get; set; }

        public virtual Ecu IdEcuNavigation { get; set; } = null!;
        public virtual Identification IdIdentificationsNavigation { get; set; } = null!;
        public virtual ICollection<VehiclesEcu> VehiclesEcus { get; set; }
    }
}
