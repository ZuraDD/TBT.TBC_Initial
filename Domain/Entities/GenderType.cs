using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;

namespace Domain.Entities
{
    public class GenderType : SimpleEntity
    {
        public Enums.GenderTypeEnum Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
}
}
