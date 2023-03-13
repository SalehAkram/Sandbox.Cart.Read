using Cart.Read.Core.Contracts;
using Cart.Read.Core.Entities;
using Domain.Events.Cart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Read.Core.EventHandlers
{
    public class CartCreatedHandler : IRequestHandler<CartCreated>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartCreatedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CartCreated request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.FoodCartRepository.GetByIdAsync(request.Id);
            if (cart != null)
                throw new InvalidOperationException(message: "Unable to create the cart. Cart with that Id already exists");
            cart = new FoodCart(request.Id, request.CustomerId, request.RestaurantId, DateTime.UtcNow, request.Version);
             _unitOfWork.FoodCartRepository.Add(cart);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
