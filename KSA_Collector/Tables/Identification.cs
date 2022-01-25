using System;
using System.Collections.Generic;

namespace KSA_Collector.Tables
{
    public partial class Identification
    {
        public Identification()
        {
            EcuIdentifications = new HashSet<EcuIdentification>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;

        public virtual ICollection<EcuIdentification> EcuIdentifications { get; set; }
    }
}
