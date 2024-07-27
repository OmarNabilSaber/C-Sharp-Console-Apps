using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    internal class MathExpression
    {
        public double LeftSideOperand { get; set; }
        public double RightSideOperand { get; set; }
        public MathOperation Operation { get; set; } = MathOperation.None;

        public override string ToString()
        {
            double result = Eval(out string OperationStr);
            var message = result == 0.0 ? "" : $"Left Side = {LeftSideOperand}    Operation = {Operation}    Right Side = {RightSideOperand} \n " +
                $"{LeftSideOperand} {OperationStr} {RightSideOperand} = {result} ";
            return message;
        }
        public double Eval(out string OperationStr)
        {
            switch (Operation)
            {
                case MathOperation.Addition:
                    OperationStr = "+";
                    return LeftSideOperand + RightSideOperand;
                case MathOperation.Subtraction:
                    OperationStr = "-";
                    return LeftSideOperand - RightSideOperand;
                case MathOperation.Division:
                    OperationStr = "/";
                    return LeftSideOperand / RightSideOperand;
                case MathOperation.Multiplication:
                    OperationStr = "*";
                    return LeftSideOperand * RightSideOperand;
                case MathOperation.Modulus:
                    OperationStr = "%";
                    return LeftSideOperand % RightSideOperand;
                case MathOperation.Power:
                    OperationStr = "^";
                    return Math.Pow(LeftSideOperand, RightSideOperand);
                case MathOperation.Sin:
                    OperationStr = "sin";
                    return Math.Sin(RightSideOperand);
                case MathOperation.Cos:
                    OperationStr = "cos";
                    return Math.Cos(RightSideOperand);
                case MathOperation.Tan:
                    OperationStr = "tan";
                    return Math.Tan(RightSideOperand);
                default:
                    OperationStr = " ";
                    return 0.0;
            }
        }
    }
}
