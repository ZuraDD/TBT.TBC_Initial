using Domain.Common;
using Domain.Entities;

namespace Domain.Events.PersonEvents
{
    public class PersonUpdatedEvent : DomainEvent
    {
        public PersonUpdatedEvent(Person person)
        {
            Person = person;
        }

        public Person Person { get; }
    }
}
