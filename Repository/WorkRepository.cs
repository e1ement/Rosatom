using System;
using System.Collections.Generic;
using Contracts;
using Entities;
using Entities.Dto;
using Entities.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public IEnumerable<WorkEntity> GenerateData()
        {
            var result = new List<WorkEntity>();
            var j = 1;
            var parentIdsList = new List<Guid>();

            var mainWorkElement = new WorkEntity
            {
                Id = Guid.NewGuid(),
                JobName = $"Работа {j}",
                PlannedStartDate = DateTime.Now,
                DecDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                IncDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                NormDuration = new Random().Next(6, 20),
                MinimalDuration = new Random().Next(1, 5),
                MinimalDurationCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                NextWorks = new List<WorkEntity>(),
                PrevWorks = new List<WorkEntity>()
            };

            result.Add(mainWorkElement);
            parentIdsList.Add(mainWorkElement.Id);

            for (var i = 0; i < j; i++)
            {
                var newElement = new WorkEntity
                {
                    Id = Guid.NewGuid(),
                    JobName = $"Работа {i}{j}",
                    PlannedStartDate = DateTime.Now,
                    DecDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    IncDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    NormDuration = new Random().Next(6, 20),
                    MinimalDuration = new Random().Next(1, 5),
                    MinimalDurationCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    NextWorks = new List<WorkEntity>(),
                    PrevWorks = new List<WorkEntity>()
                };

                result.Add(newElement);
                parentIdsList.Add(newElement.Id);
                newElement.NextWorks.Add(result.FirstOrDefault(r => r.Id == mainWorkElement.Id));
            }

            j *= 45;

            for (var i = 0; i < j; i++)
            {
                var newElement = new WorkEntity
                {
                    Id = Guid.NewGuid(),
                    JobName = $"Работа {i}{j}",
                    PlannedStartDate = DateTime.Now,
                    DecDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    IncDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    NormDuration = new Random().Next(6, 20),
                    MinimalDuration = new Random().Next(1, 5),
                    MinimalDurationCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    NextWorks = new List<WorkEntity>(),
                    PrevWorks = new List<WorkEntity>()
                };

                for (var k = 0; k < 4; k++)
                {
                    var tmp = new Random().Next(1, 100);
                    if (tmp <= 40)
                    {
                        continue;
                    }

                    var id = parentIdsList[new Random().Next(0, parentIdsList.Count - 1)];
                    if (newElement.NextWorks.Any(n => n.Id == id))
                    {
                        continue;
                    }
                    newElement.NextWorks.Add(result.FirstOrDefault(r => r.Id == id));
                }

                result.Add(newElement);
                parentIdsList.Add(newElement.Id);
            }

            j *= 45;

            for (var i = 0; i < j; i++)
            {
                var newElement = new WorkEntity
                {
                    Id = Guid.NewGuid(),
                    JobName = $"Работа {i}{j}",
                    PlannedStartDate = DateTime.Now,
                    DecDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    IncDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    NormDuration = new Random().Next(6, 20),
                    MinimalDuration = new Random().Next(1, 5),
                    MinimalDurationCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    NextWorks = new List<WorkEntity>(),
                    PrevWorks = new List<WorkEntity>()
                };

                for (var k = 0; k < 4; k++)
                {
                    var tmp = new Random().Next(1, 100);
                    if (tmp <= 40)
                    {
                        continue;
                    }

                    var id = parentIdsList[new Random().Next(0, parentIdsList.Count - 1)];
                    if (newElement.NextWorks.Any(n => n.Id == id))
                    {
                        continue;
                    }
                    newElement.NextWorks.Add(result.FirstOrDefault(r => r.Id == id));
                }

                result.Add(newElement);
                parentIdsList.Add(newElement.Id);
            }

            j *= 45;

            for (var i = 0; i < j; i++)
            {
                var newElement = new WorkEntity
                {
                    Id = Guid.NewGuid(),
                    JobName = $"Работа {i}{j}",
                    PlannedStartDate = DateTime.Now,
                    DecDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    IncDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    NormDuration = new Random().Next(6, 20),
                    MinimalDuration = new Random().Next(1, 5),
                    MinimalDurationCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
                    NextWorks = new List<WorkEntity>(),
                    PrevWorks = new List<WorkEntity>()
                };

                for (var k = 0; k < 4; k++)
                {
                    var tmp = new Random().Next(1, 100);
                    if (tmp <= 40)
                    {
                        continue;
                    }

                    var id = parentIdsList[new Random().Next(0, parentIdsList.Count - 1)];
                    if (newElement.NextWorks.Any(n => n.Id == id))
                    {
                        continue;
                    }
                    newElement.NextWorks.Add(result.FirstOrDefault(r => r.Id == id));
                }

                result.Add(newElement);
                parentIdsList.Add(newElement.Id);
            }

            result.Where(x => (x.NextWorks == null || !x.NextWorks.Any()) && x.Id != mainWorkElement.Id)
                .ToList()
                .ForEach(item =>
                {
                    item.NextWorks.Add(result.FirstOrDefault(r => r.Id == mainWorkElement.Id));
                });

            //SetDates(result);

            return result;
        }

        public async Task<IEnumerable<WorkDto>> GetByIdAsync(Guid? id, bool trackChanges)
        {
            try
            {
                if (!id.HasValue)
                {
                    var mainEntity = await FindByCondition(e => !e.PrevWorks.Select(p => p.Id).Any(), trackChanges)
                        .FirstOrDefaultAsync();
                    if (mainEntity != null)
                        id = mainEntity.Id;
                }

                var entity = await FindByCondition(e => e.Id == id, trackChanges)
                    .Include(e => e.PrevWorks)
                    .FirstOrDefaultAsync();

                return entity.PrevWorks.Select(workEntity => new WorkDto(workEntity)).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task CreateCollectionAsync(IEnumerable<WorkEntity> works)
        {
            try
            {
                await CreateCollection(works);
                await SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void SetDates(IEnumerable<WorkEntity> works)
        {
            var mainWork = works.FirstOrDefault(w => w.NextWorks == null || !w.NextWorks.Any());

            if (mainWork != null)
            {
                var prevWorks = works.Where(w => w.NextWorks.Select(s => s.Id).ToList().Contains(mainWork.Id)).ToList();
                var date = mainWork.PlannedStartDate;
                SetPlannedDate(prevWorks, date, works);
            }
        }

        private static void SetPlannedDate(List<WorkEntity> prevWorks, DateTime date, IEnumerable<WorkEntity> works)
        {
            if (prevWorks == null || !prevWorks.Any())
            {
                return;
            }

            foreach (var work in prevWorks)
            {
                work.PlannedStartDate = date.AddDays(-1).AddDays(work.NormDuration);
            }

            var minDate = prevWorks.Min(p => p.PlannedStartDate);

            foreach (var work in prevWorks)
            {
                var tmp = works.Where(w => w.NextWorks.Select(s => s.Id).ToList().Contains(work.Id)).ToList();
                SetPlannedDate(tmp, minDate, works);
            }
        }
    }
}
