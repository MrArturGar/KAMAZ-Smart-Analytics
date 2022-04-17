using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KSA_Collector.Settings
{
    public abstract class Settings
    {
        string _path;
        public object Base;
        public Settings(string _name)
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory + "Settings/";

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            _path = dir + _name + ".cfg";
        }
        protected bool Load()
        {
            try
            {
                using (var sr = new StreamReader(_path))
                {
                    XmlSerializer serializer = new XmlSerializer(GetType());
                    Base = serializer.Deserialize(sr);
                }

                if (Base == null)
                    throw new Exception("Base on abstract class is null");

                return true;
            }
            catch (Exception ex)
            {
                AddDefault();
                Save();
                Base = this;
                return false;
            }
        }

        protected void Save()
        {
            using (var sw = new StreamWriter(_path)) {
                XmlSerializer serializer = new XmlSerializer(GetType());
                serializer.Serialize(sw, this);
            }
        }
        protected abstract void AddDefault();

    }
}
