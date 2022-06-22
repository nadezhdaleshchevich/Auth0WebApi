using AutoMapper;
using DataAccess.Services.MapProfiles;
using NUnit.Framework;

namespace DataAccess.Services.Tests.MapProfiles
{
    [TestFixture]
    public class DataAccessServicesMapProfileTests
    {
        [Test]
        public void CreateMapper_DataAccessServicesMapProfile_MapperIsValid()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DataAccessServicesMapProfile());
            });

            var mapper = config.CreateMapper();

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
