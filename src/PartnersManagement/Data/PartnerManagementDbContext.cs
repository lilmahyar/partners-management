using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PartnersManagement.Orders.Entities;

namespace PartnersManagement.Data
{
    public class PartnerManagementDbContext : DbContext
    {
        public PartnerManagementDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.ConfigureOrdersDataModel();

            //https://stackoverflow.com/questions/37398141/ef7-migrations-the-corresponding-clr-type-for-entity-type-is-not-instantiab
            builder.Entity<PaidSearchProductOrderItem>().OwnsOne(x => x.AdWordCampaign);
            builder.Entity<WebSiteProductOrderItem>().OwnsOne(x => x.WebsiteDetails);

            base.OnModelCreating(builder);
        }
    }
}