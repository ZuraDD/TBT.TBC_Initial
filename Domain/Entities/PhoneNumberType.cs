using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;

namespace Domain.Entities
{
    public class PhoneNumberType : SimpleEntity
    {
        public Enums.PhoneNumberTypeEnum Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}
