using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Works")]
    public class WorkEntity : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string JobName { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public DateTime? FactStartDate { get; set; }
        public DateTime? NewPlannedStartDate { get; set; }
        public decimal IncDayCost { get; set; }
        public decimal DecDayCost { get; set; }
        public int MinimalDuration { get; set; }
        public int NormDuration { get; set; }
        public decimal MinimalDurationCost { get; set; }
        public decimal AddedCost { get; set; }
        public decimal AddedChildrenCost { get; set; }
        
        public List<WorkEntity> ChildWorks { get; set; }
        public List<WorkEntity> ParentWorks { get; set; }
    }
}
