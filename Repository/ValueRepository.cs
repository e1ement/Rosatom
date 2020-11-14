using Contracts;
using Entities;
using Entities.Dto;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class ValueRepository : RepositoryBase<ValueEntity>, IValueRepository
    {
        public ValueRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<ValueDto>> GetAllAsync(bool trackChanges)
        {
            var entities = await FindAll(trackChanges)
                .OrderBy(o => o.Id)
                .ToListAsync();

            return entities.Select(s => new ValueDto(s));
        }
    }
}
