using Domain.Events.Cart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Read.Core.EventHandlers
{
    public class ItemAddedHandler : IRequestHandler<ItemAdded>
    {
        public Task Handle(ItemAdded request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
