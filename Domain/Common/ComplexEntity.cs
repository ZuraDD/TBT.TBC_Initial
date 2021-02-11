using System;
using System.Collections.Generic;

namespace Domain.Common
{
    public abstract class ComplexEntity : AuditableEntity, IHasDomainEvent, IEntity
    {
        public List<DomainEvent> DomainEvents { get; set; }

        protected ComplexEntity()
        {
            DomainEvents = new List<DomainEvent>();
        }
    }
}
