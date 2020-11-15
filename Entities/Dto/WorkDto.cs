using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Dto
{
    public class WorkDto
    {
        public Guid Id { get; set; }
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
        public bool HasChildren { get; set; }

        public List<WorkDto> NextWorks { get; set; } = new List<WorkDto>();
        public List<WorkDto> PrevWorks { get; set; } = new List<WorkDto>();

        public decimal SumAddedCost => AddedChildrenCost + AddedCost;

        public WorkDto(WorkEntity entity)
        {
            if (entity == null)
            {
                return;
            }

            Id = entity.Id;
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
            HasChildren = entity.PrevWorks.Any();
        }

        public WorkDto(WorkEntity entity, List<WorkEntity> nextWorks, List<WorkEntity> prevWorks)
        {
            if (entity == null)
            {
                return;
            }

            Id = entity.Id;
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
            HasChildren = entity.PrevWorks.Any();

            if (nextWorks != null)
            {
                NextWorks.AddRange(nextWorks.Select(s => new WorkDto(s)));
            }

            if (prevWorks != null)
            {
                PrevWorks.AddRange(prevWorks.Select(s => new WorkDto(s)));
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