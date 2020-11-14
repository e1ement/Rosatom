using Entities.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IValueRepository
    {
        Task<IEnumerable<ValueDto>> GetAllAsync(bool trackChanges);
    }
}
