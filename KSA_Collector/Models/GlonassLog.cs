using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSA_Collector.Models
{

    // Примечание. Для запуска созданного кода может потребоваться NET Framework версии 4.5 или более поздней версии и .NET Core или Standard версии 2.0 или более поздней.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class GlonassLog
    {

        private GlonassLogAction[] actionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Action")]
        public GlonassLogAction[] Action
        {
            get
            {
                return this.actionField;
            }
            set
            {
                this.actionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GlonassLogAction
    {

        private GlonassLogActionRequest requestField;

        private GlonassLogActionResponse[] responseTableField;

        private string dateTimeField;

        private string typeField;

        private string usernameField;

        /// <remarks/>
        public GlonassLogActionRequest Request
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
        [System.Xml.Serialization.XmlArrayItemAttribute("Response", IsNullable = false)]
        public GlonassLogActionResponse[] ResponseTable
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
    public partial class GlonassLogActionRequest
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Iccid1;

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Iccid1Specified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Vin1;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BrandAndModel;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Color;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Iccid;

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IccidSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Vbn;

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool VbnSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Vin;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Iccid2;

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Iccid2Specified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Iccid3;

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Iccid3Specified;
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GlonassLogActionResponse
    {

        private string iccidField;

        private bool iccidFieldSpecified;

        private string requestProcessingStatusField;

        private string requestStatusDetailsField;

        private string requestIdField;

        private bool requestIdFieldSpecified;

        private string requestTypeField;

        private string statusField;

        private string vinField;

        private System.DateTime requestDateTimeField;

        private bool requestDateTimeFieldSpecified;

        private string msisdnField;

        private bool msisdnFieldSpecified;

        private string statusDescrField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Iccid
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IccidSpecified
        {
            get
            {
                return this.iccidFieldSpecified;
            }
            set
            {
                this.iccidFieldSpecified = value;
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RequestId
        {
            get
            {
                return this.requestIdField;
            }
            set
            {
                this.requestIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RequestIdSpecified
        {
            get
            {
                return this.requestIdFieldSpecified;
            }
            set
            {
                this.requestIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RequestType
        {
            get
            {
                return this.requestTypeField;
            }
            set
            {
                this.requestTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Vin
        {
            get
            {
                return this.vinField;
            }
            set
            {
                this.vinField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime RequestDateTime
        {
            get
            {
                return this.requestDateTimeField;
            }
            set
            {
                this.requestDateTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RequestDateTimeSpecified
        {
            get
            {
                return this.requestDateTimeFieldSpecified;
            }
            set
            {
                this.requestDateTimeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Msisdn
        {
            get
            {
                return this.msisdnField;
            }
            set
            {
                this.msisdnField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MsisdnSpecified
        {
            get
            {
                return this.msisdnFieldSpecified;
            }
            set
            {
                this.msisdnFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string StatusDescr
        {
            get
            {
                return this.statusDescrField;
            }
            set
            {
                this.statusDescrField = value;
            }
        }
    }


}
