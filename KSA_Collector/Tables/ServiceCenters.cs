using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

using KSA_Collector.Settings;

namespace KSA_Collector.Tables
{
    internal class ServiceCenters
    {
        string Table = "ServiceCenters";
        string username;
        string tempHashPath = "\\Temp\\ServiceCenters.hash";
        string CSV_Path;
        public ServiceCenters(string _username)
        {
            username = _username;
        }


        internal int GetId_ServiceCenters()
        {
            int id;
            using (var sql = new MySQL())
            {
                try
                {
                    id= sql.GetServiceCenterByUsername(username);
                }
                catch
                {
                    CheckValidTable();
                    id = sql.GetServiceCenterByUsername(username);
                }
            }

            return id;
        }

        public void CheckValidTable()
        {
            ServiceSettings settings = ServiceSettings.GetSettings();
            CSV_Path = settings.ServiceCentersPath;

            string newHash = GetHash();
            if (File.Exists(tempHashPath))
            {
                string oldHash = File.ReadAllText(tempHashPath);
                if (oldHash != newHash)
                {
                    Update_db();
                    File.WriteAllText(tempHashPath, newHash);
                }
            }
            else
            {
                Insert_db();
                File.WriteAllText(tempHashPath, newHash);
            }
        }

        private string GetHash()
        {

            byte[] hash;

            using (FileStream stream = File.OpenRead(CSV_Path))
            {
                SHA256Managed sha = new SHA256Managed();
                hash = sha.ComputeHash(stream);
            }
            return BitConverter.ToString(hash).Replace("-", String.Empty);
        }

        private void Insert_db()
        {
            using (var sql = new MySQL())
            {
                sql.ImportCSV(CSV_Path, Table);
            }
        }

        private void Update_db()
        {
            Insert_db();
        }
    }
}
