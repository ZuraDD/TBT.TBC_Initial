using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class RelatedPerson : ComplexEntity
    {
        public int Id { get; set; }

        public RelationTypeEnum RelationTypeId { get; set; }

        public virtual RelationType RelationType { get; set; }

        public int PersonForId { get; set; }

        public virtual Person PersonFor { get; set; }

        public int PersonToId { get; set; }

        public virtual Person PersonTo { get; set; }

        #region methods

        public static RelatedPerson Create(RelationTypeEnum relationTypeId, int personForId, int personToId)
        {
            var instance = new RelatedPerson
            {
                RelationTypeId = relationTypeId,
                PersonForId = personForId,
                PersonToId = personToId
            };

            Validate(instance);

            return instance;
        }

        public static void Validate(RelatedPerson instance)
        {
            if(
                instance.RelationTypeId == default
            )
                throw new DomainException(DomainExceptionCode.InvalidRelatedPerson);

        }

        #endregion
    }
}
