using System;
using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;
using NUnit.Framework;

namespace Domain.UnitTests.ValueObjects
{
    public class BirthDateVoTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldReturnCorrectValue()
        {
            var value = DateTime.UtcNow.AddYears(-18);

            var vo = BirthDateVO.Create(value);

            Assert.AreEqual(vo.Value, new DateTime(value.Year, value.Month, value.Day));
        }

        [Test]
        public void ShouldBeCastableToString()
        {
            var value = DateTime.UtcNow.AddYears(-18);

            var vo = BirthDateVO.Create(value);

            Assert.AreEqual(vo.ToString(), $"{value:MM/dd/yyyy}");
        }

        [Test]
        public void ShouldBeCastedFromString()
        {
            var value = DateTime.UtcNow.AddYears(-18);

            var vo = (BirthDateVO) value;

            Assert.AreEqual(vo.ToString(), $"{value:MM/dd/yyyy}");
        }

        [Test]
        public void ShouldBeEqual()
        {
            var value = DateTime.UtcNow.AddYears(-18);

            var vo1 = BirthDateVO.Create(value);

            var vo2 = BirthDateVO.Create(value);

            Assert.AreEqual(vo1, vo2);
        }

        [Test]
        public void ShouldNotBeEqual()
        {
            var value = DateTime.UtcNow.AddYears(-18);

            var value2 = DateTime.UtcNow.AddYears(-19);

            var vo1 = BirthDateVO.Create(value);

            var vo2 = BirthDateVO.Create(value2);

            Assert.AreNotEqual(vo1, vo2);
        }

        [Test]
        public void ShouldThrowExceptionWithEmptyOrDefaultValue()
        {
            var value = default(DateTime);

            var ex = Assert.Throws<DomainException>(() => BirthDateVO.Create(value));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidBirthDate));
        }

        [Test]
        public void ShouldThrowExceptionWithInvalidAgeSpecification()
        {
            var value = DateTime.UtcNow.AddYears(-17);

            var ex = Assert.Throws<DomainException>(() => BirthDateVO.Create(value));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidBirthDate));
        }
    }
}