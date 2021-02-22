namespace PrimeNumbers.TestApp.Models.Tests
{
    internal class InvalidTest : ITest
    {
        public int ExpectedStatusCodeWithRange { get; } = 400;
        public int ExpectedStatusCodeWithNumber { get; } = 404;
        public string RequestWithRange { get; private set; }
        public string RequestNumber { get; private set; }

        public InvalidTest(string requestNumber, string requestWithRange)
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
