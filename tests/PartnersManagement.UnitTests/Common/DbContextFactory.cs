using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PartnersManagement.Data;
using PartnersManagement.Orders;
using PartnersManagement.Orders.Entities;
using PartnersManagement.Orders.Entities.Partners;

namespace PartnersManagement.UnitTests.Common
{
    public static class DbContextFactory
    {
        public static PartnerManagementDbContext Create()
        {
            var options = new DbContextOptionsBuilder<PartnerManagementDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var context = new PartnerManagementDbContext(options);

            // Seed our data
            SeedOrders(context);

            return context;
        }

        private static void SeedOrders(PartnerManagementDbContext context)
        {
            var order = new PartnerAOrder()
            {
                Partner = PartnerType.PartnerA,
                CompanyId = "123",
                CompanyName = "test",
                OrderItems = new List<OrderItem>
                {
                    new WebSiteProductOrderItem
                    {
                        ProductId = "12",
                        WebsiteDetails = new WebsiteDetails()
                    }
                }
            };
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public static async Task Destroy(PartnerManagementDbContext context)
        {
            await context.Database.EnsureDeletedAsync();
            await context.DisposeAsync();
        }
    }
}