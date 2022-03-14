using System;
using System.Collections.Generic;

namespace TableModelLibrary.Web
{
    public partial class IdentificationWeb
    {
        public int IdSession { get; set; }
        public string Codifier { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Value { get; set; }
    }
}
