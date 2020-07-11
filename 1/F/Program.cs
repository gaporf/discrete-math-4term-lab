using System.Collections.Generic;
using System.Linq;

namespace F
{
    internal class Program
    {
        private static long MOD = 1_000_000_007;

        private static List<long> GetNumbers()
        {
            var result = new List<long>();
            var numbers = System.Console.ReadLine()?.Split(' ');
            if (numbers == null)
            {
                throw new System.ArgumentException();
            }

            foreach (var number in numbers)
            {
                result.Add(long.Parse(number));
            }

            return result;
        }

        private static long AddModulo(long a, long b)
        {
            return (a + b) % MOD;
        }

        private static long MulModulo(long a, long b)
        {
            return a * b % MOD;
        }

        public static void Main(string[] args)
        {
            int m = int.Parse(System.Console.ReadLine()?.Split(' ')[1] ?? throw new System.ArgumentException());
            var cs = GetNumbers();
            var p = Enumerable.Repeat(0L, m + 1).ToList();
            var sum = Enumerable.Repeat(0L, m + 1).ToList();
            sum[0] = 1;
            p[0] = 1;
            for (int i = 1; i <= m; i++)
            {
                foreach (var c in cs)
                {
                    if (c <= i)
                    {
                        p[i] = AddModulo(p[i], sum[i - (int) c]);
                    }
                }

                System.Console.Write($"{p[i].ToString()} ");
                for (int k = 0; k <= i; k++)
                {
                    sum[i] = AddModulo(sum[i], MulModulo(p[i - k], p[k]));
                }
            }
        }
    }
}