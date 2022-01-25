using System;
using System.Collections.Generic;

namespace KSA_Collector.Tables
{
    public partial class Ecu
    {
        public Ecu()
        {
            Composites = new HashSet<Composite>();
            EcuIdentifications = new HashSet<EcuIdentification>();
        }

        public int Id { get; set; }
        public string Codifier { get; set; } = null!;
        public int SystemId { get; set; }

        public virtual System System { get; set; } = null!;
        public virtual ICollection<Composite> Composites { get; set; }
        public virtual ICollection<EcuIdentification> EcuIdentifications { get; set; }
    }
}
