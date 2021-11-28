using AutoMapper;

namespace PartnersManagement.UnitTests.Common
{
    public class MappingFixture
    {
        public MappingFixture()
        {
            Mapper = MapperFactory.Create();
        }

        public IMapper Mapper { get; }
    }
}