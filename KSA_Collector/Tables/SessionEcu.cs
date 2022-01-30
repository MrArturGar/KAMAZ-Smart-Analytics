using System;
using System.Collections.Generic;

namespace KSA_Collector.Tables
{
    public partial class SessionEcu
    {
        public int Id { get; set; }
        public int IdIdentifications { get; set; }
        public int IdSession { get; set; }

        public virtual EcuIdentification IdIdentificationsNavigation { get; set; } = null!;
        public virtual Session IdSessionNavigation { get; set; } = null!;
    }
}
