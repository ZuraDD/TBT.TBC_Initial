using Domain.Common;
using Domain.Entities;

namespace Domain.Events.PersonEvents
{
    public class PersonPhoneNumberUpdatedEvent : DomainEvent
    {
        public PersonPhoneNumberUpdatedEvent(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public PhoneNumber PhoneNumber { get; }
    }
}
