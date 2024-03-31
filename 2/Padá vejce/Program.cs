using System.Reflection.Metadata.Ecma335;

namespace Padá_vejce
{
    internal class Program
    {
        static int U(int n, int k, Dictionary<string, int> memo)
        {
            if (k == 1 || n == 1) return n;
            if (n < 1) return 0;
            string key = n.ToString() + "_" + k.ToString();
            if (!(memo.ContainsKey(key)))
            {
                int minI = Int32.MaxValue;
                int minAmount = Int32.MaxValue;
                int left = 1;
                int right = n / 2;
                while (left != right)
                {
                    int leftBrokeAmount = U(left - 1 , k - 1, memo) + 1;
                    int leftNotBrokeAmount = U(n - left , k, memo) + 1;

                    int rightBrokeAmount = U(right- 1, k - 1, memo) + 1;
                    int rightNotBrokeAmount = U(n - right, k, memo) + 1;

                    int leftAmount = Math.Max(leftBrokeAmount, leftNotBrokeAmount);
                    int rightAmount = Math.Max(rightBrokeAmount, rightNotBrokeAmount);
                    if (leftAmount < rightAmount)
                    {
                        right = (left + right) / 2;
                    }
                    else left = (right + left) / 2;
                }
                int oneAmount = U(left-1, k-1, memo) + 1;
                int secondAmount = U(n-left, k, memo) + 1;
                minAmount = Math.Max(oneAmount, secondAmount);
                //Console.WriteLine(key + " " + minAmount);
                memo.Add(key, minAmount);
            }
            return memo[key];
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            while (true)
            {
                var input = Console.ReadLine().Split();
                Console.WriteLine(U(Convert.ToInt32(input[0]), Convert.ToInt32(input[1]), new Dictionary<string, int>()));
            }
        }
    }
}