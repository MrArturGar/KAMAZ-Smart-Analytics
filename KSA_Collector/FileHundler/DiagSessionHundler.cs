using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KSA_Collector.FileHundler
{
    internal class DiagSessionHundler
    {
        public DiagSessionHundler(DirectoryInfo _path)
        {
            Load(_path);
        }

        private void Load(DirectoryInfo _path)
        {

            FileInfo fileSession = _path.GetFiles()[0];

            session sessionFile = new session();
            XmlSerializer serializer = new XmlSerializer(typeof(session));
            try
            {
                sessionFile = (session)serializer.Deserialize(new StreamReader(fileSession.FullName));
            }
            catch (Exception ex) { }

        }
    }
}
