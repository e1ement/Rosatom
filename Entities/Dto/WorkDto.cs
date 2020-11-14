using Entities.Common;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Dto
{
    public class WorkDto : UniqueIdObject
    {
        public string JobName { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public DateTime? NewPlannedStartDate { get; set; }
        public DateTime? FactStartDate { get; set; }
        public decimal IncDayCost { get; set; }
        public decimal DecDayCost { get; set; }
        public int NormDuration { get; set; }
        public int MinimalDuration { get; set; }
        public decimal MinimalDurationCost { get; set; }
        public decimal AddedCost { get; set; }
        public decimal AddedChildrenCost { get; set; }

        public List<WorkDto> NextWorks { get; set; }
        public List<WorkDto> PrevWorks { get; set; }

        public decimal SumAddedCost => AddedChildrenCost + AddedCost;

        public WorkDto()
        {
            NextWorks = new List<WorkDto>();
            PrevWorks = new List<WorkDto>();
        }

        public WorkDto(WorkEntity entity) : base(entity)
        {
            if (entity == null)
            {
                return;
            }

            JobName = entity.JobName;
            PlannedStartDate = entity.PlannedStartDate;
            FactStartDate = entity.FactStartDate;
            IncDayCost = entity.IncDayCost;
            DecDayCost = entity.DecDayCost;
            NormDuration = entity.NormDuration;
            MinimalDuration = entity.MinimalDuration;
            MinimalDurationCost = entity.MinimalDurationCost;
            AddedCost = entity.AddedCost;
            AddedChildrenCost = entity.AddedChildrenCost;
            NewPlannedStartDate = entity.NewPlannedStartDate;

            if (entity.NextWorks != null 
                && entity.NextWorks.Any())
            {
                entity.NextWorks.ForEach(e => NextWorks.Add(new WorkDto(e)));
            }

            if ( entity.PrevWorks != null 
                 && entity.PrevWorks.Any())
            {
                entity.PrevWorks.ForEach(e => PrevWorks.Add(new WorkDto(e)));
            }
        }

        public WorkEntity ToEntity()
        {
            return new WorkEntity
            {
                Id = Id,
                PlannedStartDate = PlannedStartDate,
                FactStartDate = FactStartDate,
                IncDayCost = IncDayCost,
                DecDayCost = DecDayCost,
                NormDuration = NormDuration,
                MinimalDuration = MinimalDuration,
                MinimalDurationCost = MinimalDurationCost,
                AddedCost = AddedCost,
                AddedChildrenCost = AddedChildrenCost,
                NewPlannedStartDate = NewPlannedStartDate
            };
        }

        public void UpdateEntity(WorkEntity entity)
        {
            entity.NewPlannedStartDate = NewPlannedStartDate;
            entity.AddedCost = AddedCost;
            AddedChildrenCost = AddedChildrenCost;
        }
    }
}
