using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace ProjectGuard.Ef.Entities
{
    public abstract class BaseEntity : Entity, IHasCreationTime, IHasModificationTime
    {
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }

        public BaseEntity()
        {
            CreationTime = DateTime.UtcNow;
        }
    }
}
