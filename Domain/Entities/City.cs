using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class City : SimpleEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
    }
}
