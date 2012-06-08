using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroBarnEngine;

namespace HeroBarnConsole
{
    class HeroBarnConsole
    {
        static private string consoleInput;

        public static void SubscribeToFieldChange(Field changedField)
        {
            changedField.FieldValueChanged += new Field.FieldValueChangedEventHandler(EngineFieldValueChanged);
        }

        public static void EngineFieldValueChanged(object changedField, FieldValueChangedEventArgs e)
        {
            Console.WriteLine("The value for {0} was recalculated.",((Field)changedField).name);
        }

        static void Main(string[] args)
        {
            Console.Write("Welcome! ");
            
            do
            {
                Console.WriteLine("Enter a command. Use \"q\" or \"Q\" to quit.");
                Console.Write("> ");
                consoleInput = Console.ReadLine();
                checkInputForCommand(consoleInput);
                Console.WriteLine("");
            }
            while (consoleInput != "q" && consoleInput != "Q");
        }

        static void checkInputForCommand(string consoleInput)
        {
            if (consoleInput.StartsWith("Eval: "))
            {
                string evalstring = consoleInput.Substring(6);
                Console.WriteLine("You are evaluating: " + evalstring);
                Console.WriteLine("Your result: " + ControlEngine.parseFormula(evalstring));
            }
            else if (consoleInput.StartsWith("Field: "))
            {
                    string fieldString = consoleInput.Substring(7);
                    int firstSpaceIndex = fieldString.IndexOf(" ");
                    string fieldName = fieldString.Substring(0, firstSpaceIndex);
                    int secondSpaceIndex = fieldString.IndexOf(" ",firstSpaceIndex+1);
                    string fieldValue = fieldString.Substring(firstSpaceIndex+1,secondSpaceIndex-firstSpaceIndex-1);
                    string formulaValue = fieldString.Substring(secondSpaceIndex+1);
                    Console.WriteLine("Name: \"{0}\"   Value: \"{1}\"   Formula: \"{2}\"", fieldName, fieldValue,formulaValue);
                    double doubleValue = Convert.ToDouble(fieldValue);
                    ControlEngine.createDoubleField(fieldName, doubleValue,formulaValue);
                    SubscribeToFieldChange(ControlEngine.selectField(fieldName));
            }
            else if (consoleInput.StartsWith("Value: "))
            {
                string fieldString = consoleInput.Substring(7);
                int firstSpaceIndex = fieldString.IndexOf(" ");
                string fieldName = fieldString.Substring(0, firstSpaceIndex);
                string fieldValue = fieldString.Substring(firstSpaceIndex + 1);
                Console.WriteLine("Name: \"{0}\"   Value: \"{1}\"", fieldName, fieldValue);
                ControlEngine.selectField(fieldName).fieldValue = Convert.ToDouble(fieldValue);
                Console.WriteLine("Value changed.");
            }
            else if (consoleInput.StartsWith("Formula: "))
            {
                string fieldString = consoleInput.Substring(9);
                int firstSpaceIndex = fieldString.IndexOf(" ");
                string fieldName = fieldString.Substring(0, firstSpaceIndex);
                string formula = fieldString.Substring(firstSpaceIndex + 1);
                Console.WriteLine("Name: \"{0}\"   Formula: \"{1}\"", fieldName, formula);
                ControlEngine.selectField(fieldName).formula = formula;
                Console.WriteLine("Value changed.");
            }
            else if (consoleInput.StartsWith("Children: "))
            {
                string fieldString = consoleInput.Substring(10);
                int firstSpaceIndex = fieldString.IndexOf(" ");
                string fieldName = fieldString.Substring(0, firstSpaceIndex);
                string childNamesUnseparated = fieldString.Substring(firstSpaceIndex + 1);
                List<string> inputChildNames = childNamesUnseparated.Split(' ').ToList();
                Console.WriteLine("Name: \"{0}\"   Children:\"{1}\"", fieldName, childNamesUnseparated);
                ControlEngine.FieldList.First(x => x.name == fieldName).childNames = inputChildNames;
                Console.WriteLine("Children changed.");
            }
            else if (consoleInput.StartsWith("Print: "))
            {
                string fieldName = consoleInput.Substring(7);
                Console.WriteLine("Printing Field with name \"{0}\"", fieldName);
                Field selectedField = ControlEngine.selectField(fieldName);
                string childrenAsString = "";
                if (!(selectedField.childNames == null) && selectedField.childNames.Count > 0)
                {
                    foreach (string childName in selectedField.childNames)
                    {
                        childrenAsString += childName + ";";
                    }
                }
                Console.WriteLine("Name: \"{0}\"   Type: \"{1}\"   Value: \"{2}\"   Formula: \"{3}\"   Children: \"{4}\"", selectedField.name, selectedField.fieldValueType.ToString(), selectedField.fieldValue.ToString(), selectedField.formula, childrenAsString);
            }
            else if (consoleInput.StartsWith("PrintAll") || consoleInput.Equals("p"))
            {
                if (ControlEngine.FieldList != null && ControlEngine.FieldList.Count > 0)
                foreach (Field field in ControlEngine.FieldList)
                {
                    string childNamesString = string.Join(",", field.childNames.ToArray());
                    Console.WriteLine("Name: {0}   Value: {1}   Formula: {2}   Children: {3}", field.name, field.fieldValue.ToString(), field.formula, childNamesString);
                }
            }
            else if (consoleInput.StartsWith("Save"))
            {
                ControlEngine.SaveFields();
                Console.WriteLine("Fields saved.");
            }
            else if (consoleInput.StartsWith("Load"))
            {
                ControlEngine.LoadFields();
                Console.WriteLine("Fields loaded.");
            }
            else
            {
                Console.WriteLine("You typed \'{0}\', you clever bastard. Unfortunately that isn't a command.", consoleInput);
            }
        }

    }
}
