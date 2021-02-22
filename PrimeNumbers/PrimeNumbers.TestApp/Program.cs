using System;
using System.Threading.Tasks;
using PrimeNumbers.TestApp.Models.Tests;

namespace PrimeNumbers.TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var validator = TestValidator.GetInstance();

            //test for root "/"
            await validator.TestRoot();

            //valid tests
            var validTest = new ValidTest("/primes/7", "/primes?from=1&to=5");
            await validator.TestRequest(validTest, 7, (1, 5));
            await validator.TestRequest(validTest.SetNewRequests("/primes/2", "/primes?from=5&to=17"), 2, (5, 17));
            await validator.TestRequest(validTest.SetNewRequests("/primes/11", "/primes?from=-5&to=1"), 11, (-5, 1));

            //invalid tests
            var invalidTest = new InvalidTest("/primes/1", "/primes?to=abc");
            await validator.TestRequest(invalidTest, 1, (null, "abc"));
            await validator.TestRequest(invalidTest.SetNewRequests("/primes/4", "/primes?from=a&to=5"), 4, ("a", 5));
            await validator.TestRequest(invalidTest.SetNewRequests("/primes/0", "/primes?from=1"), 0, (1, null));
        }
    }
}
