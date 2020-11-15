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
                    if (tmp <= 10)
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

            j *= 5;

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
                    if (tmp <= 10)
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

            //j *= 45;

            //for (var i = 0; i < j; i++)
            //{
            //    var newElement = new WorkEntity
            //    {
            //        Id = Guid.NewGuid(),
            //        JobName = $"Работа {i}{j}",
            //        PlannedStartDate = DateTime.Now,
            //        DecDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
            //        IncDayCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
            //        NormDuration = new Random().Next(6, 20),
            //        MinimalDuration = new Random().Next(1, 5),
            //        MinimalDurationCost = decimal.Parse($"{new Random().NextDouble() * 100:0.##}"),
            //        NextWorks = new List<WorkEntity>(),
            //        PrevWorks = new List<WorkEntity>()
            //    };

            //    for (var k = 0; k < 4; k++)
            //    {
            //        var tmp = new Random().Next(1, 100);
            //        if (tmp <= 2)
            //        {
            //            continue;
            //        }

            //        var id = parentIdsList[new Random().Next(0, parentIdsList.Count - 1)];
            //        if (newElement.NextWorks.Any(n => n.Id == id))
            //        {
            //            continue;
            //        }
            //        newElement.NextWorks.Add(result.FirstOrDefault(r => r.Id == id));
            //    }

            //    result.Add(newElement);
            //    parentIdsList.Add(newElement.Id);
            //}

            result.Where(x => (x.NextWorks == null || !x.NextWorks.Any()) && x.Id != mainWorkElement.Id)
                .ToList()
                .ForEach(item =>
                {
                    item.NextWorks.Add(result.FirstOrDefault(r => r.Id == mainWorkElement.Id));
                });

            SetDates(result);

            return result;
        }

        public async Task<IEnumerable<WorkDto>> GetByIdAsync(Guid? id, bool trackChanges)
        {
            try
            {
                if (!id.HasValue)
                {
                    var mainEntity = await FindByCondition(e => !e.NextWorks.Select(p => p.Id).Any(), trackChanges)
                        .FirstOrDefaultAsync();
                    if (mainEntity != null)
                        id = mainEntity.Id;
                    else return new List<WorkDto>();
                }

                var entity = await FindByCondition(e => e.Id == id, trackChanges)
                    .Include(e => e.PrevWorks)
                    .ThenInclude(ti => ti.PrevWorks)
                    .FirstOrDefaultAsync();
                if (entity == null) return  new List<WorkDto>();

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

        public async Task Recalculate()
        {
            var entities = await FindAll(true)
                .ToListAsync();

            foreach (var e in entities.Where(e => e.PlannedStartDate < e.NewPlannedStartDate))
            {
                if (e.NewPlannedStartDate != null)
                {
                    var daysDifference = e.NewPlannedStartDate.Value.Subtract(e.PlannedStartDate).Days;

                    e.AddedCost = daysDifference > 0
                        ? e.IncDayCost * daysDifference
                        : e.DecDayCost * daysDifference;
                }
            }

            await SaveChanges();
        }

        public async Task<int> UpdateAsync(WorkForUpdateDto workForUpdate)
        {
            var entity = await FindByCondition(x => x.Id == workForUpdate.Id, true)
                .Include(i => i.NextWorks)
                .Include(i => i.PrevWorks)
                .FirstOrDefaultAsync();
            if (entity.FactStartDate.HasValue && workForUpdate.FactStartDate.HasValue &&
                entity.FactStartDate.Value != workForUpdate.FactStartDate)
            {
                return 0;
            }

            var parentWorks = entity.PrevWorks;
            if (parentWorks != null && parentWorks.Any())
            {
                var possibleDateList =
                    entity.PrevWorks.Select(s => s.PlannedStartDate.AddDays(s.NormDuration)).ToList();
                var maxPossibleDate = possibleDateList.Min(i => i);

                if (workForUpdate.NewPlannedStartDate.HasValue && workForUpdate.NewPlannedStartDate < maxPossibleDate)
                {
                    return 0;
                }
            }

            entity.NewPlannedStartDate = workForUpdate.NewPlannedStartDate;
            if (workForUpdate.FactStartDate.HasValue)
                entity.FactStartDate = workForUpdate.FactStartDate;
            await SaveChanges();

            if (workForUpdate.NewPlannedStartDate != null)
                foreach (var p in entity.NextWorks)
                {
                    await GetNext(p.Id, workForUpdate.NewPlannedStartDate.Value.AddDays(entity.NormDuration));
                }

            return 1;
        }

        private async Task GetNext(Guid id, DateTime prevEndDate)
        {
            var entity = await FindByCondition(e => e.Id == id, true)
                .Include(e => e.NextWorks)
                .FirstOrDefaultAsync();

            if (prevEndDate < entity.PlannedStartDate)
            {
                entity.NewPlannedStartDate = prevEndDate.AddDays(1);
                await SaveChanges();

                foreach (var p in entity.NextWorks)
                {
                    await GetNext(p.Id, entity.NewPlannedStartDate.Value.AddDays(entity.NormDuration));
                }
            }
        }

        private static void SetDates(List<WorkEntity> works)
        {
            if (works == null || !works.Any())
            {
                return;
            }

            var mainWork = works.FirstOrDefault(w => w.NextWorks == null || !w.NextWorks.Any());
            if (mainWork == null)
            {
                return;
            }

            var prevWorkList = works.Where(w => w.NextWorks.Select(s => s.Id).ToList().Contains(mainWork.Id))
                .ToList();
            var date = mainWork.PlannedStartDate;
            SetPlannedDate(prevWorkList, date, works);
        }

        private static void SetPlannedDate(List<WorkEntity> prevWorks, DateTime date, IEnumerable<WorkEntity> works)
        {
            if (prevWorks == null || !prevWorks.Any())
            {
                return;
            }

            foreach (var work in prevWorks)
            {
                work.PlannedStartDate = date.AddDays(-1).AddDays(-1 * work.NormDuration);
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