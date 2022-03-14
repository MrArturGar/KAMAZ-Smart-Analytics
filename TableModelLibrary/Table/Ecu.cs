using System;
using System.Collections.Generic;

namespace TableModelLibrary.Table
{
    public partial class Ecu
    {

        public int Id { get; set; }
        public string Codifier { get; set; } = null!;
        public int IdControlSystem { get; set; }

    }
}
