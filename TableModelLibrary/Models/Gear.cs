using System;
using System.Collections.Generic;

namespace TableModelLibrary.Models
{
    public partial class Gear
    {
        public Gear()
        {
            Ecus = new HashSet<Ecu>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Domain { get; set; } = null!;

        public virtual ICollection<Ecu> Ecus { get; set; }
    }
}
