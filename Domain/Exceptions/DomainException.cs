using System;
using Domain.Enums;

namespace Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainExceptionCode Code { get; set; }

        public DomainException(DomainExceptionCode code)
            : base(code.ToString())
        {
            Code = code;
        }
    }
}
