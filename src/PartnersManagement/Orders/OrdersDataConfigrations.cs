using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartnersManagement.Orders.Entities;
using PartnersManagement.Orders.Entities.Partners;

namespace PartnersManagement.Orders
{
    public class OrdersDataConfigrations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order", "dbo");
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.OrderItems).WithOne(x => x.Order);

            builder.Property(x => x.Partner)
                .HasConversion(
                    v => v.ToString(),
                    v => (PartnerType)Enum.Parse(typeof(PartnerType), v));
        }
    }
}