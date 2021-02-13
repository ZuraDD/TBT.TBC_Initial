using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;

namespace Domain.Entities
{
    public class RelationType : SimpleEntity
    {
        public Enums.RelationTypeEnum Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Relation> RelatedPersons { get; set; }
    }
}
