using Cart.Read.Core.Contracts;
using Cart.Read.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Cart.Infra.Repository
{
    public class FoodCartRepository : Repository<FoodCart>, IFoodCartRepository
    {
        public FoodCartRepository(CRDbContext context) : base(context)
        {

        }
        public override void Add(FoodCart entity)
        {
            // We can override repository virtual methods in order to customize repository behavior, Template Method Pattern
            // Code here

            base.Add(entity);
        }
    }
}
