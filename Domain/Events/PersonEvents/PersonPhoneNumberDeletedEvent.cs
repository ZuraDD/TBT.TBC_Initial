using Domain.Common;
using Domain.Entities;

namespace Domain.Events.PersonEvents
{
    public class PersonPhoneNumberDeletedEvent : DomainEvent
    {
        public PersonPhoneNumberDeletedEvent(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public PhoneNumber PhoneNumber { get; }
    }
}
