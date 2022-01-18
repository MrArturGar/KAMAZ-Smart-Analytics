namespace SessionFile
{
// Примечание. Для запуска созданного кода может потребоваться NET Framework версии 4.5 или более поздней версии и .NET Core или Standard версии 2.0 или более поздней.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class session
{

    private string idField;

    private sessionMachine machineField;

    private string usernameField;

    /// <remarks/>
    public string id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public sessionMachine machine
    {
        get
        {
            return this.machineField;
        }
        set
        {
            this.machineField = value;
        }
    }

    /// <remarks/>
    public string username
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
public partial class sessionMachine
{

    private string idField;

    private sessionMachineNetworks networksField;

    private sessionMachineTestResults[] testResultsField;

    /// <remarks/>
    public string id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public sessionMachineNetworks networks
    {
        get
        {
            return this.networksField;
        }
        set
        {
            this.networksField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("testResults")]
    public sessionMachineTestResults[] testResults
    {
        get
        {
            return this.testResultsField;
        }
        set
        {
            this.testResultsField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class sessionMachineNetworks
{

    private string idField;

    private ushort displayNameField;

    private sessionMachineNetworksEcus[] ecusField;

    /// <remarks/>
    public string id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public ushort displayName
    {
        get
        {
            return this.displayNameField;
        }
        set
        {
            this.displayNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ecus")]
    public sessionMachineNetworksEcus[] ecus
    {
        get
        {
            return this.ecusField;
        }
        set
        {
            this.ecusField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class sessionMachineNetworksEcus
{

    private object[] itemsField;

    private ItemsChoiceType[] itemsElementNameField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("displayName", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("dtcs", typeof(sessionMachineNetworksEcusDtcs))]
    [System.Xml.Serialization.XmlElementAttribute("id", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("identifications", typeof(sessionMachineNetworksEcusIdentifications))]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
    public object[] Items
    {
        get
        {
            return this.itemsField;
        }
        set
        {
            this.itemsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public ItemsChoiceType[] ItemsElementName
    {
        get
        {
            return this.itemsElementNameField;
        }
        set
        {
            this.itemsElementNameField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class sessionMachineNetworksEcusDtcs
{

    private uint troubleCodeField;

    private string displayTroubleCodeField;

    private string textField;

    private string statusField;

    private string statusTextField;

    private sessionMachineNetworksEcusDtcsEnvironmentData[] environmentDataField;

    /// <remarks/>
    public uint troubleCode
    {
        get
        {
            return this.troubleCodeField;
        }
        set
        {
            this.troubleCodeField = value;
        }
    }

    /// <remarks/>
    public string displayTroubleCode
    {
        get
        {
            return this.displayTroubleCodeField;
        }
        set
        {
            this.displayTroubleCodeField = value;
        }
    }

    /// <remarks/>
    public string text
    {
        get
        {
            return this.textField;
        }
        set
        {
            this.textField = value;
        }
    }

    /// <remarks/>
    public string status
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
    public string statusText
    {
        get
        {
            return this.statusTextField;
        }
        set
        {
            this.statusTextField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("environmentData")]
    public sessionMachineNetworksEcusDtcsEnvironmentData[] environmentData
    {
        get
        {
            return this.environmentDataField;
        }
        set
        {
            this.environmentDataField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class sessionMachineNetworksEcusDtcsEnvironmentData
{

    private byte recordNumberField;

    private sessionMachineNetworksEcusDtcsEnvironmentDataValues[] valuesField;

    /// <remarks/>
    public byte recordNumber
    {
        get
        {
            return this.recordNumberField;
        }
        set
        {
            this.recordNumberField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("values")]
    public sessionMachineNetworksEcusDtcsEnvironmentDataValues[] values
    {
        get
        {
            return this.valuesField;
        }
        set
        {
            this.valuesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class sessionMachineNetworksEcusDtcsEnvironmentDataValues
{

    private string idField;

    private sessionMachineNetworksEcusDtcsEnvironmentDataValuesValue valueField;

    private string displayTextField;

    /// <remarks/>
    public string id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public sessionMachineNetworksEcusDtcsEnvironmentDataValuesValue value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }

    /// <remarks/>
    public string displayText
    {
        get
        {
            return this.displayTextField;
        }
        set
        {
            this.displayTextField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class sessionMachineNetworksEcusDtcsEnvironmentDataValuesValue
{

    private decimal valueField;

    private string unitDisplayNameField;

    private string[] textField;

    /// <remarks/>
    public decimal value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }

    /// <remarks/>
    public string unitDisplayName
    {
        get
        {
            return this.unitDisplayNameField;
        }
        set
        {
            this.unitDisplayNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text
    {
        get
        {
            return this.textField;
        }
        set
        {
            this.textField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class sessionMachineNetworksEcusIdentifications
{

    private string idField;

    private string valueField;

    private string displayTextField;

    /// <remarks/>
    public string id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public string value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }

    /// <remarks/>
    public string displayText
    {
        get
        {
            return this.displayTextField;
        }
        set
        {
            this.displayTextField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
public enum ItemsChoiceType
{

    /// <remarks/>
    displayName,

    /// <remarks/>
    dtcs,

    /// <remarks/>
    id,

    /// <remarks/>
    identifications,
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class sessionMachineTestResults
{

    private string idField;

    private string resultField;

    private string assertionField;

    private string timestampField;

    private string displayTextField;

    private string dataField;

    /// <remarks/>
    public string id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public string result
    {
        get
        {
            return this.resultField;
        }
        set
        {
            this.resultField = value;
        }
    }

    /// <remarks/>
    public string assertion
    {
        get
        {
            return this.assertionField;
        }
        set
        {
            this.assertionField = value;
        }
    }

    /// <remarks/>
    public string timestamp
    {
        get
        {
            return this.timestampField;
        }
        set
        {
            this.timestampField = value;
        }
    }

    /// <remarks/>
    public string displayText
    {
        get
        {
            return this.displayTextField;
        }
        set
        {
            this.displayTextField = value;
        }
    }

    /// <remarks/>
    public string data
    {
        get
        {
            return this.dataField;
        }
        set
        {
            this.dataField = value;
        }
    }
}
    }
