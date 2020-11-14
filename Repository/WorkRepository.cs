using System;
using System.Collections.Generic;
using Contracts;
using Entities;
using Entities.Dto;
using Entities.Models;
using System.Linq;

namespace Repository
{
    public class WorkRepository : RepositoryBase<WorkEntity>, IWorkRepository
    {
        public WorkRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CalculateAddedCost(WorkDto work)
        {
            if (!work.NewPlannedStartDate.HasValue)
            {
                work.AddedCost = 0;
                return;
            }

            var daysDifference = work.NewPlannedStartDate.Value.Subtract(work.PlannedStartDate).Days;

            work.AddedCost = daysDifference > 0 
                ? work.IncDayCost * daysDifference 
                : work.DecDayCost * daysDifference;
        }

        public decimal CalculateChildWorksCost(WorkDto work)
        {
            decimal result = 0;

            if (work.PrevWorks == null 
                || !work.PrevWorks.Any())
            {
                return result;
            }

            foreach (var cWork in work.PrevWorks)
            {
                result += cWork.AddedCost;
                if (cWork.PrevWorks != null 
                    && cWork.PrevWorks.Any())
                {
                    result += CalculateChildWorksCost(cWork);
                }
            }

            return result;
        }

        public IEnumerable<WorkDto> GenerateData()
        {
            var result = new List<WorkEntity>();
            var j = 1;
            var parentIdsList = new List<Guid>();

            var mainWorkElement = new WorkEntity
            {
                Id = new Guid(),
                PlannedStartDate = DateTime.Now,
                DecDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                IncDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                NormDuration = new Random().Next(6, 20),
                MinimalDuration = new Random().Next(1, 5),
                MinimalDurationCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}")
            };

            result.Add(mainWorkElement);
            parentIdsList.Add(mainWorkElement.Id);

            for (var i = 0; i < j; i++)
            {
                var newElement = new WorkEntity
                {
                    Id = new Guid(),
                    PlannedStartDate = DateTime.Now,
                    DecDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    IncDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    NormDuration = new Random().Next(6, 20),
                    MinimalDuration = new Random().Next(1, 5),
                    MinimalDurationCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}")
                };

                result.Add(newElement);
                parentIdsList.Add(newElement.Id);
                newElement.NextWorks.Add(new WorkWorkEntity(){NextWorkId = mainWorkElement.Id, PrevWorkId = newElement.Id});
            }

            j *= 45;

            for (var i = 0; i < j; i++)
            {
                var newElement = new WorkEntity
                {
                    Id = new Guid(),
                    PlannedStartDate = DateTime.Now,
                    DecDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    IncDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    NormDuration = new Random().Next(6, 20),
                    MinimalDuration = new Random().Next(1, 5),
                    MinimalDurationCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}")
                };

                for (var k = 0; k < 4; k++)
                {
                    var tmp = new Random().Next(1, 100);
                    if (tmp <= 40)
                    {
                        continue;
                    }

                    var id = parentIdsList[new Random().Next(0, parentIdsList.Count - 1)];
                    newElement.NextWorks.Add(new WorkWorkEntity() { NextWorkId = id, PrevWorkId = newElement.Id });
                }

                result.Add(newElement);
                parentIdsList.Add(newElement.Id);
            }

            j *= 45;

            for (var i = 0; i < j; i++)
            {
                var newElement = new WorkEntity
                {
                    Id = new Guid(),
                    PlannedStartDate = DateTime.Now,
                    DecDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    IncDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    NormDuration = new Random().Next(6, 20),
                    MinimalDuration = new Random().Next(1, 5),
                    MinimalDurationCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}")
                };

                for (var k = 0; k < 4; k++)
                {
                    var tmp = new Random().Next(1, 100);
                    if (tmp <= 40)
                    {
                        continue;
                    }

                    var id = parentIdsList[new Random().Next(0, parentIdsList.Count - 1)];
                    newElement.NextWorks.Add(new WorkWorkEntity() { NextWorkId = id, PrevWorkId = newElement.Id });
                }

                result.Add(newElement);
                parentIdsList.Add(newElement.Id);
            }

            j *= 45;

            for (var i = 0; i < j; i++)
            {
                var newElement = new WorkEntity
                {
                    Id = new Guid(),
                    PlannedStartDate = DateTime.Now,
                    DecDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    IncDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    NormDuration = new Random().Next(6, 20),
                    MinimalDuration = new Random().Next(1, 5),
                    MinimalDurationCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}")
                };

                for (var k = 0; k < 4; k++)
                {
                    var tmp = new Random().Next(1, 100);
                    if (tmp <= 40)
                    {
                        continue;
                    }

                    var id = parentIdsList[new Random().Next(0, parentIdsList.Count - 1)];
                    newElement.NextWorks.Add(new WorkWorkEntity() { NextWorkId = id, PrevWorkId = newElement.Id });
                }

                result.Add(newElement);
                parentIdsList.Add(newElement.Id);
            }

            result.Where(x => x.NextWorks == null || !x.NextWorks.Any())
                .ToList()
                .ForEach(item =>
                {
                    item.NextWorks.Add(new WorkWorkEntity() { NextWorkId = mainWorkElement.Id, PrevWorkId = item.Id });
                });

            SetDates(result);
            return result.Select(s => new WorkDto(s));
        }

        private static void SetDates(IEnumerable<WorkEntity> works)
        {
            var mainWork = works.FirstOrDefault(w => w.NextWorks == null 
                                                     || !w.NextWorks.Any());
            if (mainWork == null)
            {
                return;
            }

            var prevWorks = mainWork.PrevWorks;
            SetPlannedDate(prevWorks.Select(e => e.PrevWork).ToList(), mainWork.PlannedStartDate);
        }

        private static void SetPlannedDate(List<WorkEntity> prevWorks, DateTime date)
        {
            foreach (var work in prevWorks)
            {
                work.PlannedStartDate = date.AddDays(-1).AddDays(work.NormDuration);
            }

            var minDate = prevWorks.Min(p => p.PlannedStartDate);

            foreach (var work in prevWorks.Where(work => work.PrevWorks != null && work.PrevWorks.Any()))
            {
                SetPlannedDate(work.PrevWorks.Select(e => e.PrevWork).ToList(), minDate);
            }
        }
    }
}
