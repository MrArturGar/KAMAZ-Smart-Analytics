using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace KSA_Collector.Tables
{
    internal interface ITable
    {
        int id { get; }
        void GetData(XElement _element);

        void Read_db();

        bool Insert_db();

        bool Update_db();

        bool IsValid();

        string GetHash();
    }
}
