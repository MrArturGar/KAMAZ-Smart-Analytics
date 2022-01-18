using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SessionFile
{
    public class SessionFile
    {
        public void Load(string _filename)
        {
            Stream stream = File.Open(_filename, FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(session));
            session session = (session)serializer.Deserialize(stream);
            stream.Flush();
        }
    }
}
