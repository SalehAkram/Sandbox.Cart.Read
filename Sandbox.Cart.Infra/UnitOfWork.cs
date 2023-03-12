using Cart.Read.Core.Contracts;
using Sandbox.Cart.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Cart.Infra
{
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly CRDbContext _context;

        public IFoodCartRepository FoodCartRepository { get; private set; }
        public UnitOfWork(CRDbContext context)
        {
            _context = context;
            FoodCartRepository = new FoodCartRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// No matter an exception has been raised or not, this method always will dispose the DbContext 
        /// </summary>
        /// <returns></returns>
        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}
