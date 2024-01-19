using System;
namespace EVENTING
{
    using System;

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to the Generic Calculator Program!");

            Console.Write("Enter the first value: ");
            double value1 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter the second value: ");
            double value2 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter the operation (+, -, *, /): ");
            string operation = Console.ReadLine();

            Calculator<double> doubleCalculator = new Calculator<double>();
            doubleCalculator.CalculationPerformed += DisplayResult;

            try
            {
                double result = 0;

                switch (operation)
                {
                    case "+":
                        result = doubleCalculator.Add(value1, value2);
                        break;
                    case "-":
                        result = doubleCalculator.Subtract(value1, value2);
                        break;
                    case "*":
                        result = doubleCalculator.Multiply(value1, value2);
                        break;
                    case "/":
                        result = doubleCalculator.Divide(value1, value2);
                        break;
                    default:
                        Console.WriteLine("Invalid operation. Please enter +, -, *, or /.");
                        return;
                }

                Console.WriteLine($"Result of {value1} {operation} {value2} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void DisplayResult(object sender, CalculationEventArgs<double> e)
        {
            Console.WriteLine($"Calculation Result: {e.Result}");
        }
    }
}