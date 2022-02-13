using System;
using System.Collections.Generic;

namespace KSA_API.Views
{
    public partial class System
    {
        public System()
        {
            Ecus = new HashSet<Ecu>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Domain { get; set; } = null!;

        public virtual ICollection<Ecu> Ecus { get; set; }
    }
}
