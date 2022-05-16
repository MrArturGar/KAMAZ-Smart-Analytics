using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace KSA_Collector.Handlers
{
    internal class DataHandler
    {

        /// <summary>
        /// Определяем систему ЭБУ
        /// </summary>
        /// <returns>Имя таблицы</returns>
        public string GetSystemName(string _codifier)
        {
            string[] parts = _codifier.Split('_');
            return parts[2];
        }

        /// <summary>
        /// Определяем расположение ЭБУ
        /// </summary>
        /// <returns>Имя таблицы</returns>
        public string GetDomainName(string _codifier)
        {
            string[] parts = _codifier.Split('_');
            return parts[1];
        }

        /// <summary>
        /// Убираем DC из кодификатора ЭБУ
        /// </summary>
        /// <param name="_codifier"></param>
        /// <returns></returns>
        public string RemoveDCPrefix(string _codifier)
        {
            if (Regex.IsMatch(_codifier, @"^DC_.*$"))
                return _codifier.Remove(0, 3);
            else
                return _codifier;
        }

        public DateTime GetSessionDate(string _sessionName)
        {
            //2021-12-30_13-58-00
            string dateString = _sessionName.Split('@').Last();

            return DateTime.ParseExact(dateString, "yyyy-MM-dd_HH-mm-ss", null);
        }
        public DateTime? GetProcedureDateOrNull(string _sessionName)
        {
            if (string.IsNullOrEmpty(_sessionName))
                return null;
            //12.10.2020 14:03:54
            string dateString = _sessionName.Split('@').Last();

            return DateTime.ParseExact(dateString, "dd.MM.yyyy HH:mm:ss", null);
        }
        public DateTime GetProcedureDate(string _sessionName)
        {
            string dateString = _sessionName.Split('@').Last();

            return DateTime.ParseExact(dateString, "dd.MM.yyyy HH:mm:ss", null);
        }

        public string GetProcedureECUCodifier(string _codifier)
        {
            string[] parts = _codifier.Split('_');
            return _codifier.Replace("_" + parts[parts.Length - 1], "");
        }

        public string GetProcedureECUType(string _codifier)
        {
            string[] parts = _codifier.Split('_');
            return parts[parts.Length - 1];
        }

        public string GetSessionVin(string sessionId)
        {
            return sessionId.Split('@').First();
        }

        public string GetVehicleType(string type)
        {
            return Regex.Split(type, "(?<!^)(?=[A-Z])")[0];
        }
    }
}
