﻿using Domain.Common;
using Domain.Entities;

namespace Domain.Events.RelationEvents
{
    public class RelationCreatedEvent : DomainEvent
    {
        public RelationCreatedEvent(Relation relation)
        {
            Relation = relation;
        }

        public Relation Relation { get; }
    }
}
