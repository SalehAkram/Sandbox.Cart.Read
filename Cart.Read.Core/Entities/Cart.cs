using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implementation-entity-framework-core
namespace Cart.Read.Core.Entities
{
    public enum CartStatus { Created, Abandoned, Paid, PaymentRejected };
    public class Cart
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid RestaurantId { get; private set; }
        public DateTime Created { get; private set; }
        public CartStatus Status { get; private set; }
        public int EventVersion { get; private set; }
        private readonly List<Item> _items;
        public IReadOnlyCollection<Item> Items => _items;
        public Cart() { }
        public Cart(Guid id, Guid customerId, Guid restaurantId, DateTime created, int eventVersion) 
        { 
            Id = id;
            CustomerId = customerId;
            RestaurantId = restaurantId;
            Created = created;
            Status = CartStatus.Created;
            EventVersion = eventVersion;
        }
        public void AddItemToCart(Guid itemId, Guid cartId, int quantity, decimal price)
        {
            _items.Add(new Item(itemId, cartId, quantity, price));
        }
    }
}
