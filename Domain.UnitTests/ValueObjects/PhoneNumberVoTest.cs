using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;
using NUnit.Framework;

namespace Domain.UnitTests.ValueObjects
{
    public class PhoneNumberVoTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldReturnCorrectValue()
        {
            var type = PhoneNumberTypeEnum.Home;
            var value = "599684567";

            var vo = PhoneNumberVO.Create(type, value);

            Assert.AreEqual(vo.Type, type);
            Assert.AreEqual(vo.Value, value);
        }

        [Test]
        public void ShouldBeCastableToString()
        {
            var type = PhoneNumberTypeEnum.Home;
            var value = "599684567";

            var vo = PhoneNumberVO.Create(type, value);

            Assert.AreEqual(vo.ToString(), $"Type - {type}, LastName - {value}");
        }

        [Test]
        public void ShouldBeEqual()
        {
            var type = PhoneNumberTypeEnum.Home;
            var value = "599684567";

            var vo1 = PhoneNumberVO.Create(type, value);

            var vo2 = PhoneNumberVO.Create(type, value);

            Assert.AreEqual(vo1, vo2);
        }

        [Test]
        public void ShouldNotBeEqual_Case1()
        {
            var type1 = PhoneNumberTypeEnum.Home;
            var value1 = "599684567";

            var type2 = PhoneNumberTypeEnum.Home;
            var value2 = "599684569";

            var vo1 = PhoneNumberVO.Create(type1, value1);

            var vo2 = PhoneNumberVO.Create(type2, value2);

            Assert.AreNotEqual(vo1, vo2);
        }

        [Test]
        public void ShouldNotBeEqual_Case2()
        {
            var type1 = PhoneNumberTypeEnum.Home;
            var value1 = "599684567";

            var type2 = PhoneNumberTypeEnum.Mobile;
            var value2 = "599684567";

            var vo1 = PhoneNumberVO.Create(type1, value1);

            var vo2 = PhoneNumberVO.Create(type2, value2);

            Assert.AreNotEqual(vo1, vo2);
        }

        [Test]
        public void ShouldNotBeEqual_Case3()
        {
            var type1 = PhoneNumberTypeEnum.Mobile;
            var value1 = "599684567";

            var type2 = PhoneNumberTypeEnum.Office;
            var value2 = "599684569";

            var vo1 = PhoneNumberVO.Create(type1, value1);

            var vo2 = PhoneNumberVO.Create(type2, value2);

            Assert.AreNotEqual(vo1, vo2);
        }

        [Test]
        public void ShouldThrowExceptionWithEmptyOrDefaultValue_Case1()
        {
            var type = default(PhoneNumberTypeEnum);
            var value = "599684567";

            var ex = Assert.Throws<DomainException>(() => PhoneNumberVO.Create(type, value));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidPhoneNumber));
        }

        [Test]
        public void ShouldThrowExceptionWithEmptyOrDefaultValue_Case2()
        {
            var type = PhoneNumberTypeEnum.Home;
            var value = default(string);

            var ex = Assert.Throws<DomainException>(() => PhoneNumberVO.Create(type, value));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidPhoneNumber));
        }

        [Test]
        public void ShouldThrowExceptionWithInvalidLength_Case1()
        {
            var type = PhoneNumberTypeEnum.Home;
            var value = "599";

            var ex = Assert.Throws<DomainException>(() => PhoneNumberVO.Create(type, value));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidPhoneNumber));
        }

        [Test]
        public void ShouldThrowExceptionWithInvalidLength_Case2()
        {
            var type = PhoneNumberTypeEnum.Home;
            var value = "5996855509024"+ "5996855509024" + "5996855509024" +
                        "5996855509024" + "5996855509024" + "5996855509024" +
                        "5996855509024" + "5996855509024" + "5996855509024" +
                        "5996855509024" + "5996855509024" + "5996855509024" +
                        "5996855509024" + "5996855509024" + "5996855509024";

            var ex = Assert.Throws<DomainException>(() => PhoneNumberVO.Create(type, value));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidPhoneNumber));
        }

        [Test]
        public void ShouldThrowExceptionWithInvalidRegex()
        {
            var type = PhoneNumberTypeEnum.Home;
            var value = "5996G4567";

            var ex = Assert.Throws<DomainException>(() => PhoneNumberVO.Create(type, value));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidPhoneNumber));
        }
    }
}