using Domain.Common;
using Domain.Entities;

namespace Domain.Events.PersonEvents
{
    public class PersonDeletedEvent : DomainEvent
    {
        public PersonDeletedEvent(Person person)
        {
            Person = person;
        }

        public Person Person { get; }
    }
}
