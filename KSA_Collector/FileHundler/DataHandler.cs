using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KSA_Collector.FileHundler
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

    }
}
