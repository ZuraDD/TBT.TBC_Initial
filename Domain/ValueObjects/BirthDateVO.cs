using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public class BirthDateVO : ValueObject
    {
        public DateTime Value { get; private set; }

        public static BirthDateVO Create(DateTime birthDate)
        {
            var instance = new BirthDateVO { Value = new DateTime(birthDate.Year, birthDate.Month, birthDate.Day) };

            ValidateInstance(instance);

            return instance;
        }

        public static void ValidateInstance(BirthDateVO instance)
        {
            // Validations goes here

            if (
                instance.Value == default
                ||
                instance.Value > DateTime.UtcNow.AddYears(-18)
            )
                throw new DomainException(DomainExceptionCode.InvalidAge);
        }

        public static implicit operator string(BirthDateVO instance)
        {
            return instance.ToString();
        }

        public static explicit operator BirthDateVO(DateTime value)
        {
            return Create(value);
        }

        public override string ToString()
        {
            return $"{Value:MM/dd/yyyy}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
