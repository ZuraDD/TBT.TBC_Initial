using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Enums;
using Domain.Events.PersonEvents;
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

        public virtual ICollection<Relation> DirectRelatedPersons { get; set; }

        public virtual ICollection<Relation> IndirectRelatedPersons { get; set; }

        #region methods

        public static Person Create(
            string firstName, 
            string lastName, 
            string personalNumber, 
            DateTime birthDate , 
            GenderTypeEnum genderTypeId, 
            int cityId)
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
                GenderTypeId = genderTypeId,
                PhoneNumbers = new List<PhoneNumber>()
            };

            Validate(instance);

            instance.DomainEvents.Add(new PersonCreatedEvent(instance));

            return instance;
        }

        public void Update(
            string firstName,
            string lastName,
            string personalNumber,
            DateTime birthDate,
            GenderTypeEnum genderTypeId,
            int cityId)
        {
            var instance = Create(firstName, lastName, personalNumber, birthDate, genderTypeId, cityId);

            Validate(instance);

            Update(instance);
        }

        public void Update(Person instance)
        {
            Name = instance.Name;
            BirthDate = instance.BirthDate;
            PersonalNumber = instance.PersonalNumber;
            CityId = instance.CityId;
            GenderTypeId = instance.GenderTypeId;

            DomainEvents.Add(new PersonUpdatedEvent(this));
        }

        public void Delete()
        {
            DomainEvents.Add(new PersonDeletedEvent(this));
        }

        public static void Validate(Person instance)
        {

        }

        #endregion
    }
}
