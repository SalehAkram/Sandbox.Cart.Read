using Cart.Read.Core.Contracts;
using Cart.Read.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Cart.Read.Core.Entities
{

    public class FoodCart : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid RestaurantId { get; private set; }
        public DateTime DateCreated { get; private set; }
        public CartStatus CartStatus { get; private set; }
        public int EventVersion { get; private set; }
        private readonly List<FoodItem> _items;
        public IReadOnlyCollection<FoodItem> Items => _items;
        public FoodCart() { }
        public FoodCart(Guid id, Guid customerId, Guid restaurantId, DateTime dateCreated, int eventVersion)
        {
            Id = id;
            CustomerId = customerId;
            RestaurantId = restaurantId;
            DateCreated = dateCreated;
            CartStatus = new CartStatus("Created", dateCreated);
            EventVersion = eventVersion;
          
        }
        public void AddItemToCart(Guid itemId, Guid cartId, int quantity, decimal price)
        {
            if (Id != cartId)
                throw new InvalidOperationException(message: "Attempting add an item to the wrong Cart");
            var existingItem = _items.FirstOrDefault(x => x.ItemId == itemId);
            if (existingItem != null)
                throw new InvalidOperationException(message: "Attempting add duplicate item. You can only increase quantity of an existing item");
           
            _items.Add(new FoodItem(itemId, cartId, quantity, price));
        }
    }
}
