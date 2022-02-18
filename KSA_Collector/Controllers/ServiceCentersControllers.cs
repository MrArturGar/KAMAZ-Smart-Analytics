using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

using KSA_Collector.Settings;
using System.Text.RegularExpressions;
using TableModelLibrary.Models;

namespace KSA_Collector.Controllers
{
    internal class ServiceCentersControllers
    {
        string tempHashPath = Environment.CurrentDirectory + "\\Temp\\ServiceCenters.hash";
        string CSV_Path;

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
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Temp");
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
            using (ApiController dB = new ApiController())
            {

                using (StreamReader reader = new StreamReader(CSV_Path))
                {
                    reader.ReadLine();//Titles
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(';');

                        if (parts.Length == 10)
                        {
                            Regex rg = new Regex(@"^\d{1,}[.]\d{1,}$");
                            if (rg.IsMatch(parts[4].Trim()) && rg.IsMatch(parts[5].Trim()))
                            {
                                List<string> buffer = parts.ToList();
                                buffer[4] = parts[4].Trim() + ", " + parts[5].Trim();
                                buffer.RemoveAt(5);
                                parts = buffer.ToArray();
                            }
                        }


                        if (parts.Length != 9)
                            throw new Exception("CSV file is not validate.");

                        var service = new ServiceCenter()
                        {
                            Name = parts[0],
                            Address = parts[1],
                            City = parts[2],
                            Country = parts[3],
                            Postcode = parts[4],
                            Region = parts[5],
                            Username = parts[6],
                            Status = parts[7],
                            DilerTr = parts[8],
                        };
                        dB.SaveServiceCenter(service);
                    }
                }
            }
        }

        private void Update_db()
        {
            Insert_db();
        }
    
        public void DeleteHash()
        {
            File.Delete(tempHashPath);
        }
    }
}
