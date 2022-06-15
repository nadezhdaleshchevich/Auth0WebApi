using System;
using AutoMapper;
using DataAccess.Interfaces;
using DataAccess.Services.Implementation;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using DataAccess.Services.Models;

namespace DataAccess.Services.Tests.Implementation
{
    [TestFixture]
    public class CompanyServiceTests
    {
        private MockRepository _mockRepository;

        private Mock<ICompanyContext> _mockCompanyContext;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _mockCompanyContext = _mockRepository.Create<ICompanyContext>();
            _mockMapper = _mockRepository.Create<IMapper>();
        }

        private void SetupMock()
        {

        }

        private CompanyService CreateService()
        {
            return new CompanyService(
                _mockCompanyContext.Object,
                _mockMapper.Object);
        }

        [TestCase(null, null)]
        public void Ctor_(ICompanyContext context, IMapper mapper)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new CompanyService(context, mapper));

            Assert.AreEqual(typeof(ArgumentNullException), exception.GetType());
        }
    }
}
