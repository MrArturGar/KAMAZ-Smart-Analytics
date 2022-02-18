using System;
using System.Collections.Generic;

namespace TableModelLibrary.Models
{
    public partial class ControlSystem
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Domain { get; set; } = null!;
    }
}
