using Cart.Read.Core.Contracts;
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
        private readonly IUnitOfWork _unitOfWork;
        public ItemAddedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ItemAdded request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.FoodCartRepository.FindBy(c => c.Id == request.CartId, ci => ci.Items);
            if (cart == null)
                throw new InvalidOperationException(message: $"Unable to add items to cart. Cart with that Id {request.CartId} does not exists");
            cart.AddItemToCart(request.Id, request.CartId, request.Quantity, request.Price);
            _unitOfWork.FoodCartRepository.Update(cart);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
