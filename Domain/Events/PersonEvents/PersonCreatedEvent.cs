using Domain.Common;
using Domain.Entities;

namespace Domain.Events.PersonEvents
{
    public class PersonCreatedEvent : DomainEvent
    {
        public PersonCreatedEvent(Person person)
        {
            Person = person;
        }

        public Person Person { get; }
    }
}
