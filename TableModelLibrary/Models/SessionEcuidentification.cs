using System;
using System.Collections.Generic;

namespace TableModelLibrary.Models
{
    public partial class SessionEcuidentification
    {
        public int Id { get; set; }
        public int IdEcuidentifications { get; set; }
        public int IdSession { get; set; }
    }
}
