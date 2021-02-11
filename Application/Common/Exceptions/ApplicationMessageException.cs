using System;

namespace Application.Common.Exceptions
{
    public class ApplicationMessageException : Exception
    {
        public ApplicationMessageException()
            : base()
        {
        }

        public ApplicationMessageException(string message)
            : base(message)
        {
        }
    }
}
