using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroBarnEngine
{
    //[System.Xml.Serialization.XmlInclude(typeof(StringField))]
    //[System.Xml.Serialization.XmlInclude(typeof(DoubleField))]
    //[System.Xml.Serialization.XmlInclude(typeof(IntField))]
    [Serializable]
    public class Field
    {
        private string m_name;
        private Type m_fieldValueType;
        private object m_fieldValue;
        private string m_formula;
        private List<string> m_childNames;

        protected Field()
        {
        }

        protected Field(string Name, Type fieldValueType, object fieldValue, string Formula = "", List<string> ChildNames = null)
        {
            m_name = Name;
            m_fieldValueType = fieldValueType;
            m_fieldValue = fieldValue;
            m_formula = Formula;
            m_childNames = ChildNames;
            ControlEngine.FieldList.Add(this);
            FieldValueChangedEventArgs e = new FieldValueChangedEventArgs(this.name, m_fieldValue, Type.GetType("System." + m_fieldValueType.ToString()));
        }

        public string name
        {
            get { return m_name; }
            set { if (ControlEngine.checkForExistingField(name) != true) { m_name = name; } }
        }
        public Type fieldValueType
        {
            get { return m_fieldValueType; }
            set { m_fieldValueType = value; }
        }
        public object fieldValue
        {
            get { return m_fieldValue; }
            set { m_fieldValue = value; OnFieldValueChanged(this, new FieldValueChangedEventArgs(this.name, value, Type.GetType("System." + m_fieldValueType.ToString()))); }
        }
        public string formula
        {
            get { return m_formula; }
            set { m_formula = value; }
        }
        public List<string> childNames
        {
            get { return m_childNames; }
            set { m_childNames = value; }
        }

        public delegate void FieldValueChangedEventHandler(object sender, FieldValueChangedEventArgs e);

        public event FieldValueChangedEventHandler FieldValueChanged;

        protected virtual void OnFieldValueChanged(object sender, FieldValueChangedEventArgs e)
        {
            if (FieldValueChanged != null)
            {
                FieldValueChanged(this, e);
            }
        }
    }

    [Serializable]
    public class StringField : Field
    {
        private string m_fieldValue { get; set; }

        private StringField()
            : base()
        {
        }

        public StringField(string name, object fieldValue, Type myType, string formula, List<string> childNames)
            : base(name, myType, fieldValue, formula, childNames)
        {
            fieldValueType = myType;
            m_fieldValue = fieldValue.ToString();
        }

        public new object fieldValue
        {
            get { return m_fieldValue; }
            set { m_fieldValue = value.ToString(); OnFieldValueChanged(this, new FieldValueChangedEventArgs(this.name, value, Type.GetType("System.String"))); }
        }
    }

    [Serializable]
    public class DoubleField : Field
    {
        private double m_fieldValue { get; set; }

        private DoubleField()
            : base()
        {
        }
        
        public DoubleField(string name, object fieldValue, Type myType, string formula, List<string> childNames)
            : base(name, myType, fieldValue, formula, childNames)
        {
            m_fieldValue = (double)fieldValue;
        }

        public new object fieldValue
        {
            get { return m_fieldValue; }
            set { m_fieldValue = Convert.ToDouble(value); OnFieldValueChanged(this, new FieldValueChangedEventArgs(this.name, value, Type.GetType("System.Double"))); }
        }
    }
    
    [Serializable]
    public class IntField : Field
    {
        private int m_fieldValue { get; set; }

        private IntField()
            : base()
        {
        }

        public IntField(string name, object value, Type myType, string formula, List<string> childNames)
            : base(name, myType, value, formula, childNames)
        {
            m_fieldValue = (int)value;
        }

        public new object fieldValue
        {
            get { return m_fieldValue; }
            set { m_fieldValue = Convert.ToInt32(value); OnFieldValueChanged(this, new FieldValueChangedEventArgs(this.name, value, Type.GetType("System.Int32"))); }
        }
    }

    public class FieldValueChangedEventArgs : EventArgs
    {
        private readonly string m_senderName;
        private readonly object m_newValue;
        private readonly Type m_fieldValueType;

        public FieldValueChangedEventArgs(string senderName, object newValue, Type valueType)
        {
            m_newValue = newValue;
            m_fieldValueType = valueType;
        }

        public string senderName { get { return m_senderName; } }
        public object newValue { get { return m_newValue; } }
        public object valueType { get { return m_fieldValueType; } }
    }

    
}
