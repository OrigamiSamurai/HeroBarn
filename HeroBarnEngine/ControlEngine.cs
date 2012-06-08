using System;
using System.IO;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroBarnEngine
{
    public class ControlEngine
    {
        public static List<Field> FieldList = new List<Field> { };

        public static bool checkForExistingField(string fieldName)
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



        public static void createDoubleField(string name, double fieldValue, string formulaValue)
        {
            var newField = new DoubleField(name, fieldValue, typeof(double), formulaValue, new List<string> { "" });
            FieldList.First(x => x.name == name).FieldValueChanged += new Field.FieldValueChangedEventHandler(FieldValueChanged);
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

        public static void FieldValueChanged(object sender, EventArgs e)
        {
            if (sender is Field)
            {
                Console.WriteLine("FieldValueChanged: {0} to {1}", ((Field)sender).name, ((FieldValueChangedEventArgs)e).newValue); //REMOVE
                updateCascadeField(((Field)sender).name);
            }
        }

        public static void updateCascadeField(string parentFieldName)
        {
            {
                Field parentField = selectField(parentFieldName);
                if ((parentField.childNames != null) && (parentField.childNames.Count() > 0) && (parentField.childNames[0] != ""))
                {
                    Console.WriteLine("updateCascadeField: called by {0}, and children property is not empty or null", parentFieldName); //REMOVE
                    List<string> childrenList = parentField.childNames;
                    foreach (string childName in childrenList)
                    {
                        Console.WriteLine("\"{0}\" is a child in the children list", childName); //REMOVE
                        Console.WriteLine("parsing {0} now... and then changing the value of {0}", childName); //REMOVE
                        selectField(childName).fieldValue = parseFieldFormula(childName);
                    }
                }
            }
        }

        public static double parseFieldFormula(string fieldName)
        {
            if (selectField(fieldName).fieldValueType == typeof(int) || selectField(fieldName).fieldValueType == typeof(double))
            {
                if (selectField(fieldName).formula != null && selectField(fieldName).formula != "")
                {
                    string formula = selectField(fieldName).formula;
                    return parseFormula(formula);
                }
                else throw new Exception("The targeted field does not have a formula defined.");
            }
            else throw new Exception("The field value selected is not an integer or double-precision floating point number.");
        }

        public static double parseFormula(string expression)
        {
            MathParser.Calculator calc = new MathParser.Calculator();
            Dictionary<string, double> VariableDictionary = new Dictionary<string, double> { };
            if (FieldList.Any())
            {
                foreach (Field field in FieldList)
                {
                    if (field is DoubleField || field is IntField)
                    {
                        VariableDictionary.Add(field.name, (double)field.fieldValue);
                    }
                }
            }
            calc.LoadVariables(VariableDictionary);
            return calc.Evaluate(expression);
        }

        public static void SaveFields()
        {
            IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            Stream stream = new FileStream("FieldList.bin",FileMode.Create,FileAccess.Write,FileShare.None);
            formatter.Serialize(stream, FieldList);
            stream.Close();
        }

        public static void LoadFields()
        {
            IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            Stream stream = new FileStream("FieldList.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            FieldList = (List<Field>)formatter.Deserialize(stream);
            stream.Close();
        }
    }

    

}
