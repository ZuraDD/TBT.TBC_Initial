using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;
using NUnit.Framework;

namespace Domain.UnitTests.ValueObjects
{
    public class PersonNameVOTest
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

            var vo = PersonNameVO.Create(firstName, lastName);

            Assert.AreEqual(vo.FirstName, firstName);

            Assert.AreEqual(vo.LastName, lastName);
        }

        [Test]
        public void ShouldBeCastableToString()
        {
            var firstName = "Zura";

            var lastName = "Jincharadze";

            var vo = PersonNameVO.Create(firstName, lastName);

            Assert.AreEqual(vo.ToString(), $"FirstName - {firstName}, LastName - {lastName}");
        }

        [Test]
        public void ShouldBeEqual()
        {
            var firstName = "Zura";

            var lastName = "Jincharadze";

            var vo1 = PersonNameVO.Create(firstName, lastName);

            var vo2 = PersonNameVO.Create(firstName, lastName);

            Assert.AreEqual(vo1, vo2);
        }

        [Test]
        public void ShouldNotBeEqual()
        {
            var firstName1 = "Zura";

            var lastName1 = "Jincharadze";

            var vo1 = PersonNameVO.Create(firstName1, lastName1);

            var firstName2 = "Giorgi";

            var lastName2 = "Jincharadze";

            var vo2 = PersonNameVO.Create(firstName2, lastName2);

            Assert.AreNotEqual(vo1, vo2);
        }

        [Test]
        public void ShouldThrowExceptionWithEmptyOrDefaultValue_Case1()
        {
            var firstName = default(string);

            var lastName = "Jincharadze";

            var ex = Assert.Throws<DomainException>(() => PersonNameVO.Create(firstName, lastName));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidPersonName));
        }

        [Test]
        public void ShouldThrowExceptionWithEmptyOrDefaultValue_Case2()
        {
            var firstName = "Zura";

            var lastName = default(string);

            var ex = Assert.Throws<DomainException>(() => PersonNameVO.Create(firstName, lastName));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidPersonName));
        }

        [Test]
        public void ShouldThrowExceptionWithInvalidLength_Case1()
        {
            var firstName = "Z";

            var lastName = "Jincharadze";

            var ex = Assert.Throws<DomainException>(() => PersonNameVO.Create(firstName, lastName));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidPersonName));
        }

        [Test]
        public void ShouldThrowExceptionWithInvalidLength_Case2()
        {
            var firstName = "Zura";

            var lastName = "J";

            var ex = Assert.Throws<DomainException>(() => PersonNameVO.Create(firstName, lastName));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidPersonName));
        }

        [Test]
        public void ShouldThrowExceptionWithInvalidRegex_Case1()
        {
            var firstName = "Zнз";

            var lastName = "Jincharadze";

            var ex = Assert.Throws<DomainException>(() => PersonNameVO.Create(firstName, lastName));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidPersonName));
        }

        [Test]
        public void ShouldThrowExceptionWithInvalidRegex_Case2()
        {
            var firstName = "Zura";

            var lastName = "днз";

            var ex = Assert.Throws<DomainException>(() => PersonNameVO.Create(firstName, lastName));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidPersonName));
        }
    }
}