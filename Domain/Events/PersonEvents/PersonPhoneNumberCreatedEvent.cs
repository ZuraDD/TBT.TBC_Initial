using Domain.Common;
using Domain.Entities;

namespace Domain.Events.PersonEvents
{
    public class PersonPhoneNumberCreatedEvent : DomainEvent
    {
        public PersonPhoneNumberCreatedEvent(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public PhoneNumber PhoneNumber { get; }
    }
}
