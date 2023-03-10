using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Read.Core.Contracts
{
    public interface IUnitOfWork
    {
        ICartRepository CartRepository { get; }
        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
