using System;
using System.Collections.Generic;

namespace Domain.Common
{
    public abstract class ComplexEntity : Auditable, IEntity, IHasDomainEvent 
    {
        public List<DomainEvent> DomainEvents { get; set; }

        protected ComplexEntity()
        {
            DomainEvents = new List<DomainEvent>();
        }
    }
}
