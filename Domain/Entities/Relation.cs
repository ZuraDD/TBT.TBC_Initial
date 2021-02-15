using Domain.Common;
using Domain.Enums;
using Domain.Events.RelationEvents;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Relation : ComplexEntity
    {
        public int Id { get; set; }

        public RelationTypeEnum RelationTypeId { get; set; }

        public virtual RelationType RelationType { get; set; }

        public int PersonForId { get; set; }

        public virtual Person PersonFor { get; set; }

        public int PersonToId { get; set; }

        public virtual Person PersonTo { get; set; }

        #region methods

        public static Relation Create(RelationTypeEnum relationTypeId, int personForId, int personToId)
        {
            var instance = new Relation
            {
                RelationTypeId = relationTypeId,
                PersonForId = personForId,
                PersonToId = personToId
            };

            Validate(instance);

            instance.DomainEvents.Add(new RelationCreatedEvent(instance));

            return instance;
        }

        public void Delete()
        {
            DomainEvents.Add(new RelationDeletedEvent(this));
        }

        public static void Validate(Relation instance)
        {
            if(
                instance.RelationTypeId == default(RelationTypeEnum) ||
                instance.PersonForId <= 0 ||
                instance.PersonToId <= 0
            )
                throw new DomainException(DomainExceptionCode.InvalidRelation);
        }

        #endregion
    }
}
