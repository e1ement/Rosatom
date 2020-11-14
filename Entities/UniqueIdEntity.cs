using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public abstract class UniqueIdEntity : BaseEntity,
        IUniqueIdObject
    {
        [Key]
        public Guid Id { get; set; }

        public UniqueIdEntity()
        {
        }

        public UniqueIdEntity(IUniqueIdObject entity)
        {
            if (entity == null)
            {
                return;
            }

            Id = entity.Id;
        }
    }
}
