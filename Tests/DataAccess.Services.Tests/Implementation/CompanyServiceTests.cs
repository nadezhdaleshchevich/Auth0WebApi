using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using DataAccess.Services.Exceptions;
using DataAccess.Services.Implementation;
using DataAccess.Services.MapProfiles;
using DataAccess.Services.Models;
using DataAccess.Services.Tests.Extensions;
using Moq;
using NUnit.Framework;
using DbCompany = DataAccess.Models.Company;

namespace DataAccess.Services.Tests.Implementation
{
    [TestFixture]
    public class CompanyServiceTests
    {
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(config =>
            {
                config.AddProfile(new DataAccessServicesMapProfile());
            });

            _mapper = config.CreateMapper();
        }

        [Test]
        public void Ctor_When_ContextIsNull_Throw_ArgumentNullException()
        {
            var action = () => new CompanyService(null, _mapper);

            Assert.Throws<ArgumentNullException>(() => action());
        }

        [Test]
        public void Ctor_When_MapperIsNull_Throw_ArgumentNullException()
        {
            var mockedContext = new Mock<ICompanyContext>();

            var action = () => new CompanyService(mockedContext.Object, null);

            Assert.Throws<ArgumentNullException>(() => action());
        }

        [Test]
        public async Task GetCompanies_When_ContextReturnsThreeCompanies_Returns_ThreeCompanies()
        {
            var mockedContext = new Mock<ICompanyContext>();
            mockedContext.Setup(m => m.Companies).ReturnsEntitySet(new List<DbCompany>()
            {
                new DbCompany(),
                new DbCompany(),
                new DbCompany()
            });
            var companyService = new CompanyService(mockedContext.Object, _mapper);

            var companies = await companyService.GetCompaniesAsync();

            Assert.AreEqual(companies.Count(), 3);
            mockedContext.Verify(m => m.Companies, Times.Once);
        }

        [Test]
        public async Task CreateCompanyAsync_When_CompanyIsNull_Throw_ArgumentNullException()
        {
            var mockedContext = new Mock<ICompanyContext>();
            var companyService = new CompanyService(mockedContext.Object, _mapper);

            var action  = async () => await companyService.CreateCompanyAsync(null);

            Assert.ThrowsAsync<ArgumentNullException>(() => action());
        }

