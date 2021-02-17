namespace PrimeNumbers.TestApp.Modules
{
    internal class ValidTest : ITest
    {
        public int ExpectedStatusCodeWithRange { get; } = 400;
        public int ExpectedStatusCodeWithNumber { get; } = 404;
        public string RequestWithRange { get; set; }
        public string RequestNumber { get; set; }

        public ValidTest(string requestWithRange, string requestNumber)
        {
            RequestNumber = requestNumber;
            RequestWithRange = requestWithRange;
        }
    }
}
