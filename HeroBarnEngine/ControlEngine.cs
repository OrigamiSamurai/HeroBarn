using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroBarnEngine
{
    public class ControlEngine
    {
        static public List<Field> FieldList;

        static public bool checkForExistingField(string fieldName)
        {
            if (FieldList.Count > 0)
            {
                var matchingNames = from field in FieldList
                                    where field.name == fieldName
                                    select field.name;
                if (matchingNames.Count() > 0) { return true; }
                else { return false; };                                   
            }
            else return false;
        }

        static public Field selectField(string fieldName)
        {
            if (checkForExistingField(fieldName) == true)
            {
                var matchingNames = from field in FieldList
                                    where field.name == fieldName
                                    select field;
                return matchingNames.First();
            }
            else throw new System.Exception("There was an error when attempting to find the field in the FieldList.");    
        }

        public delegate void FieldValueChangedEventHandler(object sender, FieldValueChangedEventArgs e);

        public event FieldValueChangedEventHandler FieldValueChanged;

        protected virtual void OnFieldValueChanged(object sender, FieldValueChangedEventArgs e)
        {
            if (FieldValueChanged != null)
            {
                updateCascadeField(e.senderName);
                FieldValueChanged(this, e);
            }
        }

        public void updateCascadeField(string parentFieldName)
        {
            {
                List<string> childrenList = selectField(parentFieldName).childNames;
                if (childrenList.Count() > 0)
                {
                    foreach (string childName in childrenList)
                    {
                        selectField(childName).fieldValue = parseFieldFormula(childName);
                    }
                }
            }
        }

        public double parseFieldFormula(string fieldName)
        {
            if (selectField(fieldName).fieldValueType == typeof(int) || selectField(fieldName).fieldValueType == typeof(double))
            {
                MathParser.Calculator calc = new MathParser.Calculator();
                string formula = selectField(fieldName).formula;
                var keys = new Dictionary<string, double> { };
                //TODO: get variables needed? - crap, we need to do this dynamically or pre-crawl... sooooo... build in functionality to get data and add it to variables
                calc.LoadVariables(keys);
                calc.Evaluate(formula);
                return 0;
            }
            else throw new Exception("The field value selected is not an integer or double-precision floating point number.");
        }
    
    }

    

}
