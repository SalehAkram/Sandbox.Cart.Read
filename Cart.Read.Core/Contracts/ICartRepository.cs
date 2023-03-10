using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Read.Core.Contracts
{
    public interface ICartRepository
    {
        Task<Entities.Cart> GetByCartId(Guid id);
        void Insert(Entities.Cart entity);

        void Delete(Entities.Cart entityToDelete);
        void Update(Entities.Cart entityToUpdate);
        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
