using System;
using System.Collections.Generic;

namespace KSA_API.Views
{
    public partial class AoglonassReport
    {
        public int Id { get; set; }
        public int IdSession { get; set; }
        public DateTime DateStart { get; set; }
        public string Action { get; set; } = null!;
        public string ParamText { get; set; } = null!;
        public string Status { get; set; } = null!;

        public virtual Session IdSessionNavigation { get; set; } = null!;
    }
}
