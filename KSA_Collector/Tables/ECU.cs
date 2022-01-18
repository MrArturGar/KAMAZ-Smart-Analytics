using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KSA_Collector.Settings;

namespace KSA_Collector.Tables
{
    internal class ECU
    {
        int id;
        string Codifier;
        public Dictionary<string, string> identifications;

        string TableName;

        public ECU(XElement _element)
        {
            string codifier = _element.Element("id").Value;
            Codifier = RemoveDCPrefix(codifier);
            TableName = GetTableName();
        }
        /// <summary>
        /// Определяем тип ЭБУ
        /// </summary>
        /// <returns>Имя таблицы</returns>
        private string GetTableName()
        {
            string[] parts = Codifier.Split('_');
            return parts[2];
        }

        /// <summary>
        /// Убираем DC из кодификатора ЭБУ
        /// </summary>
        /// <param name="_codifier"></param>
        /// <returns></returns>
        private string RemoveDCPrefix(string _codifier)
        {
            if (Regex.IsMatch(_codifier, @"^DC_.*$"))
                return _codifier.Remove(0, 3);
            else
                return _codifier;
        }

        private string[] GetSignalNames()
        {
            IdentificationSettings identificationSettings = IdentificationSettings.GetSettings();
            return identificationSettings.GetSignalNames(Codifier);
        }
    }
}
