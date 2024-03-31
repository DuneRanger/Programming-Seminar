namespace Rada
{
    internal class Program
    {
        static int DynamicRecursion(int inp, Dictionary<int, int> memo)
        {
            if (inp <= 0)
            {
                return 0;
            }
            if (inp <= 2) { return inp + 1;  }
            if (!memo.ContainsKey(inp))
            {
                int total = 0;
                total += DynamicRecursion(inp - 1, memo);
                total += DynamicRecursion(inp - 2, memo);
                memo.Add(inp, total);
            }
            return memo[inp];
        }
        static void Main(string[] args)
        {
            while (true) {
                Console.Write("Input:");
                var input = Console.ReadLine();
                Dictionary<int, int> memo = new Dictionary<int, int>();
                int inp = Convert.ToInt32(input);
                int answer = DynamicRecursion(inp, memo);
                Console.WriteLine(answer);
            }
        }
    }
}