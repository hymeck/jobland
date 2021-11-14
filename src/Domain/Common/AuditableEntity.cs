using System;

namespace Domain.Common
{
    public class AuditableEntity : Entity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
