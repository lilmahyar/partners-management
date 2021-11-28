using System.Threading.Tasks;
using BuildingBlocks.Persistence;
using PartnersManagement.Data;

namespace PartnersManagement.IntegrationTests
{
    public class IntegrationTestDataSeeder : IDataSeeder
    {
        private readonly PartnerManagementDbContext _dbContext;

        public IntegrationTestDataSeeder(PartnerManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SeedAllAsync()
        {
            // await _dbContext.Orders.AddAsync(OrderMocks.PartnerA_Order);
            // await _dbContext.SaveChangesAsync();
        }
    }
}