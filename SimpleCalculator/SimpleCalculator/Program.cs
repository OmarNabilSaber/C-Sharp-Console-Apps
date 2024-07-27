namespace SimpleCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true) 
            {
                Console.WriteLine("Don'n use more than one operator to work correctly");
                Console.Write("Please, enter math expression: ");
                var mathExpression = Console.ReadLine().Trim();
                var parrsedMathExpression = ExpressionParser.Parse(mathExpression);
                Console.WriteLine(parrsedMathExpression.ToString());
            }
        }
    }
}
