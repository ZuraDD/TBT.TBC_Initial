using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public interface IHasDomainEvent
    {
        public HashSet<DomainEvent> DomainEvents { get; set; }
    }
}
