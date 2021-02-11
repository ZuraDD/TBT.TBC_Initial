using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public interface IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }
}
