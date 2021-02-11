using System;

namespace Domain.Common
{
    public abstract class Auditable
    {
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }
}
