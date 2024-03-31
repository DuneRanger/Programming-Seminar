using System;
using System.Collections.Generic;

namespace Dalsi_dynamika
{
    internal class Program
    {
        static int DynamicRecursion(int n, bool state, Dictionary<int, Dictionary<bool, int>> memo)
        {
            if (n == 0 || n%2 == 1) return 0; if (n == 2) { return state ? 3 : 1; }
            if (!(memo.ContainsKey(n))) memo.Add(n, new Dictionary<bool, int>());
            if (!(memo[n].ContainsKey(state))) memo[n].Add(state, (state ? 3 : 1) * DynamicRecursion(n - 2, true, memo) + (state ? 2 : 1) * DynamicRecursion(n - 2, false, memo));
            return memo[n][state];
        }
        static void Main(string[] args)
        {
            //while (true)
            //{
                //Console.Write("Input: ");
                int input = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(DynamicRecursion(input, true, new Dictionary<int, Dictionary<bool, int>>()));
            //}
        }
    }
}