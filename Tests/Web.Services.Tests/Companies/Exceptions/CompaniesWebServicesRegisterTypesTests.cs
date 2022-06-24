using NUnit.Framework;
using System;
using DataAccess.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Web.Services.Companies.Exceptions;
using Web.Services.Companies.Interfaces;

namespace Web.Services.Tests.Companies.Exceptions
{
    [TestFixture]
    public class CompaniesWebServicesRegisterTypesTests
    {
        private IServiceCollection _services;

        [SetUp]
        public void SetUp()
        {
            var mockedCompanyService = new Mock<ICompanyService>();

            _services = new ServiceCollection();

            _services.AddSingleton<ICompanyService>(mockedCompanyService.Object);
        }

        [Test]
        public void LoadCompaniesWebServicesTypes_When_ServiceCollectionIsNul_Throw_ArgumentNullException()
        {
            IServiceCollection services = null;

            Action action = () => services.LoadCompaniesWebServicesTypes();

            Assert.Throws<ArgumentNullException>(() => action());
        }

        [Test]
        public void LoadCompaniesWebServicesTypes_NumberOfServices()
        {
            var services = new ServiceCollection();

            services.LoadCompaniesWebServicesTypes();

            Assert.AreEqual(4, services.Count);
        }

        [Test]
        public void LoadCompaniesWebServicesTypes_AllTypesAreLoaded()
        {
            _services.LoadCompaniesWebServicesTypes();

            var provider = _services.BuildServiceProvider();

            var service1 = provider.GetService<IGetCompanyService>();
            var service2 = provider.GetService<ICreateCompanyService>();
            var service3 = provider.GetService<IUpdateCompanyService>();
            var service4 = provider.GetService<IDeleteCompanyService>();

            Assert.NotNull(service1);
            Assert.NotNull(service2);
            Assert.NotNull(service3);
            Assert.NotNull(service4);
        }
    }
}
