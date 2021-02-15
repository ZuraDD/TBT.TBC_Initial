using System.Linq;
using Domain.Entities;
using Domain.Enums;
using Domain.Events.PersonEvents;
using Domain.Exceptions;
using NUnit.Framework;

namespace Domain.UnitTests.Entities
{
    public class PhoneNumberTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldReturnCorrectValue()
        {
            var phoneNumberValue = "599668877";

            var phoneNumberType = PhoneNumberTypeEnum.Home;

            var personId = 2;

            var entity = PhoneNumber.Create(phoneNumberValue, phoneNumberType, personId);

            Assert.AreEqual(entity.Value, phoneNumberValue);
            Assert.AreEqual(entity.PhoneNumberTypeId, phoneNumberType);
            Assert.AreEqual(entity.PersonId, personId);
        }

        [Test]
        public void ShouldGeneratePersonPhoneNumberCreatedEvent()
        {
            var phoneNumberValue = "599668877";

            var phoneNumberType = PhoneNumberTypeEnum.Home;

            var personId = 2;

            var entity = PhoneNumber.Create(phoneNumberValue, phoneNumberType, personId);
            
            Assert.AreEqual((entity.DomainEvents.SingleOrDefault() as PersonPhoneNumberCreatedEvent)?.PhoneNumber, entity);
        }

        [Test]
        public void ShouldGeneratePersonPhoneNumberUpdatedEvent_Case1()
        {
            var phoneNumberValue = "599668877";

            var phoneNumberType = PhoneNumberTypeEnum.Home;

            var personId = 2;

            var entity = PhoneNumber.Create(phoneNumberValue, phoneNumberType, personId);

            entity.DomainEvents.Clear();

            var phoneNumberTypeUpdate = PhoneNumberTypeEnum.Mobile;

            entity.Update(phoneNumberValue, phoneNumberTypeUpdate);

            Assert.AreEqual((entity.DomainEvents.SingleOrDefault() as PersonPhoneNumberUpdatedEvent)?.PhoneNumber, entity);
        }

        [Test]
        public void ShouldGeneratePersonPhoneNumberUpdatedEvent_Case2()
        {
            var phoneNumberValue = "599668877";

            var phoneNumberType = PhoneNumberTypeEnum.Home;

            var personId = 2;

            var entity = PhoneNumber.Create(phoneNumberValue, phoneNumberType, personId);

            entity.DomainEvents.Clear();

            var phoneNumberTypeUpdate = PhoneNumberTypeEnum.Mobile;

            var updatedEntity = PhoneNumber.Create(phoneNumberValue, phoneNumberTypeUpdate, personId);

            entity.Update(updatedEntity);

            Assert.AreEqual((entity.DomainEvents.SingleOrDefault() as PersonPhoneNumberUpdatedEvent)?.PhoneNumber, entity);
        }

        [Test]
        public void ShouldGeneratePersonPhoneNumberDeletedEvent()
        {
            var phoneNumberValue = "599668877";

            var phoneNumberType = PhoneNumberTypeEnum.Home;

            var personId = 2;

            var entity = PhoneNumber.Create(phoneNumberValue, phoneNumberType, personId);

            entity.DomainEvents.Clear();

            entity.Delete();

            Assert.AreEqual((entity.DomainEvents.SingleOrDefault() as PersonPhoneNumberDeletedEvent)?.PhoneNumber, entity);
        }
    }
}