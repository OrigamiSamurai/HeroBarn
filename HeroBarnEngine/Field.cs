using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroBarnEngine
{

    public class Field
    {
        private string m_name;
        private Type m_fieldValueType;
        private object m_fieldValue;
        private string m_formula { set; get; }
        private List<string> m_childNames { set; get; }

        protected Field(string Name, Type fieldValueType, object fieldValue, string Formula = "", List<string> ChildNames = null)
        {
            m_name = Name;
            m_fieldValueType = null;
            m_fieldValue = fieldValue;
            m_formula = Formula;
            m_childNames = ChildNames;
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
            set { m_fieldValue = value; FieldValueChangedEventArgs e = new FieldValueChangedEventArgs(this.name, value, Type.GetType("System." + m_fieldValueType.ToString())); }
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
    }

    public class StringField : Field
    {
        private string m_fieldValue { get; set; }

        public StringField(string name, object fieldValue, Type myType, string formula, List<string> childNames)
            : base(name, myType, fieldValue, formula, childNames)
        {
            fieldValueType = myType;
            m_fieldValue = fieldValue.ToString();
        }

        public new object fieldValue
        {
            get { return m_fieldValue; }
            set { m_fieldValue = value.ToString(); FieldValueChangedEventArgs e = new FieldValueChangedEventArgs(this.name, value, Type.GetType("System.String")); }
        }
    }

    public class DoubleField : Field
    {
        private double m_fieldValue { get; set; }

        public DoubleField(string name, object fieldValue, Type myType, string formula, List<string> childNames)
            : base(name, myType, fieldValue, formula, childNames)
        {
            m_fieldValue = (double)fieldValue;
        }

        public new object fieldValue
        {
            get { return m_fieldValue; }
            set { m_fieldValue = Convert.ToDouble(value); FieldValueChangedEventArgs e = new FieldValueChangedEventArgs(this.name, value, Type.GetType("System.Double")); }
        }
    }
    
    public class IntField : Field
    {
        private int m_fieldValue { get; set; }

        public IntField(string name, object value, Type myType, string formula, List<string> childNames)
            : base(name, myType, value, formula, childNames)
        {
            m_fieldValue = (int)value;
        }

        public new object fieldValue
        {
            get { return m_fieldValue; }
            set { m_fieldValue = Convert.ToInt32(value); FieldValueChangedEventArgs e = new FieldValueChangedEventArgs(this.name, value, Type.GetType("System.Int32")); }
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
