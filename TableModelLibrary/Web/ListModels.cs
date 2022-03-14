using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableModelLibrary.Table;

namespace TableModelLibrary.Web
{

    public class SessionListWeb
    {
        public Session[] Items { get; set; }

        public int Count { get; set; }
    }
    public class VehicleListWeb
    {
        public Vehicle[] Items { get; set; }

        public int Count { get; set; }
    }
}
