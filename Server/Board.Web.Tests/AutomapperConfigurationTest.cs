using AutoMapper;
using NUnit.Framework;

namespace Board.Web.Tests
{
    [TestFixture]
    public class AutomapperConfigurationTest
    {
        [Test]
        public void MappingProfile_VerifyMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });

            var mapper = new Mapper(config);

            ((IMapper) mapper).ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
