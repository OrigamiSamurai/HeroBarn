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
            //EXAMPLE:   Rounddown((a+b)/c-12%3)  <parenth> <parenth> <variable>a</variable><operator>+</operator><variable>b</variable><parenth><operator>/</operator>   parsing left to right?
            //STEP 1: Figure out where to start
                //Find any parentheses
                //If parentheses exist, check to make sure there are an even number
                //Find the last left parenthesis
                //Find the first right parenthesis after the last left parenthesis
                //grab whatever is inside, store it somewhere to be dealt with first, set tag if special operator is done to them after processing internal stuff, then begin "normal" processing
                    //check for function keyword to left of parenthesis
                        //if present, add a flag to the data struct of this node to do the operation on this node before using it as a value
                        //if not, continue
                    //get value of stuff between parentheses, process as normal
            //STEP 2: Normal processing of expression without parentheses
                //values need to know about the operators near them, thusly, we need to structure the data in such a way that it can be indexed, or can "know" what things are adjacent
                //first thing in an expression should be a variable name or numerical value
                    //if non-numerical, find the boundaries of the word and save the variable name as a node in the data structure
                        //find boundaries of variable name
                        //save as node
                    //if numerical, find boundaries of number, save number as string (calculator/processor will infer the correct type of value later)
                        //find boundaries of number
                        //save as node
                    //replace where  
            //STEP 3: save parenthetical value as numerical or generic named variable, lather rinse repeat... by replacing the previously evaluated expressions with something else?
            /*
             * <function name="ROUNDUP">
             *   <parenthesis>
             *     <variable>a</variable>
             *     <operator>+</operator>
             *     <variable>b<variable>
             *   </parenthesis>
             *   <operation>/</operation>
             *   <number>2</number>
             *   <operator>-</operator>
             *   <number>3</number>
             *   <operator>*</operator>
             *   <number>5</number>
             * </function>
             * 
             * steps to actually computer this: need to flatten this calculation somehow... I.E. do the first deepest levels FIRST
             * need to find out how many levels of descendents there are - does function that does this need to know how many levels? NO
             * 
             * 1) find out if the given element has any children
             * 2) if it does, start the function on IT
             * 3) etc, until you reach the first element with no children
             * 4) gather its siblings
             * 5) process them
             *     now, on an even playing field (no functions or parentheses) you should have just the basic operators
             *     check to make sure you have an alternating pattern of variable, operator, variable....operator, variable
             *     if so, find the first operator, and check if there is a second operator
             *     if there is, check if it has a higher PEMDAS priority
             *     if not, operate on the first two variable using the first operator
             *       get value of variable OR use numeric value
             *     if so, then operate on the second and third variables, since there should only be +- or * / (guaranteed to be a third variable here)
             *       replace the variable or number nodes acted upon with a single numeric value
             *          OPERATING ON TWO values
             *              get value of first field, get value of second field, get operator, send to function that takes those inputs and passes them to the appropriate calculator function and returns a value
             *              take the single value and replace that portion of the expression tree with the single value
             *  iterate until only a single value remains
             *  then iterate this process again until only a single value remains
             * 
             * 

        //bnf - syntax for describing language syntax
        
        /*
         * http://www.iphonedevsdk.com/forum/iphone-sdk-development/85299-building-math-expression-tree.html
         * 
         Hi,

i have a problem.
I tried a lot of things, but i can´t figure it out.

Maybe someone can help.

I want to build an expression tree from a math expression in objective-c.

I have pseudo code, but i can´t do it in Objective-C.

Here is the pseudo code.



PopConnectPush
{
pop the top node off the operator stack and call it N; 
pop the top node off the tree stack and make it N's right child; 
pop the top node off the tree stack and make it N's left child; 
push N back into the tree stack;
}


Convert Expression to Tree
initialize operator and tree stacks;
while (there are tokens remaining in the expression) 
{
T = next token from expression; 
if (T == '(') 
{
create a node and store T in it; 
push the node onto the operator stack;
} 
else if (T is a variable or numeric literal) 
{
create a node and store T in it; 
push the node onto the tree stack;
} 
else if (T is '+', '-', '+', or '/') 
{ 
create a node and store T in it; 
if ((operator stack is empty) or 
(the value at the top of the operator stack is '(') or 
(priority(operator at top of stack) < priority(T))) 
{
push the node onto the operator stack;
} 
else // clear operator stack and push new one onto it 
{
do 
{
PopConnectPush;
} 
while ((the operator stack is not empty) and 
(the top of the operator stack is not '(') and 
(priority(T) < priority(operator at top of stack)));

create a node and store T in it; 
push the node onto operator stack;
} 
else if (T is ')') // clear operator stack back to the '(' 
{
while (top of operator stack is not '(') 
{
PopConnectPush;
}
} 
else 
{
report error!
} 
}

// no more tokens left in expression 
while (operator stack is not empty) 
{
PopConnectPush;
}

// pointer to root of final tree is on top of the tree stack
}
         */


        FormulaParser(string formulaToParse)
        {
        }
    }
}
