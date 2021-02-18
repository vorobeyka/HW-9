namespace PrimeNumbers.TestApp.Models.Tests
{
    internal class InvalidTest : ITest
    {
        public int ExpectedStatusCodeWithRange { get; } = 400;
        public int ExpectedStatusCodeWithNumber { get; } = 404;
        public string RequestWithRange { get; set; }
        public string RequestNumber { get; set; }

        public InvalidTest(string requestNumber, string requestWithRange)
        {
            RequestNumber = requestNumber;
            RequestWithRange = requestWithRange;
        }
    }
}
