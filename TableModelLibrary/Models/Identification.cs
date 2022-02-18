using System;
using System.Collections.Generic;

namespace TableModelLibrary.Models
{
    public partial class Identification
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Value { get; set; }
    }
}
