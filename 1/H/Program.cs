using System.Collections.Generic;
using System.Text;

namespace H
{
    internal static class Program
    {
        private static long MOD = 998_244_353;
        private static int N = 5_001;

        private static readonly long[] Factorial;

        static Program()
        {
            Factorial = new long[N];
            Factorial[0] = 1;
            for (int i = 1; i < N; i++)
            {
                Factorial[i] = MulModulo(Factorial[i - 1], i);
            }
        }

        private static long Pow(long a, long b)
        {
            if (b == 0)
            {
                return 1;
            }

            long res = Pow(a, b / 2);
            res = MulModulo(res, res);
            if (b % 2 == 1)
            {
                res = MulModulo(res, a);
            }

            return res;
        }

        private static long Binomial(long n, long k)
        {
            if (n < k)
            {
                return 0;
            }

            return DivModulo(Factorial[n], MulModulo(Factorial[k], Factorial[n - k]));
        }

        private static long GetModulo(long a)
        {
            return a >= 0 ? a % MOD : a + MOD;
        }

        private static long AddModulo(long a, long b)
        {
            return (a + b) % MOD;
        }

        private static long MulModulo(long a, long b)
        {
            return a * b % MOD;
        }

        private static long DivModulo(long a, long b)
        {
            return MulModulo(a, Pow(b, MOD - 2));
        }

        private static List<long> Get(int k)
        {
            var result = new List<long> {1};
            for (var i = 1;; i++)
            {
                long c = Binomial(k - i, i);
                if (c == 0)
                {
                    break;
                }

                result.Add(i % 2 == 0 ? c : GetModulo(-c));
            }

            return result;
        }

        private static List<long> DivGenFunction(List<long> numerator, List<long> denominator, int n)
        {
            var result = new List<long>();
            for (int i = 0; i < n; i++)
            {
                var sum = i < numerator.Count ? numerator[i] : 0L;
                for (int j = 1; j <= i && j < denominator.Count; j++)
                {
                    sum = AddModulo(sum, GetModulo(-MulModulo(denominator[j], result[i - j])));
                }
                
                result.Add(sum);
            }

            return result;
        }

        public static void Main(string[] args)
        {
            int k, n;
            {
                var numbers = System.Console.ReadLine()?.Split(' ');
                if (numbers == null)
                {
                    throw new System.ArgumentException();
                }

                k = int.Parse(numbers[0]);
                n = int.Parse(numbers[1]);
            }

            var numerator = Get(k - 2);
            var denominator = Get(k - 1);
            var result = DivGenFunction(numerator, denominator, n);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var c in result)
            {
                stringBuilder.Append(c).Append(System.Environment.NewLine);
            }

            System.Console.WriteLine(stringBuilder.ToString());
        }
    }
}