using System;
using System.Collections.Generic;

namespace KSA_API.Views
{
    public partial class Identification
    {
        public Identification()
        {
            EcuIdentifications = new HashSet<EcuIdentification>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Value { get; set; }

        public virtual ICollection<EcuIdentification> EcuIdentifications { get; set; }
    }
}
