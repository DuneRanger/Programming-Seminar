using System;
using System.Collections;
using System.Collections.Generic;

namespace Highway_builder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //while (true)
            //{

                int n = Int32.Parse(Console.ReadLine());
                int g = Int32.Parse(Console.ReadLine());
                int b = Int32.Parse(Console.ReadLine());

                int gDays = 0;
                int bDays = 0;


            if (g >= (n+1) / 2 || g > b) { Console.WriteLine(n); }
            else
            {
                    while (gDays + g < (n + 1) / 2)
                    {
                        gDays += g;
                        bDays += b;
                    }
                    gDays += Math.Min((n+1)/ 2 - gDays, g);
                    Console.WriteLine((Math.Max(gDays + bDays, n)).ToString());
                }
            //    Console.WriteLine();
            //}
        }
    }
}