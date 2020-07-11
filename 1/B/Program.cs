using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace B
{
    internal class Program
    {
        private static List<long> ReadNumbers()
        {
            var res = new List<long>();
            string[] numbers = System.Console.ReadLine()?.Split(' ');
            if (numbers == null)
            {
                throw new System.ArgumentException();
            }

            foreach (var number in numbers)
            {
                res.Add(int.Parse(number));
            }

            return res;
        }

        private static long MOD = 998_244_353;

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

        private static void AddGenFunction(List<long> dist, List<long> add)
        {
            for (int i = 0; i < add.Count; i++)
            {
                dist[i] = AddModulo(dist[i], add[i]);
            }
        }

        private static List<long> MultiplyGenFunction(List<long> first, List<long> second, int m)
        {
            var result = Enumerable.Repeat(0L, System.Math.Min(m, first.Count + second.Count - 1)).ToList();
            for (int i = 0; i < first.Count; i++)
            {
                for (int j = 0; j < second.Count && i + j < m; j++)
                {
                    result[i + j] = AddModulo(result[i + j], MulModulo(first[i], second[j]));
                }
            }

            return result;
        }

        private static string ListToString(List<long> coefficients)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var coefficient in coefficients)
            {
                stringBuilder.Append(coefficient).Append(' ');
            }

            return stringBuilder.ToString();
        }

        private static string SquareRoot(List<long> genFunction, int m)
        {
            if (m == 1)
            {
                return "1";
            }

            var coefficients = Enumerable.Repeat(0L, m).ToList();
            coefficients[0] = 1;
            var x = Enumerable.Repeat(0L, genFunction.Count).ToList();
            for (int i = 0; i < x.Count; i++)
            {
                x[i] = genFunction[i];
            }

            long a = 1, b = 2;
            AddGenFunction(coefficients, MultiplyGenFunction(x, new List<long> {DivModulo(a, b)}, m));
            for (int i = 2; i < m; i++)
            {
                b = MulModulo(b, 2);
                b = MulModulo(b, i);
                a = MulModulo(a, 2 * i - 3);
                long coefficient = DivModulo(a, b);
                if (i % 2 == 0)
                {
                    coefficient = (MOD - coefficient) % MOD;
                }

                x = MultiplyGenFunction(x, genFunction, m);
                AddGenFunction(coefficients, MultiplyGenFunction(x, new List<long> {coefficient}, m));
            }

            return ListToString(coefficients);
        }

        private static string Exponent(List<long> genFunction, int m)
        {
            var coefficients = Enumerable.Repeat(0L, m).ToList();
            coefficients[0] = 1;
            var x = Enumerable.Repeat(0L, genFunction.Count).ToList();
            x[0] = 1;
            long b = 1;
            for (int i = 1; i < m; i++)
            {
                b = MulModulo(b, i);
                x = MultiplyGenFunction(x, genFunction, m);
                AddGenFunction(coefficients, MultiplyGenFunction(x, new List<long> {DivModulo(1L, b)}, m));
            }

            return ListToString(coefficients);
        }

        private static string Logarithm(List<long> genFunction, int m)
        {
            var coefficients = Enumerable.Repeat(0L, m).ToList();
            var x = Enumerable.Repeat(0L, genFunction.Count).ToList();
            x[0] = 1;

            for (int i = 1; i < m; i++)
            {
                x = MultiplyGenFunction(x, genFunction, m);
                long coefficient = DivModulo(1L, i);
                if (i % 2 == 0)
                {
                    coefficient = (MOD - coefficient) % MOD;
                }
                AddGenFunction(coefficients, MultiplyGenFunction(x, new List<long> {coefficient}, m));
            }

            return ListToString(coefficients);
        }

        public static void Main(string[] args)
        {
            int m = int.Parse(System.Console.ReadLine()?.Split(' ')[1] ?? throw new System.ArgumentException());
            var coefficients = ReadNumbers();
            System.Console.WriteLine(SquareRoot(coefficients, m));
            System.Console.WriteLine(Exponent(coefficients, m));
            System.Console.WriteLine(Logarithm(coefficients, m));
        }
    }
}