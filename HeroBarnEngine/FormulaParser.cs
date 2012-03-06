using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroBarn
{
    class FormulaParser
    {
        
        //QUERY: should the parser be able to get the values and do the actual calculation? OR is it simply building a data structure that will be fed to another object that will 

        //This object will parse the string input of rule to create an XML element that will be processed by another object that will fill in the data structure with the appropriate values, while another object will perform the actual operations
            //MATH EXPRESSION PARSER PSEUDOCODE
            //Needs to respect PEMDAS
            //Needs to be able to find the innermost parentheses and evaluate it first, then keep working the way out
            //build an algorithm to parse an expression
            //EXAMPLE:   Rounddown((a+b)/c-12%3)
            //STEP 1: Figure out where to start
                //Find any parentheses
                //If parentheses exist, check to make sure there are an even number
                //Find the last left parenthesis
                //Find the first right parenthesis after the last left parenthesis
                //grab whatever is inside, store it somewhere to be dealt with first, then begin "normal" processing
            //STEP 2: Normal processing of expression without parentheses
                //values need to know about the operators near them, thusly, we need to structure the data in such a way that it can be indexed, or can "know" what things are adjacent
                //first thing in an expression should be a variable name or numerical value
                    //if non-numerical, find the boundaries of the word and save the variable name as a node in the data structure
                        //find boundaries of variable name
                        //save as node
                    //if numerical, find boundaries of number, save number as string (calculator/processor will infer the correct type of value later)
                        //find boundaries of number
                        //save as node
            //STEP 3: save parenthetical value as numerical or generic named variable, lather rinse repeat


        //bnf - syntax for describing language syntax
            
        FormulaParser(string formulaToParse)
        {
        }
    }
}
