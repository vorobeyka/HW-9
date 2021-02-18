namespace PrimeNumbers.TestApp.Models.Tests
{
    internal class ValidTest : ITest
    {
        public int ExpectedStatusCodeWithRange { get; } = 200;
        public int ExpectedStatusCodeWithNumber { get; } = 200;
        public string RequestWithRange { get; set; }
        public string RequestNumber { get; set; }

        public ValidTest(string requestNumber, string requestWithRange)
        {
            RequestNumber = requestNumber;
            RequestWithRange = requestWithRange;
        }
    }
}
