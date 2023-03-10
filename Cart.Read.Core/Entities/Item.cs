using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Read.Core.Entities
{
    public class Item
    {

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
       
        public Guid ItemId { get; private set; }
        public Guid CartId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public Item( Guid itemId, Guid cartId, int quantity, decimal price)
        {
            ItemId = itemId;
            CartId = cartId;
            Quantity = quantity;
            Price = price;
        }
    }
}
