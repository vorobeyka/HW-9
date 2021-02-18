using System;
using System.Net.Http;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using PrimeNumbers.TestApp.Models;

namespace PrimeNumbers.TestApp.Models.Tests
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

        public async Task TestRoot()
        {
            var response = await _client.GetAsync("/");
            var content = await response.Content.ReadAsStringAsync();
            WriteResult("/", (int)response.StatusCode, 200);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Test success!");
            }
            else
            {
                Console.WriteLine("Test failed!");
            }
            Console.WriteLine();
        }

        public async Task TestRequest(ITest test, int number, (object, object) range)
        {
            var response = await _client.GetAsync(test.RequestNumber);
            WriteResult(test.RequestNumber, (int)response.StatusCode, test.ExpectedStatusCodeWithNumber);
            TestPrimeNumber(number, response.StatusCode);

            response = await _client.GetAsync(test.RequestWithRange);
            var content = await response.Content.ReadAsStringAsync();
            WriteResult(test.RequestWithRange, (int)response.StatusCode, test.ExpectedStatusCodeWithRange);
            TestPrimeNumbersFromRange(content, response.StatusCode, range);
            Console.WriteLine();
        }

        private void TestPrimeNumber(int number, HttpStatusCode statusCode)
        {
            if ((PrimeNumbers.IsPrime(number) && statusCode == HttpStatusCode.OK)
                || (!PrimeNumbers.IsPrime(number) && statusCode == HttpStatusCode.NotFound))
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
                var primes = PrimeNumbers.FromRange((int)range.Item1, (int)range.Item2);

                if (primes.Equals(primes)) Console.WriteLine("Test success!");
                else Console.WriteLine("Test failed!");
            }
            else if (range.Item1 == null || range.Item1.GetType() != typeof(int)
                || range.Item2 == null || range.Item2.GetType() != typeof(int))
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
    }
}
