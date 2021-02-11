using System;
using Domain.Enums;

namespace Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(DomainExceptionCodes code)
            : base(code.ToString())
        {
        }
    }
}
