using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public class PhoneNumberVO : ValueObject
    {
        public PhoneNumberTypeEnum Type { get; private set; }

        public string Value { get; private set; }

        public static PhoneNumberVO Create(PhoneNumberTypeEnum type, string value)
        {
            var instance = new PhoneNumberVO { Type = type, Value = value };

            ValidateInstance(instance);

            return instance;
        }

        public static void ValidateInstance(PhoneNumberVO instance)
        {
            if (
                instance.Type == default
                ||
                Enumerable.Range(4, 50).Contains(instance.Value.Length)
                ||
                !Regex.Match(instance.Value, @"^([0-9])$", RegexOptions.IgnoreCase).Success
            )
                throw new DomainException(DomainExceptionCode.InvalidPhoneNumber);
        }

        public static implicit operator string(PhoneNumberVO instance)
        {
            return instance.ToString();
        }

        public override string ToString()
        {
            return $"Type - {Type}, LastName - {Value}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return Value;
        }
    }
}
