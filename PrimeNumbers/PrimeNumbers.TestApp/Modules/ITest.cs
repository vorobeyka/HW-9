namespace PrimeNumbers.TestApp.Modules
{
    internal interface ITest
    {
        int ExpectedStatusCodeWithRange { get; }
        int ExpectedStatusCodeWithNumber { get; }
        string RequestWithRange { get; set; }
        string RequestNumber { get; set; }

        ITest SetNewRequests(string requestWithRange, string requestNumber)
        {
            RequestWithRange = requestWithRange;
            RequestNumber = requestNumber;
            return this;
        }
    }
}
