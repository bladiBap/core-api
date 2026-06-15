namespace TestDevCore.Core.Results
{
    public class DomainException : Exception
    {
        public Error Error { get; }

        public DomainException(Error error)
            : base(error.Message)
        {
            Error = error;
        }

        public DomainException(Error error, Exception originalException)
            : base(error.Message, originalException)
        {
            Error = error;
        }
    }
}
