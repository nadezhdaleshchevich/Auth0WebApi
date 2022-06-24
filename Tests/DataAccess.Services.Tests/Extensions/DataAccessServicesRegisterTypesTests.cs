using System;
using DataAccess.Services.Extensions;
using Moq;
using NUnit.Framework;
using AutoMapper;
using DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Services.Tests.Extensions
{
    [TestFixture]
    public class DataAccessServicesRegisterTypesTests
    {
        private IServiceCollection _services;

        [SetUp]
        public void SetUp()
        {
            var mockedUserContext = new Mock<IUserContext>();
            var mockedCompanyContext = new Mock<ICompanyContext>();

            _services = new ServiceCollection();

            _services.AddSingleton<IUserContext>(mockedUserContext.Object);
            _services.AddSingleton<ICompanyContext>(mockedCompanyContext.Object);
        }

        [Test]
        public void LoadDataAccessServicesTypes_When_ServiceCollectionIsNul_Throw_()
        {
            IServiceCollection services = null;

            Action action = () => services.LoadDataAccessServicesTypes();

            Assert.Throws<ArgumentNullException>(() => action());
        }

        [Test]
        public void LoadDataAccessServicesTypes_NumberOfServices()
        {
            var services = new ServiceCollection();

            services.LoadDataAccessServicesTypes();

            Assert.AreEqual(10, services.Count);
        }

        [Test]
        public void LoadDataAccessServicesTypes_AllTypesAreLoaded()
        {
            _services.LoadDataAccessServicesTypes();

            var provider = _services.BuildServiceProvider();

            var service1 = provider.GetService<IUserContext>();
            var service2 = provider.GetService<ICompanyContext>();
            var service3 = provider.GetService<IMapper>();

            Assert.NotNull(service1);
            Assert.NotNull(service2);
            Assert.NotNull(service3);
        }
    }
}
