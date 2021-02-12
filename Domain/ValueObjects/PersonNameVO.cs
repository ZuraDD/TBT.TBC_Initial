using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public class PersonNameVO : ValueObject
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public static PersonNameVO Create(string firstName, string lastName)
        {
            var instance = new PersonNameVO { FirstName = firstName, LastName = lastName};

            ValidateInstance(instance);

            return instance;
        }


        public static void ValidateInstance(PersonNameVO personName)
        {
            // Validations goes here

            if (
                string.IsNullOrWhiteSpace(personName.FirstName)
                ||
                string.IsNullOrWhiteSpace(personName.LastName)
                ||
                !Regex.Match(personName.FirstName, @"^([a-zA-Z]+|[ა-ჰ]+)$", RegexOptions.IgnoreCase).Success
                ||
                !Regex.Match(personName.LastName, @"^([a-zA-Z]+|[ა-ჰ]+)$", RegexOptions.IgnoreCase).Success
                ||
                Enumerable.Range(2, 50).Contains(personName.FirstName.Length)
                ||
                Enumerable.Range(2, 50).Contains(personName.LastName.Length)
            )
                throw new DomainException(DomainExceptionCode.InvalidPersonName);
        }

        public static implicit operator string(PersonNameVO personName)
        {
            return personName.ToString();
        }

        public override string ToString()
        {
            return $"FirstName - {FirstName}, LastName - {LastName}"; ;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
