using System;

namespace Entities.Dto
{
    public class WorkForUpdateDto
    {
        public Guid Id { get; set; }
        public DateTime? NewPlannedStartDate { get; set; }
        public DateTime? FactStartDate { get; set; }
    }
}