        [Test]
        public async Task CreateCompanyAsync_When_CompanyIsValid_Then_CompanyIsAdded()
        {
            var mockedContext = new Mock<ICompanyContext>();
            var mockedEntitySet = new Mock<IEntitySet<DbCompany>>();
            mockedContext.Setup(m => m.Companies).Returns(mockedEntitySet.Object);
            var companyService = new CompanyService(mockedContext.Object, _mapper);
            var companyDto = new UpdateCompanyDto
            {
                Name = "Company 1"
            };

            var company = await companyService.CreateCompanyAsync(companyDto);

            Assert.NotNull(company);
            Assert.AreEqual(companyDto.Name, company.Name);
            mockedContext.Verify(m => m.Companies.Add(It.IsAny<DbCompany>()), Times.Once);
            mockedContext.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task UpdateCompanyAsync_When_CompanyIsNull_Throw_ArgumentNullException()
        {
            var mockedContext = new Mock<ICompanyContext>();
            var companyService = new CompanyService(mockedContext.Object, _mapper);

            var action = async () => await companyService.UpdateCompanyAsync(0, null);

            Assert.ThrowsAsync<ArgumentNullException>(() => action());
            mockedContext.Verify(m => m.SaveChangesAsync(), Times.Never);
        }

        [Test]
        public async Task UpdateCompanyAsync_When_CompanyExists_CompanyIsUpdated()
        {
            var mockedContext = new Mock<ICompanyContext>();
            mockedContext.Setup(m => m.Companies).ReturnsEntitySet(new List<DbCompany>()
            {
                new DbCompany
                {
                    Id = 1,
                    Name = "Old company name"
                }
            });
            var companyService = new CompanyService(mockedContext.Object, _mapper);
            var companyDto = new UpdateCompanyDto
            {
                Name = "New company name"
            };

            var company = await companyService.UpdateCompanyAsync(1, companyDto);

            Assert.NotNull(company);
            Assert.AreEqual(companyDto.Name, company.Name);
            mockedContext.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task UpdateCompanyAsync_When_CompanyDoesNotExist_Throw_RequestedResourceNotFoundException()
        {
            var mockedContext = new Mock<ICompanyContext>();
            mockedContext.Setup(m => m.Companies).ReturnsEntitySet(new List<DbCompany>());
            var companyService = new CompanyService(mockedContext.Object, _mapper);
            var companyDto = new UpdateCompanyDto
            {
                Name = "New company name"
            };

            var action = async () => await companyService.UpdateCompanyAsync(1, companyDto);

            Assert.ThrowsAsync<RequestedResourceNotFoundException>(() => action());
            mockedContext.Verify(m => m.SaveChangesAsync(), Times.Never);
        }

        [Test]
        public async Task DeleteCompanyAsync_When_CompanyDoesNotExist_Throw_RequestedResourceNotFoundException()
        {
            var mockedContext = new Mock<ICompanyContext>();
            mockedContext.Setup(m => m.Companies).ReturnsEntitySet(new List<DbCompany>());
            var companyService = new CompanyService(mockedContext.Object, _mapper);

            var action = async () => await companyService.DeleteCompanyAsync(1);

            Assert.ThrowsAsync<RequestedResourceNotFoundException>(() => action());
            mockedContext.Verify(m => m.Companies.Remove(It.IsNotNull<DbCompany>()), Times.Never);
            mockedContext.Verify(m => m.SaveChangesAsync(), Times.Never);
        }

        [Test]
        public async Task DeleteCompanyAsync_When_CompanyExists_CompanyIsDeleted()
        {
            var mockedContext = new Mock<ICompanyContext>();
            var mockedEntitySet = new Mock<IEntitySet<DbCompany>>();
            mockedContext.SetupSequence(m => m.Companies)
                .ReturnsEntitySet(new List<DbCompany>()
                {
                    new DbCompany
                    {
                        Id = 1
                    }
                })
                .Returns(mockedEntitySet.Object);

            var companyService = new CompanyService(mockedContext.Object, _mapper);

            await companyService.DeleteCompanyAsync(1);

            mockedContext.Verify(m => m.Companies.Remove(It.IsNotNull<DbCompany>()), Times.Once);
            mockedContext.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task FindCompanyByIdCompanyAsync_When_CompanyDoesNotExist_Throw_RequestedResourceNotFoundException()
        {
            var mockedContext = new Mock<ICompanyContext>();
            mockedContext.Setup(m => m.Companies).ReturnsEntitySet(new List<DbCompany>());
            var companyService = new CompanyService(mockedContext.Object, _mapper);

            var action = async () => await companyService.FindCompanyByIdCompanyAsync(1);

            Assert.ThrowsAsync<RequestedResourceNotFoundException>(() => action());
        }

        [Test]
        public async Task FindCompanyByIdCompanyAsync_When_CompanyExists_Return_Company()
        {
            var dbCompany = new DbCompany
            {
                Id = 1,
                Name = "Company Name"
            };
            var mockedContext = new Mock<ICompanyContext>();
            mockedContext.Setup(m => m.Companies).ReturnsEntitySet(new List<DbCompany>()
            {
                dbCompany
            });
            var companyService = new CompanyService(mockedContext.Object, _mapper);

            var companyDto = await companyService.FindCompanyByIdCompanyAsync(1);

            Assert.NotNull(companyDto);
            Assert.AreEqual(dbCompany.Id, companyDto.Id);
            Assert.AreEqual(dbCompany.Name, companyDto.Name);
        }
    }
}