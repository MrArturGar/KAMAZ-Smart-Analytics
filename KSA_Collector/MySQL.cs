using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSA_Collector
{
    internal class MySQL : IDisposable
    {
        #region ServiceCenters
        internal int GetServiceCenterByUsername(string _username)
        {
            return 0;
        }
        #endregion

        #region ECU
        internal string GetColumnsFromTable(string _table)
        {
            string sql = $"SHOW COLUMNS FROM `{_table}`";

            return null;
        }

        internal bool ImportCSV(string _path, string _table)
        {
            string sql = @$"LOAD DATA INFILE '{_path}' 
            INTO TABLE {_table}.importexport 
            FIELDS TERMINATED BY ';';";


            return false;
        }

        #endregion
        public void Dispose()
        {

        }
    }
}
