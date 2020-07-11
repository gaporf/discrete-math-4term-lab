using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A
{
    internal class Program
    {
        private static long MOD = 998244353;

        private class Function
        {
            private readonly List<long> _coefficients;

            public Function(List<long> coefficients)
            {
                _coefficients = coefficients;
            }

            public long GetValue(int index)
            {
                return (index < _coefficients.Count) ? _coefficients[index] : 0;
            }

            public Function Add(Function another)
            {
                int newLen = System.Math.Max(_coefficients.Count, another._coefficients.Count);
                var coefficients = Enumerable.Repeat(0L, newLen).ToList();
                for (int i = 0; i < newLen; i++)
                {
                    coefficients[i] = (GetValue(i) + another.GetValue(i)) % MOD;
                }

                return new Function(coefficients);
            }

            public Function Multiply(Function another)
            {
                int newLen = _coefficients.Count + another._coefficients.Count - 1;
                var coefficients = Enumerable.Repeat(0L, newLen).ToList();
                for (int i = 0; i < _coefficients.Count; i++)
                {
                    for (int j = 0; j < another._coefficients.Count; j++)
                    {
                        var res = _coefficients[i] * another._coefficients[j] % MOD;
                        coefficients[i + j] = (coefficients[i + j] + res) % MOD;
                    }
                }

                return new Function(coefficients);
            }

            public override string ToString()
            {
                if (_coefficients == null || _coefficients.Count == 0)
                {
                    return $"0{System.Environment.NewLine}0";
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (var coefficient in _coefficients)
                    {
                        stringBuilder.Append(coefficient).Append(" ");
                    }

                    return $"{(_coefficients.Count - 1).ToString()}{System.Environment.NewLine}{stringBuilder}";
                }
            }
        }

        private static long SubWithMod(long a, long b)
        {
            a -= b;
            while (a < 0)
            {
                a += MOD;
            }

            return a % MOD;
        }

        private static string Divide(Function first, Function second)
        {
            var coefficients = Enumerable.Repeat(0L, 1000).ToList();
            for (int i = 0; i < 1000; i++)
            {
                coefficients[i] = first.GetValue(i);
                for (int j = 1; j <= i; j++)
                {
                    coefficients[i] = SubWithMod(coefficients[i], second.GetValue(j) * coefficients[i - j] % MOD) % MOD;
                }
            }

            var stringBuilder = new StringBuilder();
            foreach (var coefficient in coefficients)
            {
                stringBuilder.Append(coefficient).Append(' ');
            }

            return stringBuilder.ToString();
        }

        private static Function GetFunction()
        {
            var numbers = System.Console.ReadLine()?.Split(' ');
            if (numbers == null)
            {
                throw new System.ArgumentException();
            }

            var list = new List<long>();
            foreach (var t in numbers)
            {
                list.Add(int.Parse(t));
            }

            return new Function(list);
        }

        public static void Main()
        {
            System.Console.ReadLine();
            var first = GetFunction();
            var second = GetFunction();
            System.Console.WriteLine(first.Add(second));
            System.Console.WriteLine(first.Multiply(second));
            System.Console.WriteLine(Divide(first, second));
        }
    }
}