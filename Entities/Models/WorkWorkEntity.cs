using System;

namespace Entities.Models
{
    public class WorkWorkEntity
    {
        public Guid PrevWorkId { get; set; }
        public Guid NextWorkId { get; set; }

        public WorkEntity NextWork { get; set; }
        public WorkEntity PrevWork { get; set; }
    }
}
