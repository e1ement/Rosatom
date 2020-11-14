using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Entities
{
    public static class RepositorySeedContext
    {
        public static async Task Initialize(RepositoryContext context)
        {
            // Define test Value data
            if (!await context.Values.AnyAsync())
            {
                var values = new List<ValueEntity>
                {
                    new ValueEntity
                    {
                        Description = "Value 1",
                    },
                    new ValueEntity
                    {
                        Description = "Value 2",
                    }
                };

                await context.AddRangeAsync(values);
                await context.SaveChangesAsync();
            }
        }
    }
}
