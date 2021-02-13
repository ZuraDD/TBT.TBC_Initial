using Domain.Common;
using Domain.Entities;

namespace Domain.Events.RelationEvents
{
    public class RelationDeletedEvent : DomainEvent
    {
        public RelationDeletedEvent(Relation relation)
        {
            Relation = relation;
        }

        public Relation Relation { get; }
    }
}
