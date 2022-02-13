using System;
using System.Collections.Generic;

namespace KSA_API.Views
{
    public partial class SessionEcuidentification
    {
        public int Id { get; set; }
        public int IdEcuidentifications { get; set; }
        public int IdSession { get; set; }

        public virtual EcuIdentification IdEcuidentificationsNavigation { get; set; } = null!;
        public virtual Session IdSessionNavigation { get; set; } = null!;
    }
}
