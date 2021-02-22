namespace PrimeNumbers.TestApp.Models.Tests
{
    internal interface ITest
    {
        int ExpectedStatusCodeWithRange { get; }
        int ExpectedStatusCodeWithNumber { get; }
        string RequestWithRange { get; }
        string RequestNumber { get; }

        ITest SetNewRequests(string requestNumber, string requestWithRange);
    }
}
