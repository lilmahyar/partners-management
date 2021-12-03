using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PartnersManagement.Orders.Entities;
using PartnersManagement.Orders.Entities.Partners;

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

            // var s = Orders.AsQueryable().OfType<PaidSearchProductOrderItem>();
            // var s = _dbContext.Orders.AsQueryable().OfType<PartnerAOrder>();

            builder.Entity<PartnerAOrder>();
            builder.Entity<PartnerBOrder>();
            builder.Entity<PartnerCOrder>();
            builder.Entity<PartnerDOrder>();

            //https://stackoverflow.com/questions/37398141/ef7-migrations-the-corresponding-clr-type-for-entity-type-is-not-instantiab
            builder.Entity<PaidSearchProductOrderItem>().OwnsOne(x => x.AdWordCampaign);
            builder.Entity<WebSiteProductOrderItem>().OwnsOne(x => x.WebsiteDetails);

            base.OnModelCreating(builder);
        }
    }
}