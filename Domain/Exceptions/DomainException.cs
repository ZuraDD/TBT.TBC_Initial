using System;
using Domain.Enums;

namespace Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainExceptionCode Code { get; set; }

        public DomainException(string message, DomainExceptionCode code)
            : base(message)
        {
            Code = code;
        }
    }
}
