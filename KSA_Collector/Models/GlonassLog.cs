using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSA_Collector.Models
{
    public class GlonassLog
    {
        public List<Action> Actions;
    }
    // Примечание. Для запуска созданного кода может потребоваться NET Framework версии 4.5 или более поздней версии и .NET Core или Standard версии 2.0 или более поздней.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class Action
    {

        private string requestField;

        private string responseTableField;

        private string dateTimeField;

        private string typeField;

        private string usernameField;

        /// <remarks/>
        public string Request
        {
            get
            {
                return this.requestField;
            }
            set
            {
                this.requestField = value;
            }
        }

        /// <remarks/>
        public string ResponseTable
        {
            get
            {
                return this.responseTableField;
            }
            set
            {
                this.responseTableField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DateTime
        {
            get
            {
                return this.dateTimeField;
            }
            set
            {
                this.dateTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Username
        {
            get
            {
                return this.usernameField;
            }
            set
            {
                this.usernameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class ActionRequest
    {

        private ulong iccid1Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ulong Iccid1
        {
            get
            {
                return this.iccid1Field;
            }
            set
            {
                this.iccid1Field = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class ActionResponseTable
    {

        private ActionResponseTableResponse[] responseField;

        /// <remarks/>
        public ActionResponseTableResponse[] Response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class ActionResponseTableResponse
    {

        private ulong iccidField;

        private string requestProcessingStatusField;

        private string requestStatusDetailsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ulong Iccid
        {
            get
            {
                return this.iccidField;
            }
            set
            {
                this.iccidField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RequestProcessingStatus
        {
            get
            {
                return this.requestProcessingStatusField;
            }
            set
            {
                this.requestProcessingStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RequestStatusDetails
        {
            get
            {
                return this.requestStatusDetailsField;
            }
            set
            {
                this.requestStatusDetailsField = value;
            }
        }
    }


}
