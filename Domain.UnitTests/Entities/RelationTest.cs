using System.Linq;
using Domain.Entities;
using Domain.Enums;
using Domain.Events.PersonEvents;
using Domain.Events.RelationEvents;
using Domain.Exceptions;
using Domain.ValueObjects;
using NUnit.Framework;

namespace Domain.UnitTests.Entities
{
    public class RelationTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldReturnCorrectValue()
        {
            var relationTypeEnum = RelationTypeEnum.Colleague;

            var personForId = 3;

            var personToId = 5;

            var entity = Relation.Create(relationTypeEnum, personForId, personToId);

            Assert.AreEqual(entity.RelationTypeId, relationTypeEnum);
            Assert.AreEqual(entity.PersonForId, personForId);
            Assert.AreEqual(entity.PersonToId, personToId);
        }


        [Test]
        public void ShouldThrowExceptionWithInvalidRelationType()
        {
            var relationTypeEnum = default(RelationTypeEnum);

            var personForId = 3;

            var personToId = 5;

            var ex = Assert.Throws<DomainException>(() => Relation.Create(relationTypeEnum, personForId, personToId));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidRelation));
        }

        [Test]
        public void ShouldThrowExceptionWithInvalidPersonForId()
        {
            var relationTypeEnum = default(RelationTypeEnum);

            var personForId = default(int);

            var personToId = 5;

            var ex = Assert.Throws<DomainException>(() => Relation.Create(relationTypeEnum, personForId, personToId));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidRelation));
        }

        [Test]
        public void ShouldThrowExceptionWithInvalidPersonTo()
        {
            var relationTypeEnum = default(RelationTypeEnum);

            var personForId = 3;

            var personToId = default(int);

            var ex = Assert.Throws<DomainException>(() => Relation.Create(relationTypeEnum, personForId, personToId));

            Assert.That(ex.Code, Is.EqualTo(DomainExceptionCode.InvalidRelation));
        }

        [Test]
        public void ShouldGenerateRelationCreatedEvent()
        {
            var relationTypeEnum = RelationTypeEnum.Colleague;

            var personForId = 3;

            var personToId = 5;

            var entity = Relation.Create(relationTypeEnum, personForId, personToId);

            Assert.AreEqual((entity.DomainEvents.SingleOrDefault() as RelationCreatedEvent)?.Relation, entity);
        }

        [Test]
        public void ShouldGenerateRelationDeletedEvent()
        {
            var relationTypeEnum = RelationTypeEnum.Colleague;

            var personForId = 3;

            var personToId = 5;

            var entity = Relation.Create(relationTypeEnum, personForId, personToId);

            entity.DomainEvents.Clear();

            entity.Delete();

            Assert.AreEqual((entity.DomainEvents.SingleOrDefault() as RelationDeletedEvent)?.Relation, entity);
        }
    }
}