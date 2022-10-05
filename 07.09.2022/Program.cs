using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07._09
{
    class Program
    {
        static void inputAddition()
        {
            Console.Write("Enter the first number: ");
            int add1 = int.Parse(Console.ReadLine());
            Console.Write("Enter the second number: ");
            int add2 = int.Parse(Console.ReadLine());
            Console.WriteLine("The result is: " + (add1 + add2));
        }

        static void simpleCalc()
        {
            Console.Write("Enter the first number: ");
            int calc1 = int.Parse(Console.ReadLine());
            Console.Write("Enter the operation (+, -, *, //): ");
            string calc2 = Console.ReadLine();
            Console.Write("Enter the second number: ");
            int calc3 = int.Parse(Console.ReadLine());

            int calcOut = 0;
            switch (calc2)
            {
                case "+":
                    calcOut = calc1 + calc3;
                    break;
                case "-":
                    calcOut = calc1 - calc3;
                    break;
                case "*":
                    calcOut = calc1 * calc3;
                    break;
                case "//":
                    calcOut = calc1 / calc3;
                    break;
            }
            Console.WriteLine("The result is: " + calcOut);
        }

        static void maxAndSortArray()
        {
            Console.WriteLine("Please enter 10 numbers:");
            int[] numbers = new int[10];

            for (int x = 0; x < 10; x++)
            {
                numbers[x] = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("The maximum is: " + numbers.Max());

            Array.Sort(numbers);

            Console.Write("This is the sorted array: ");
            foreach (int num in numbers)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();

        }

        static void replacePetrWithPavel()
        {
            Console.WriteLine("Enter a sentence with the name 'Petr':");
            string input = Console.ReadLine();

            input = input.Replace("Petr", "Pavel");

            Console.WriteLine("Improved sentence:");
            Console.WriteLine(input);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello world");

            inputAddition();

            simpleCalc();

            maxAndSortArray();

            replacePetrWithPavel();

            Console.ReadLine();
        }
    }
}
