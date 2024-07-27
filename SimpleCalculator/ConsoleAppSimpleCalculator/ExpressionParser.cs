using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest6
{
    internal static class ExpressionParser
    {
        private const string MathSymbls = "+*?%^";
        public static MathExpression Parse(string input)
        {
            var expr = new MathExpression();
            StringBuilder token = new StringBuilder();
            bool leftSideInitialized = false;

            for (var i = 0; i < input.Length; i++)
            {
                var currentChar = input[i];
                if (char.IsDigit(currentChar))
                {
                    token.Append(currentChar); 
                    if ( i == input.Length - 1 && leftSideInitialized)
                    {
                        expr.RightSideOperand = double.Parse(token.ToString());
                    }
                }
                else if (MathSymbls.Contains(currentChar))
                {
                    if (!leftSideInitialized)
                    {
                        expr.LeftSideOperand = double.Parse(token.ToString());
                        leftSideInitialized = true;
                    }
                    expr.Operation = ParseMathOperation(currentChar.ToString());
                    token.Clear();
                }
                else if ((currentChar == '-') && (i > 0))
                {
                    if (expr.Operation == MathOperation.None)
                    {
                        if (!leftSideInitialized)
                        {
                            expr.LeftSideOperand = double.Parse(token.ToString());
                            leftSideInitialized = true;
                        }
                        expr.Operation = MathOperation.Subtraction;
                        token.Clear();
                    }
                    else if (expr.Operation != MathOperation.None)
                    {
                        token.Append(currentChar);
                    }
                }
                else if (char.IsLetter(currentChar))
                {
                    leftSideInitialized = true;
                    token.Append(currentChar);
                }
                else if (char.IsWhiteSpace(currentChar))
                {
                    if (!leftSideInitialized)
                    {
                        expr.LeftSideOperand = double.Parse(token.ToString());
                        leftSideInitialized = true;
                        token.Clear();
                    }
                    else if (expr.Operation == MathOperation.None)
                    {
                        expr.Operation = ParseMathOperation(token.ToString());
                        token.Clear();
                    }
                }
                else
                    { token.Append(currentChar); }

            }
            return expr;
        }

        private static MathOperation ParseMathOperation(string operation)
        {
            switch (operation)
            {
                case "+":
                    return MathOperation.Addition;
                case "*":
                    return MathOperation.Multiplication;
                case "/":
                    return MathOperation.Division;
                case "%":
                    return MathOperation.Modulus;
                case "^":
                case "pow":
                    return MathOperation.Power;
                case "sin":
                    return MathOperation.Sin;
                case "cos":
                    return MathOperation.Cos;
                case "tan":
                    return MathOperation.Tan;
                default:
                    return MathOperation.None;


            }
        }
    }
}
