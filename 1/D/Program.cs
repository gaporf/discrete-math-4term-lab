using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace D
{
    internal class Program
    {
        private static BigInteger Pow(BigInteger a, BigInteger b)
        {
            BigInteger result = BigInteger.One;
            for (long i = 0; i < b; i++)
            {
                result = result * a;
            }

            return result;
        }

        private static List<BigInteger> ReadNumbers()
        {
            var result = new List<BigInteger>();
            var numbers = System.Console.ReadLine()?.Split(' ');
            if (numbers == null)
            {
                throw new System.ArgumentException();
            }

            foreach (var number in numbers)
            {
                result.Add(BigInteger.Parse(number));
            }

            return result;
        }

        private static BigInteger Factorial(BigInteger n)
        {
            var result = BigInteger.One;
            for (long i = 2; i <= n; i++)
            {
                result *= i;
            }

            return result;
        }

        private static List<BigInteger> MultiplyGenFunctions(List<BigInteger> first, List<BigInteger> second)
        {
            var result = Enumerable.Repeat(BigInteger.Zero, first.Count + second.Count - 1).ToList();
            for (int i = 0; i < first.Count; i++)
            {
                for (int j = 0; j < second.Count; j++)
                {
                    result[i + j] = result[i + j] + first[i] * second[j];
                }
            }

            return result;
        }

        public static void Main(string[] args)
        {
            long r, k;
            {
                var numbers = System.Console.ReadLine()?.Split(' ');
                if (numbers == null)
                {
                    throw new System.ArgumentException();
                }

                r = long.Parse(numbers[0]);
                k = long.Parse(numbers[1]);
            }
            var coefficients = ReadNumbers();
            var result = Enumerable.Repeat(BigInteger.Zero, (int) k + 1).ToList();
            var denominator = Factorial(k) * Pow(r, k);
            for (var i = 0; i <= k; i++)
            {
                var cur = new List<BigInteger> {coefficients[i] * Pow(r, k - i)};
                for (int j = 1; j <= k; j++)
                {
                    cur = MultiplyGenFunctions(cur, new List<BigInteger> {j - i, 1});
                }

                for (int j = 0; j <= k; j++)
                {
                    result[j] += cur[j];
                }
            }

            foreach (var numerator in result)
            {
                var gcd = BigInteger.GreatestCommonDivisor(denominator, BigInteger.Abs(numerator));
                
                System.Console.Write($"{(numerator / gcd).ToString()}/{(denominator / gcd).ToString()} ");
            }
        }
    }
}