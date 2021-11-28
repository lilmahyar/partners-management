using System;
using AutoMapper;
using PartnersManagement.Data;

namespace PartnersManagement.UnitTests.Common
{
    public class UnitTestFixture : IDisposable
    {
        public UnitTestFixture()
        {
            Mapper = MapperFactory.Create();
            DbContext = DbContextFactory.Create();
        }

        public IMapper Mapper { get; }
        public PartnerManagementDbContext DbContext { get; }

        public void Dispose()
        {
            DbContextFactory.Destroy(DbContext).GetAwaiter().GetResult();
        }
    }
}