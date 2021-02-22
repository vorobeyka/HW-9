namespace PrimeNumbers.TestApp.Models.Tests
{
    internal class ValidTest : ITest
    {
        public int ExpectedStatusCodeWithRange { get; } = 200;
        public int ExpectedStatusCodeWithNumber { get; } = 200;
        public string RequestWithRange { get; private set; }
        public string RequestNumber { get; private set; }

        public ValidTest(string requestNumber, string requestWithRange)
        {
            RequestNumber = requestNumber;
            RequestWithRange = requestWithRange;
        }

        public ITest SetNewRequests(string requestNumber, string requestWithRange)
        {
            RequestWithRange = requestWithRange;
            RequestNumber = requestNumber;
            return this;
        }
    }
}
