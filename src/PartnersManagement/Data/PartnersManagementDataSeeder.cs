using System.Threading.Tasks;
using BuildingBlocks.Persistence;

namespace PartnersManagement.Data
{
    public class PartnersManagementDataSeeder : IDataSeeder
    {
        private readonly PartnerManagementDbContext _context;

        public PartnersManagementDataSeeder(PartnerManagementDbContext context)
        {
            _context = context;
            _context = context;
        }

        public async Task SeedAllAsync()
        {
            // await SeedOrders();
            // await _context.SaveChangesAsync(default);
        }
        private async Task SeedOrders()
        {

        }
    }
}