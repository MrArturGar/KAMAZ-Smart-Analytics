using System;
using System.Collections.Generic;

namespace TableModelLibrary.Web
{
    public partial class DtcWeb
    {
        public int IdSession { get; set; }
        public string Codifier { get; set; } = null!;
        public string VehicleType { get; set; } = null!;
        public int Code { get; set; }
        public string TroubleCode { get; set; } = null!;
        public bool? Status { get; set; }
    }
}
