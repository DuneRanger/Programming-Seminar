using System;
using System.Collections;

namespace Dice_tower
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split();
            foreach (var num in input)
            {
                int num2 = Int32.Parse(num);
                if (num2%14 > 0 && num2%14 < 7 && num2 > 14)
                {
                    Console.WriteLine("YES");
                } else
                {
                    Console.WriteLine("NO");
                }
            }
        }
    }
}