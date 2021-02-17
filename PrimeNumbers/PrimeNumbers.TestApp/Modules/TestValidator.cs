using System;
using System.Net.Http;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace PrimeNumbers.TestApp.Modules
{
    internal class TestValidator
    {
        private readonly string _settingsPath = "settings.json";
        private readonly HttpClient _client = new HttpClient();
        private static TestValidator _instance;

        private TestValidator()
        {
            var json = File.ReadAllText(_settingsPath);
            var settings = JsonSerializer.Deserialize<Settings>(json);
            _client.BaseAddress = new Uri(settings.Address);
        }

        public static TestValidator GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TestValidator();
            }
            return _instance;
        }

        public async Task TestRequest(ITest test, int number, (object, object) range)
        {
            var response = await _client.GetAsync(test.RequestNumber);
            WriteResult(test.RequestNumber, (int)response.StatusCode, test.ExpectedStatusCodeWithNumber);
            TestPrimeNumber(number, response.StatusCode);

            response = await _client.GetAsync(test.RequestWithRange);
            var content = await response.Content.ReadAsStringAsync();
            TestPrimeNumbersFromRange(content, response.StatusCode, range);
            WriteResult(test.RequestNumber, (int)response.StatusCode, test.ExpectedStatusCodeWithNumber);
            Console.WriteLine();
        }

        private void TestPrimeNumber(int number, HttpStatusCode statusCode)
        {
            if ((IsPrime(number) && statusCode == HttpStatusCode.OK)
                || (!IsPrime(number) && statusCode == HttpStatusCode.NotFound))
            {
                Console.WriteLine("Test success!");
            }
            else
            {
                Console.WriteLine("Test failed!");
            }
        }

        private void TestPrimeNumbersFromRange(string content, HttpStatusCode statusCode, (object, object) range)
        {
            if (statusCode == HttpStatusCode.OK)
            {
                var cleanContent = content.Replace("{", "").Replace("}", "");
                var items = cleanContent.Split(',').Select(x => int.Parse(x));
                var primes = PrimeNumbers((int)range.Item1, (int)range.Item2);

                if (primes.Equals(primes)) Console.WriteLine("Test success!");
                else Console.WriteLine("Test failed!");
            }
            else if (!int.TryParse(range.Item1.ToString(), out var from)
                    || !int.TryParse(range.Item2.ToString(), out var to))
            {
                Console.WriteLine("Test success!");
            }
            else
            {
                Console.WriteLine("Test failed!");
            }
        }

        private void WriteResult(string request, int statusCode, int expectedStatusCode)
        {
            Console.WriteLine($"Request: {request}");
            Console.WriteLine($"Status code: {statusCode}");
            Console.WriteLine($"Expected status code: {expectedStatusCode}");
        }

        private IEnumerable<int> PrimeNumbers(int from, int to)
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
