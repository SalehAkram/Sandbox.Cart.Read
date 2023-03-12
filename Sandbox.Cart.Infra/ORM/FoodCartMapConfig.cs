using Cart.Read.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Cart.Infra.ORM
{
    public class FoodCartMapConfig : IEntityTypeConfiguration<FoodCart>
    {
        public void Configure(EntityTypeBuilder<FoodCart> builder)
        {
            builder.ToTable("FoodCarts");
            builder.HasKey(fc => fc.Id);
            builder.Property(en => en.Id).ValueGeneratedNever();
            builder.Property(en => en.CustomerId).HasColumnName("CustomerId").IsRequired();
            builder.Property(en => en.RestaurantId).HasColumnName("RestaurantId").IsRequired();
            builder.Property(en => en.DateCreated).HasColumnName("DateCreated").IsRequired();
            builder.Property(en => en.EventVersion).HasColumnName("EventVersion").IsRequired();
            builder.OwnsOne(en => en.CartStatus, cs =>
            {
                cs.Property(x => x.Status).HasColumnName("Status");
                cs.Property(x => x.DateUpdated).HasColumnName("StatusUpdatedDate");
            });
            builder.OwnsMany(f => f.Items, item =>
            {
                item.ToTable("FoodItems");
               
                item.HasKey(fi => fi.Id);
                item.Property(fi => fi.Id).ValueGeneratedOnAdd().HasColumnName("Id");
                item.WithOwner().HasForeignKey("CartId");
                item.Property(fi => fi.ItemId).HasColumnName("ItemId").IsRequired();
                item.Property(fi => fi.Quantity).HasColumnName("Quantity").IsRequired();
                item.Property(fi => fi.Price).HasColumnName("Price").IsRequired();
            });
        }
    }
}
