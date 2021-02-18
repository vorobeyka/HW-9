using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;

namespace PrimeNumbers.WebService.Services
{
    internal class PrimeNumbersService : IPrimeNumbersService
    {
        public Task<IEnumerable<int>> FromRange(int from, int to)
        {
            if (from > to || to < 2)
            {
                return Task.FromResult(Enumerable.Empty<int>());
            }

            var primes = Enumerable.Range(from, to - from + 1).Where(x => IsPrime(x));
            return Task.FromResult(primes);
        }

        public Task<bool> IsPrimeNumber(int number)
        {
            return Task.FromResult(IsPrime(number));
        }

        private static bool IsPrime(int number)
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
