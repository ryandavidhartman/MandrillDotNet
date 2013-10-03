using System;

namespace MandrillWrapper.Model.Responses
{
    public class MandrillException : Exception
    {
        public ErrorResponse Error { get; private set; }

        public MandrillException() { }

        public MandrillException(string message) : base(message)
        {
        }

        public MandrillException(ErrorResponse error, string message) : base(message)
        {
            Error = error;
        }

        public MandrillException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
