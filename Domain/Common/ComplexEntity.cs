using System.Collections.Generic;

namespace Domain.Common
{
    public abstract class ComplexEntity : Auditable, IEntity, IHasDomainEvent 
    {
        public HashSet<DomainEvent> DomainEvents { get; set; }

        protected ComplexEntity()
        {
            DomainEvents = new HashSet<DomainEvent>();
        }
    }
}
