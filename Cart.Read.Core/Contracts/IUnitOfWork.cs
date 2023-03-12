using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Read.Core.Contracts
{
    public interface IUnitOfWork
    {
        IFoodCartRepository FoodCartRepository { get; }
        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
