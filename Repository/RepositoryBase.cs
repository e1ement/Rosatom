using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RepositoryContext _repositoryContext;

        protected RepositoryBase(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return !trackChanges
                ? _repositoryContext.Set<T>().AsNoTracking()
                : _repositoryContext.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return !trackChanges
                ? _repositoryContext.Set<T>().Where(expression).AsNoTracking()
                : _repositoryContext.Set<T>().Where(expression);
        }

        public async Task Create(T entity)
        {
            GenerateGuidId(entity);
            await _repositoryContext.Set<T>().AddAsync(entity);
        }

        public async Task CreateCollection(IEnumerable<T> entities)
        {
            var list = entities.ToList();
            foreach (var entity in list)
            {
                GenerateGuidId(entity);
            }

            await _repositoryContext.Set<T>().AddRangeAsync(list);
        }

        public void Update(T entity)
        {
            _repositoryContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _repositoryContext.Set<T>().Remove(entity);
        }

        private static void GenerateGuidId(T entity)
        {
            if (entity is UniqueIdEntity guidBasedEntity
                && guidBasedEntity.Id == Guid.Empty)
            {
                guidBasedEntity.Id = Guid.NewGuid();
            }
        }

        public async Task<int> SaveChanges()
        {
            return await _repositoryContext.SaveChangesAsync();
        }
    }
}
