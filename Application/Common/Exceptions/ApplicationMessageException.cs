using System;
using Application.Common.Enums;

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

        public ApplicationMessageException(ApplicationExceptionCode code)
            : base(code.ToString())
        {
        }
    }
}
