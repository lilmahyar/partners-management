using BuildingBlocks.EFCore;
using Microsoft.EntityFrameworkCore;
using PartnersManagement.Data;

namespace Chat.Infrastructure.IdentityData
{
    public class ApplicationDbContextFactory : DesignTimeDbContextFactoryBase<PartnerManagementDbContext>
    {
        protected override PartnerManagementDbContext CreateNewInstance(DbContextOptions<PartnerManagementDbContext> options)
        {
            return new PartnerManagementDbContext(options);
        }
    }
}