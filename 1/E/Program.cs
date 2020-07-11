using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E
{
    internal class Program
    {
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

        private static List<long> MultiplyGenFunctions(List<long> first, List<long> second, int m)
        {
            var result = Enumerable.Repeat(0L, System.Math.Min(m, first.Count + second.Count - 1)).ToList();
            for (int i = 0; i < first.Count; i++)
            {
                for (int j = 0; j < second.Count && i + j < m; j++)
                {
                    result[i + j] = result[i + j] + first[i] * second[j];
                }
            }

            return result;
        }

        private static string ListToString(List<long> result)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(result.Count - 1).Append(System.Environment.NewLine);
            foreach (var num in result)
            {
                stringBuilder.Append(num).Append(' ');
            }
            return stringBuilder.ToString();
        }

        private static long Pow(long a, long b)
        {
            var result = 1L;
            for (long i = 0; i < b; i++)
            {
                result *= a;
            }
            return result;
        }

        public static void Main()
        {
            var r = int.Parse(System.Console.ReadLine() ?? throw new System.ArgumentException());
            var d = int.Parse(System.Console.ReadLine() ?? throw new System.ArgumentException());
            var genFunction = GetNumbers();
            var denominator = new List<long> {1, -r};
            for (int i = 0; i < d; i++)
            {
                denominator = MultiplyGenFunctions(denominator, new List<long> {1, -r}, d + 2);
            }

            var coefficients = Enumerable.Repeat(0L, d + 1).ToList();
            for (long n = 0; n <= d; n++)
            {
                var x = 1L;
                for (int i = 0; i < genFunction.Count; i++, x *= n)
                {
                    coefficients[(int)n] += x * genFunction[i] * Pow(r, n);
                }
            }

            var numerator = MultiplyGenFunctions(coefficients, denominator, d + 1);
            System.Console.WriteLine(ListToString(numerator));
            System.Console.WriteLine(ListToString(denominator));
        }
    }
}