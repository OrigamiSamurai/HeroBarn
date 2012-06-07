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
        static public Dictionary<string, double> consoleVariables = new Dictionary<string,double>();
      

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
               // try
             //   {
                    Console.WriteLine("Your result: " + ControlEngine.parseFormula(evalstring));
              /*  }
                catch
                {
                    Console.WriteLine("That's a bad expression. Note: I don't support variable names yet.");
                }*/
            }
            else if (consoleInput.StartsWith("Field: "))
            {
               // try
                //{
                    string fieldString = consoleInput.Substring(7);
                    int firstSpaceIndex = fieldString.IndexOf(" ");
                    string fieldName = fieldString.Substring(0, firstSpaceIndex);
                    int secondSpaceIndex = fieldString.IndexOf(" ",firstSpaceIndex+1);
                    string fieldValue = fieldString.Substring(firstSpaceIndex+1,secondSpaceIndex-firstSpaceIndex-1);
                    string formulaValue = fieldString.Substring(secondSpaceIndex+1);
                    Console.WriteLine("Name: \"{0}\"   Value: \"{1}\"   Formula: \"{2}\"", fieldName, fieldValue,formulaValue);
                    double doubleValue = Convert.ToDouble(fieldValue);
                    ControlEngine.createDoubleField(fieldName, doubleValue,formulaValue);
              /*  }
                catch
                {
                    Console.WriteLine("Oops! That was incorrectly formatted. Try \"Eval: variableName variableValue\" next time.");
                }*/
            }
            else if (consoleInput.StartsWith("Edit: "))
            {
                string fieldString = consoleInput.Substring(6);
                int firstSpaceIndex = fieldString.IndexOf(" ");
                string fieldName = fieldString.Substring(0, firstSpaceIndex);
                string fieldValue = fieldString.Substring(firstSpaceIndex + 1);
                Console.WriteLine("Name: \"{0}\"   Value: \"{1}\"", fieldName, fieldValue);
                ControlEngine.FieldList.First(x => x.name == fieldName).fieldValue = Convert.ToDouble(fieldValue);
                Console.WriteLine("Value changed.");
            }
            else if (consoleInput.StartsWith("Child: "))
            {
                string fieldString = consoleInput.Substring(7);
                int firstSpaceIndex = fieldString.IndexOf(" ");
                string fieldName = fieldString.Substring(0, firstSpaceIndex);
                string childName = fieldString.Substring(firstSpaceIndex + 1);
                Console.WriteLine("Name: \"{0}\"   Child:\"{1}\"", fieldName, childName);
                ControlEngine.FieldList.First(x => x.name == fieldName).childNames = new List<string> { childName };
                Console.WriteLine("Child changed.");
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
