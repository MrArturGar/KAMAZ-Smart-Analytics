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
        string path;
        public object Base;
        public Settings(string _name)
        {
            path = AppDomain.CurrentDomain.BaseDirectory + _name;
        }
        protected bool Load()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(GetType());
                Base = serializer.Deserialize(new StreamReader(path));

                if (Base == null)
                    throw new Exception("Base on abstract class is null");

                return true;
            }
            catch (Exception ex)
            {
                AddDefault();
                Save(path);
                Base = this;
                return false;
            }
        }

        protected void Save(string _path)
        {
            XmlSerializer serializer = new XmlSerializer(GetType());
            serializer.Serialize(new StreamWriter(_path), this);
        }
        protected abstract void AddDefault();

    }
}
