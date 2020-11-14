using System.Collections.Generic;
using Entities.Dto;
using Entities.Models;

namespace Contracts
{
    public interface IWorkRepository
    {
        void CalculateAddedCost(WorkDto work);
        decimal CalculateChildWorksCost(WorkDto work);
        IEnumerable<WorkDto> GenerateData();
    }
}
