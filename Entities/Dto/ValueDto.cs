using Entities.Common;
using Entities.Models;

namespace Entities.Dto
{
    public class ValueDto : AutoincrementObject
    {
        public string Description { get; set; }

        public ValueDto(ValueEntity entity) : base(entity)
        {
            if (entity == null)
            {
                return;
            }

            Description = entity.Description;
        }
    }
}
