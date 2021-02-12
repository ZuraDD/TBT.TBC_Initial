using Domain.Common;
using Domain.Enums;
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

            return new PhoneNumber
            {
                Value = number.Value,
                PhoneNumberTypeId = number.Type,
                PersonId = personId
            };
        }

        #endregion
    }
}
