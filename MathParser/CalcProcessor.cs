using System;
using System.Collections.Generic;
using System.Text;

namespace SevenZ.Calculator
{
    public partial class Calculator
    {
        /// <summary>
        /// Calculates binary expressions and pushes the result into the operands stack
        /// </summary>
        /// <param name="op">Binary operator</param>
        /// <param name="operand1">First operand</param>
        /// <param name="operand2">Second operand</param>
        private void Calculate(string op, double operand1, double operand2)
        {
            double res = 0;
            try
            {
                switch (op)
                {
                    case Token.Add: res = operand1 + operand2; break;
                    case Token.Subtract: res = operand1 - operand2; break;
                    case Token.Multiply: res = operand1 * operand2; break;
                    case Token.Divide: res = operand1 / operand2; break;
                    case Token.Mod: res = operand1 % operand2; break;
                    case Token.Power: res = Math.Pow(operand1, operand2); break;
                    case Token.Log: res = Math.Log(operand2, operand1); break;
                    case Token.Root: res = Math.Pow(operand2, 1 / operand1); break;
                }

                operands.Push(PostProcess(res));
            }
            catch (Exception e)
            {
                ThrowException(e.Message);
            }
        }


        /// <summary>
        /// Calculates unary expressions and pushes the result into the operands stack
        /// </summary>
        /// <param name="op">Unary operator</param>
        /// <param name="operand">Operand</param>
        private void Calculate(string op, double operand)
        {
            double res = 1;

            try
            {
                switch (op)
                {
                    case Token.UnaryMinus: res = -operand; break;
                    case Token.Abs: res = Math.Abs(operand); break;
                    case Token.ACosine: res = Math.Acos(operand); break;
                    case Token.ASine: res = Math.Asin(operand); break;
                    case Token.ATangent: res = Math.Atan(operand); break;
                    case Token.Cosine: res = Math.Cos(operand); break;
                    case Token.Ln: res = Math.Log(operand); break;
                    case Token.Log10: res = Math.Log10(operand); break;
                    case Token.Sine: res = Math.Sin(operand); break;
                    case Token.Sqrt: res = Math.Sqrt(operand); break;
                    case Token.Tangent: res = Math.Tan(operand); break;
                    case Token.Exp: res = Math.Exp(operand); break;
                    case Token.Factorial: for (int i = 2; i <= (int)operand; res *= i++) ;
                        break;
                }

                operands.Push(PostProcess(res));
            }
            catch (Exception e)
            {
                ThrowException(e.Message);
            }

        }

        /// <summary>
        /// Result post-processing
        /// </summary>
        private double PostProcess(double result)
        {
            return Math.Round(result, 10);
        }
    }

}