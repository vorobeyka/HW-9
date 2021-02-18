using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeNumbers.TestApp.Models
{
    internal static class PrimeNumbers
    {
        public static IEnumerable<int> FromRange(int from, int to)
        {
            var result = Enumerable.Empty<int>();
            try
            {
                var numbers = Enumerable.Range(from, to - from + 1);
                result = numbers.Where(x => IsPrime(x));
            }
            catch (ArgumentException) { }
            return result;
        }

        public static bool IsPrime(int number)
        {
            if (number < 2) return false;
            if (number == 2) return true;

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}
