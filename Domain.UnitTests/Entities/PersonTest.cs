using System;
using System.Linq;
using Domain.Entities;
using Domain.Enums;
using Domain.Events.PersonEvents;
using Domain.Exceptions;
using NUnit.Framework;

namespace Domain.UnitTests.Entities
{
    public class PersonTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldReturnCorrectValue()
        {
            var firstName = "Zura";
            var lastName = "Jincharadze";
            var personalNumber = "01045679453";
            var birthDate = DateTime.UtcNow.AddYears(-18);
            var genderTypeId = GenderTypeEnum.Female;
            var cityId = 4;

            var entity = Person.Create(firstName, lastName, personalNumber, birthDate, genderTypeId, cityId);

            Assert.AreEqual(entity.Name.FirstName, firstName);
            Assert.AreEqual(entity.Name.LastName, lastName);
            Assert.AreEqual(entity.PersonalNumber.Value, personalNumber);
            Assert.AreEqual(entity.BirthDate.Value, new DateTime(birthDate.Year, birthDate.Month, birthDate.Day));
            Assert.AreEqual(entity.GenderTypeId, genderTypeId);
            Assert.AreEqual(entity.CityId, cityId);
        }

        [Test]
        public void ShouldThrowExceptionWithInvalidCity()
        {
            var firstName = "Zura";
            var lastName = "Jincharadze";
            var personalNumber = "01045679453";
            var birthDate = DateTime.UtcNow.AddYears(-18);
            var genderTypeId = GenderTypeEnum.Female;
            var cityId = default(int);

            var ex = Assert.Throws<DomainException>(() => Person.Create(firstName, lastName, personalNumber, birthDate, genderTypeId, cityId));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidPerson));
        }

        [Test]
        public void ShouldThrowExceptionWithInvalidGenderType()
        {
            var firstName = "Zura";
            var lastName = "Jincharadze";
            var personalNumber = "01045679453";
            var birthDate = DateTime.UtcNow.AddYears(-18);
            var genderTypeId = default(GenderTypeEnum);
            var cityId = 4;

            var ex = Assert.Throws<DomainException>(() => Person.Create(firstName, lastName, personalNumber, birthDate, genderTypeId, cityId));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidPerson));
        }

        [Test]
        public void ShouldGeneratePersonCreatedEvent()
        {
            var firstName = "Zura";
            var lastName = "Jincharadze";
            var personalNumber = "01045679453";
            var birthDate = DateTime.UtcNow.AddYears(-18);
            var genderTypeId = GenderTypeEnum.Female;
            var cityId = 4;

            var entity = Person.Create(firstName, lastName, personalNumber, birthDate, genderTypeId, cityId);

            Assert.IsNotEmpty(entity.DomainEvents);
            Assert.IsTrue(entity.DomainEvents.SingleOrDefault() is PersonCreatedEvent);
            Assert.AreEqual((entity.DomainEvents.SingleOrDefault() as PersonCreatedEvent)?.Person, entity);
        }

        [Test]
        public void ShouldGeneratePersonUpdatedEvent()
        {
            var firstName = "Zura";
            var lastName = "Jincharadze";
            var personalNumber = "01045679453";
            var birthDate = DateTime.UtcNow.AddYears(-18);
            var genderTypeId = GenderTypeEnum.Female;
            var cityId = 4;

            var entity = Person.Create(firstName, lastName, personalNumber, birthDate, genderTypeId, cityId);

            entity.DomainEvents.Clear();

            var firstNameUpdate = "NewName";

            entity.Update(firstNameUpdate, lastName, personalNumber, birthDate, genderTypeId, cityId);

            Assert.AreEqual((entity.DomainEvents.SingleOrDefault() as PersonUpdatedEvent)?.Person, entity);
        }

        [Test]
        public void ShouldGeneratePersonUpdatedEvent_Case2()
        {
            var firstName = "Zura";
            var lastName = "Jincharadze";
            var personalNumber = "01045679453";
            var birthDate = DateTime.UtcNow.AddYears(-18);
            var genderTypeId = GenderTypeEnum.Female;
            var cityId = 4;

            var entity = Person.Create(firstName, lastName, personalNumber, birthDate, genderTypeId, cityId);

            entity.DomainEvents.Clear();

            var firstNameUpdate = "NewName";

            var updatedEntity = Person.Create(firstNameUpdate, lastName, personalNumber, birthDate, genderTypeId, cityId);

            entity.Update(updatedEntity);

            Assert.AreEqual((entity.DomainEvents.SingleOrDefault() as PersonUpdatedEvent)?.Person, entity);
        }

        [Test]
        public void ShouldGeneratePersonDeletedEvent()
        {
            var firstName = "Zura";
            var lastName = "Jincharadze";
            var personalNumber = "01045679453";
            var birthDate = DateTime.UtcNow.AddYears(-18);
            var genderTypeId = GenderTypeEnum.Female;
            var cityId = 4;

            var entity = Person.Create(firstName, lastName, personalNumber, birthDate, genderTypeId, cityId);

            entity.DomainEvents.Clear();

            entity.Delete();

            Assert.AreEqual((entity.DomainEvents.SingleOrDefault() as PersonDeletedEvent)?.Person, entity);
        }
    }
}