namespace PrimeNumbers.TestApp.Modules
{
    internal class InvalidTest : ITest
    {
        public int ExpectedStatusCodeWithRange { get; } = 400;
        public int ExpectedStatusCodeWithNumber { get; } = 404;
        public string RequestWithRange { get; set; }
        public string RequestNumber { get; set; }

        public InvalidTest(string requestWithRange, string requestNumber)
        {
            RequestNumber = requestNumber;
            RequestWithRange = requestWithRange;
        }
    }
}
