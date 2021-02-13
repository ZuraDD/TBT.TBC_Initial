using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public class PersonalNumberVO : ValueObject
    {
        public string Value { get; private set; }

        public static PersonalNumberVO Create(string input)
        {
            var instance = new PersonalNumberVO { Value = input };

            ValidateInstance(instance);

            return instance;
        }

        public static void ValidateInstance(PersonalNumberVO instance)
        {
            // Validations goes here

            if (
                string.IsNullOrWhiteSpace(instance.Value)
                ||
                instance.Value.Length != 11
                ||
                !Regex.Match(instance.Value, "^([0-9]+)$", RegexOptions.IgnoreCase).Success
            )
                throw new DomainException(DomainExceptionCode.InvalidPersonalNumber);
        }

        public static implicit operator string(PersonalNumberVO instance)
        {
            return instance.ToString();
        }

        public static explicit operator PersonalNumberVO(string value)
        {
            return Create(value);
        }

        public override string ToString()
        {
            return Value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
