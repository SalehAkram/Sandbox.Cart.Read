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
        public FoodCart(Guid id, Guid customerId, Guid restaurantId, DateTime dateCreated, string eventVersion)
        {
            Id = id;
            CustomerId = customerId;
            RestaurantId = restaurantId;
            DateCreated = dateCreated;
            CartStatus = new CartStatus("Created", dateCreated);
            if (int.TryParse(eventVersion, out int version))
                EventVersion = version;
            else
                throw new InvalidOperationException(message: "Event version could not be ascertained");
        }
        public void AddItemToCart(Guid itemId, Guid cartId, int quantity, decimal price)
        {
            _items.Add(new FoodItem(itemId, cartId, quantity, price));
        }
    }
}
