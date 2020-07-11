using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace I
{
    internal static class Program
    {
        private static long MOD = 104_857_601;

        private static List<long> GetNumbers()
        {
            var numbers = System.Console.ReadLine()?.Split(' ');
            if (numbers == null)
            {
                throw new System.ArgumentException();
            }

            var result = new List<long>(numbers.Length);
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

        private static long GetModulo(long a)
        {
            if (a >= 0)
            {
                return a % MOD;
            }
            else
            {
                return a + MOD;
            }
        }

        private static long MulModulo(long a, long b)
        {
            return a * b % MOD;
        }

        private static List<long> MultiplyGenFunctions(List<long> first, List<long> second)
        {
            var result = Enumerable.Repeat(0L, first.Count + second.Count - 1).ToList();
            for (int i = 0; i < first.Count; i++)
            {
                for (int j = 0; j < second.Count; j++)
                {
                    result[i + j] = AddModulo(result[i + j], MulModulo(first[i], second[j]));
                }
            }

            return result;
        }

        private static long GetN(List<long> a, List<long> c, long n)
        {
            var q = new List<long>(c.Count + 1) {1L};
            foreach (var cI in c)
            {
                q.Add(GetModulo(-cI));
            }

            var k = a.Count;
            while (n >= k)
            {
                for (int i = k; i < 2 * k; i++)
                {
                    long aI = 0;
                    for (int j = 1; j <= k; j++)
                    {
                        aI = AddModulo(aI, MulModulo(GetModulo(-q[j]), a[i - j]));
                    }

                    a.Add(aI);
                }

                var qMinus = new List<long>(q.Count);
                for (int i = 0; i < q.Count; i++)
                {
                    qMinus.Add(i % 2 == 0 ? q[i] : GetModulo(-q[i]));
                }

                var r = MultiplyGenFunctions(q, qMinus);
                var newA = new List<long>();
                for (var i = (int) (n % 2); i < a.Count; i += 2)
                {
                    newA.Add(a[i]);
                }

                a = newA;
                var newQ = new List<long>();
                for (var i = 0; i < r.Count; i += 2)
                {
                    newQ.Add(r[i]);
                }

                q = newQ;
                n /= 2;
            }

            return a[(int) n];
        }

        public static void Main(string[] args)
        {
            long n = GetNumbers()[1];
            var a = GetNumbers();
            var c = GetNumbers();
            System.Console.WriteLine(GetN(a, c, n - 1));
        }
    }
}