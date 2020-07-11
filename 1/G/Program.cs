using System.Numerics;
using System.Collections.Generic;
using System.Linq;

namespace G
{
    internal class Program
    {
        private static List<BigInteger> GetB()
        {
            var result = Enumerable.Repeat(BigInteger.Zero, 7).ToList();
            result[1] = BigInteger.One;

            return result;
        }

        private static List<BigInteger> SubtractGenFunction(List<BigInteger> first, List<BigInteger> second)
        {
            var result = new List<BigInteger>(7);
            for (int i = 0; i < 7; i++)
            {
                result.Add(first[i] - second[i]);
            }

            return result;
        }

        private static List<BigInteger> MultiplyGenFunction(List<BigInteger> first, List<BigInteger> second)
        {
            var result = Enumerable.Repeat(BigInteger.Zero, 7).ToList();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; i + j < 7; j++)
                {
                    result[i + j] += first[i] * second[j];
                }
            }

            return result;
        }

        private static List<BigInteger> DivideGetFunction(List<BigInteger> first, List<BigInteger> second)
        {
            var result = new List<BigInteger>(7);
            for (int i = 0; i < 7; i++)
            {
                result.Add(first[i]);
                for (int j = 0; j < i; j++)
                {
                    result[i] -= result[j] * second[i - j];
                }
            }

            return result;
        }

        private static List<BigInteger> GetL(List<BigInteger> a)
        {
            a[0] = 0;
            var numerator = Enumerable.Repeat(BigInteger.Zero, 7).ToList();
            numerator[0] = 1;
            var denominator = SubtractGenFunction(numerator, a);
            return DivideGetFunction(numerator, denominator);
        }

        private static BigInteger Factorial(BigInteger n)
        {
            BigInteger result = 1;
            for (var i = 2; i <= n; i++)
            {
                result *= i;
            }

            return result;
        }

        private static BigInteger GetWay(BigInteger count, BigInteger n)
        {
            BigInteger result = 1;
            for (var i = 0; i < n; i++)
            {
                result *= count + i;
            }

            return result / Factorial(n);
        }

        private static List<BigInteger> GetS(List<BigInteger> a)
        {
            var result = Enumerable.Repeat(BigInteger.Zero, 7).ToList();
            for (int i1 = 0; i1 < 7; i1++)
            {
                for (int i2 = 0; i2 < 7; i2++)
                {
                    for (int i3 = 0; i3 < 7; i3++)
                    {
                        for (int i4 = 0; i4 < 7; i4++)
                        {
                            for (int i5 = 0; i5 < 7; i5++)
                            {
                                for (int i6 = 0; i6 < 7; i6++)
                                {
                                    int w = i1 + i2 * 2 + i3 * 3 + i4 * 4 + i5 * 5 + i6 * 6;
                                    if (w < 7)
                                    {
                                        result[w] += GetWay(a[1], i1) * GetWay(a[2], i2) * GetWay(a[3], i3) *
                                                     GetWay(a[4], i4) * GetWay(a[5], i5) * GetWay(a[6], i6);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        private static List<BigInteger> GetP(List<BigInteger> first, List<BigInteger> second)
        {
            return MultiplyGenFunction(first, second);
        }

        private static List<BigInteger> Parse(string expression, ref int pos)
        {
            var symbol = expression[pos++];
            switch (symbol)
            {
                case 'B':
                    return GetB();
                case 'L':
                {
                    pos++;
                    var argument = Parse(expression, ref pos);
                    pos++;
                    return GetL(argument);
                }
                case 'S':
                {
                    pos++;
                    var argument = Parse(expression, ref pos);
                    pos++;
                    return GetS(argument);
                }
                case 'P':
                    pos++;
                    var firstArgument = Parse(expression, ref pos);
                    pos++;
                    var secondArgument = Parse(expression, ref pos);
                    pos++;
                    return GetP(firstArgument, secondArgument);
                default:
                    throw new System.ArgumentException();
            }
        }

        public static void Main()
        {
            var expression = System.Console.ReadLine();
            var pos = 0;
            var result = Parse(expression, ref pos);
            foreach (var num in result)
            {
                System.Console.Write($"{num.ToString()} ");
            }
        }
    }
}