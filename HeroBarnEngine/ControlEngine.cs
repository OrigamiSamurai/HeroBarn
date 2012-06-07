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
            var newField = new DoubleField(name, fieldValue, typeof(double), formulaValue, new List<string> { "b" });
            FieldList.First(x => x.name == name).FieldValueChanged += new Field.FieldValueChangedEventHandler(FieldValueChanged);
        }

        static public Field selectField(string fieldName)
        {
            //if (checkForExistingField(fieldName) == true)
            //{
                var matchingNames = from field in FieldList
                                    where field.name == fieldName
                                    select field;
                return matchingNames.First();
            //}
            //else throw new System.Exception("There was an error when attempting to find the field in the FieldList.");    
        }

        public static void FieldValueChanged(object sender, EventArgs e)
        {
            if (sender is Field)
            {
                updateCascadeField(((Field)sender).name);
            }
        }

        public static void updateCascadeField(string parentFieldName)
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

        public static double parseFieldFormula(string fieldName)
        {
            if (selectField(fieldName).fieldValueType == typeof(int) || selectField(fieldName).fieldValueType == typeof(double))
            {
                string formula = selectField(fieldName).formula;
                return parseFormula(formula);
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
            /*
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Field>));
            System.IO.StreamWriter file = new System.IO.StreamWriter("FieldList.xml");
            writer.Serialize(file, FieldList);
            file.Close(); */
        }

        public static void LoadFields()
        {
            IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            Stream stream = new FileStream("FieldList.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            FieldList = (List<Field>)formatter.Deserialize(stream);
            stream.Close();
            foreach (Field field in FieldList)
            {
                field.FieldValueChanged += new Field.FieldValueChangedEventHandler(FieldValueChanged);
            }
        }
    }

    

}
