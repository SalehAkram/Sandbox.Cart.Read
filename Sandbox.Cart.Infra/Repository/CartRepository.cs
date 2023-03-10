using Cart.Read.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Cart.Infra.Repository
{
    public class CartRepository : ICartRepository
    {
        public void Delete(global::Cart.Read.Core.Entities.Cart entityToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<global::Cart.Read.Core.Entities.Cart> GetByCartId(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(global::Cart.Read.Core.Entities.Cart entity)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(global::Cart.Read.Core.Entities.Cart entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
