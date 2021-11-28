using AutoMapper;
using PartnersManagement.Orders;

namespace PartnersManagement.UnitTests.Common
{
    public static class MapperFactory
    {
        public static IMapper Create()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrderMappings>();
            });

            return configurationProvider.CreateMapper();
        }
    }
}