using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                MathParser.Calculator calc = new MathParser.Calculator();
                try
                {
                    calc.LoadVariables(consoleVariables);
                    Console.WriteLine("Your result: " + calc.Evaluate(evalstring));
                }
                catch
                {
                    Console.WriteLine("That's a bad expression. Note: I don't support variable names yet.");
                }
            }
            else if (consoleInput.StartsWith("Var: "))
            {
                try
                {
                    string variableString = consoleInput.Substring(5);
                    int spaceIndex = variableString.IndexOf(" ");
                    string variableName = variableString.Substring(0, spaceIndex);
                    string variableValue = variableString.Substring(spaceIndex + 1);
                    Console.WriteLine("Name: \"{0}\"   Value: \"{1}\"", variableName, variableValue);
                    double doublevalue = Convert.ToDouble(variableValue);
                    consoleVariables.Add(variableName, doublevalue);
                    Console.WriteLine(consoleVariables.Last().ToString());
                }
                catch
                {
                    Console.WriteLine("Oops! That was incorrectly formatted. Try \"Eval: variableName variableValue\" next time.");
                }
            }
            else
            {
                Console.WriteLine("You typed \'{0}\', you clever bastard. Unfortunately that isn't a command.", consoleInput);
            }
        }

    }
}
