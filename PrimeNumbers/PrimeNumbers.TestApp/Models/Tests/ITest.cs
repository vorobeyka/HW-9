namespace PrimeNumbers.TestApp.Models.Tests
{
    internal interface ITest
    {
        int ExpectedStatusCodeWithRange { get; }
        int ExpectedStatusCodeWithNumber { get; }
        string RequestWithRange { get; set; }
        string RequestNumber { get; set; }

        ITest SetNewRequests(string requestNumber, string requestWithRange)
        {
            RequestWithRange = requestWithRange;
            RequestNumber = requestNumber;
            return this;
        }
    }
}
