using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C
{
    internal class Program
    {
        private static List<long> GetNumbers()
        {
            var numbers = System.Console.ReadLine()?.Split(' ');
            if (numbers == null)
            {
                throw new System.ArgumentException();
            }

            var result = new List<long>();
            foreach (var number in numbers)
            {
                result.Add(long.Parse(number));
            }

            return result;
        }

        private static List<long> MultiplyGenFunction(List<long> first, List<long> second, int k)
        {
            var result = Enumerable.Repeat(0L, k).ToList();
            for (int i = 0; i < first.Count; i++)
            {
                for (int j = 0; j < second.Count && i + j < k; j++)
                {
                    result[i + j] = result[i + j] + first[i] * second[j];
                }
            }

            while (result.Count > 0 && result[result.Count - 1] == 0)
            {
                result.RemoveAt(result.Count - 1);
            }

            return result;
        }

        private static string ListToString(List<long> numbers)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(numbers.Count - 1).Append(System.Environment.NewLine);
            foreach (var number in numbers)
            {
                stringBuilder.Append(number).Append(' ');
            }

            return stringBuilder.ToString();
        }

        public static void Main(string[] args)
        {
            int k = (int) GetNumbers()[0];
            List<long> a = GetNumbers();
            List<long> c = GetNumbers();
            List<long> q = Enumerable.Repeat(0L, k + 1).ToList();
            q[0] = 1;
            for (int i = 1; i <= k; i++)
            {
                q[i] = -c[i - 1];
            }

            List<long> p = MultiplyGenFunction(a, q, k);
            System.Console.WriteLine(ListToString(p));
            System.Console.WriteLine(ListToString(q));
        }
    }
}