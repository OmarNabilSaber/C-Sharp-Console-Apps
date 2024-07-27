namespace SimpleCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true) 
            {
                Console.Write("Please, enter math expression: ");
                var mathExpression = Console.ReadLine().Trim();
                var parrsedMathExpression = ExpressionParser.Parse(mathExpression);
                Console.WriteLine(parrsedMathExpression.ToString());
            }
        }
    }
}
