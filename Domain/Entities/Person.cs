using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Person : ComplexEntity
    {
        public int Id { get; set; }

        public PersonNameVO Name { get; set; }

        public PersonalNumberVO PersonalNumber { get; set; }

        public BirthDateVO BirthDate { get; set; }

        public string RelativeImagePath { get; set; }

        public GenderTypeEnum? GenderTypeId { get; set; }
        public virtual GenderType GenderType { get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }

        public virtual ICollection<RelatedPerson> DirectRelatedPersons { get; set; }

        public virtual ICollection<RelatedPerson> IndirectRelatedPersons { get; set; }

        #region methods

        public static Person Create(
            string firstName, 
            string lastName, 
            string personalNumber, 
            DateTime birthDate , 
            GenderTypeEnum genderTypeId, 
            int cityId,
            GenderType genderType)
        {
            var nameVo = PersonNameVO.Create(firstName, lastName);
            var birthDateVo = BirthDateVO.Create(birthDate);
            var personalNumberVo = PersonalNumberVO.Create(personalNumber);

            var instance = new Person
            {
                Name = nameVo,
                BirthDate = birthDateVo,
                PersonalNumber = personalNumberVo,
                CityId = cityId,
                GenderType = genderType
            };

            Validate(instance);

            return instance;
        }


        public static void Validate(Person instance)
        {
            if (
                instance.GenderType == default
            )
                throw new DomainException(DomainExceptionCode.InvalidPerson);
        }

        #endregion
    }
}
