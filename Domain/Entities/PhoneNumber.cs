using Domain.Common;
using Domain.Enums;
using Domain.Events.PersonEvents;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class PhoneNumber : ComplexEntity
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public PhoneNumberTypeEnum PhoneNumberTypeId { get; set; }

        public virtual PhoneNumberType PhoneNumberType { get; set; }

        public int PersonId { get; set; }

        public virtual Person Person { get; set; }

        #region methods

        public static PhoneNumber Create(string value, PhoneNumberTypeEnum type, int personId)
        {
            var number = PhoneNumberVO.Create(type, value);

            var instance = new PhoneNumber
            {
                Value = number.Value,
                PhoneNumberTypeId = number.Type,
                PersonId = personId
            };

            instance.DomainEvents.Add(new PersonPhoneNumberCreatedEvent(instance));

            return instance;
        }

        public void Update(string value, PhoneNumberTypeEnum type)
        {
            var instance = Create(value, type, PersonId);

            Validate(instance);

            Update(instance);
        }

        public void Update(PhoneNumber instance)
        {
            Value = instance.Value;
            PhoneNumberTypeId = instance.PhoneNumberTypeId;

            DomainEvents.Add(new PersonPhoneNumberUpdatedEvent(this));
        }

        public void Delete()
        {
            DomainEvents.Add(new PersonPhoneNumberDeletedEvent(this));
        }

        public static void Validate(PhoneNumber instance)
        {

        }

        #endregion
    }
}
