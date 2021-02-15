using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;
using NUnit.Framework;

namespace Domain.UnitTests.ValueObjects
{
    public class PersonalNumberVoTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldReturnCorrectValue()
        {
            var value = "01001034456";

            var vo = PersonalNumberVO.Create(value);

            Assert.AreEqual(vo.Value, value);
        }

        [Test]
        public void ShouldBeCastableToString()
        {
            var value = "01001034456";

            var vo = PersonalNumberVO.Create(value);

            Assert.AreEqual(vo.ToString(), value);
        }

        [Test]
        public void ShouldBeCastedFromString()
        {
            var value = "01001034456";

            var vo = (PersonalNumberVO) value;

            Assert.AreEqual(vo.ToString(), value);
        }

        [Test]
        public void ShouldBeEqual()
        {
            var value = "01001034456";

            var vo1 = PersonalNumberVO.Create(value);

            var vo2 = PersonalNumberVO.Create(value);

            Assert.AreEqual(vo1, vo2);
        }

        [Test]
        public void ShouldNotBeEqual()
        {
            var value = "01001034456";

            var value2 = "01001034457";

            var vo1 = PersonalNumberVO.Create(value);

            var vo2 = PersonalNumberVO.Create(value2);

            Assert.AreNotEqual(vo1, vo2);
        }

        [Test]
        public void ShouldThrowExceptionWithEmptyOrDefaultValue()
        {
            var value = default(string);

            var ex = Assert.Throws<DomainException>(() => PersonalNumberVO.Create(value));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidPersonalNumber));
        }

        [Test]
        public void ShouldThrowExceptionWithInvalidLength()
        {
            var invalidLength = "01001094";

            var ex = Assert.Throws<DomainException>(() => PersonalNumberVO.Create(invalidLength));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidPersonalNumber));
        }

        [Test]
        public void ShouldThrowExceptionWithInvalidRegex()
        {
            var invalidRegex = "010010G4783";

            var ex = Assert.Throws<DomainException>(() => PersonalNumberVO.Create(invalidRegex));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidPersonalNumber));
        }
    }
}