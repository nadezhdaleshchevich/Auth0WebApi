using DataAccess.Services.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DataAccess.Services.Exceptions;
using DataAccess.Services.Models;
using Moq;
using Web.Services.Companies.Implementation;

namespace Web.Services.Tests.Companies.Implementation
{
    [TestFixture]
    public class GetCompanyServiceTests
    {
        [Test]
        public void Ctor_When_CompanyServiceIsNull_Throw_ArgumentNullException()
        {
            Action action = () => new GetCompanyService(null);

            Assert.Throws<ArgumentNullException>(() => action());
        }

        [Test]
        public async Task GetAllCompaniesAsync_When_CompanyServiceIsCalled_Return_OkAndObject()
        {
            var companies = new List<CompanyDto>();
            var companyService = new Mock<ICompanyService>();
            companyService
                .Setup(m => m.GetCompaniesAsync())
                .ReturnsAsync(companies);
            var webService = new GetCompanyService(companyService.Object);

            var actionResult = await webService.GetAllCompaniesAsync();

            Assert.NotNull(actionResult);
            Assert.True(actionResult.IsSuccess);
            Assert.AreEqual(HttpStatusCode.OK, actionResult.StatusCode);
            Assert.NotNull(actionResult.Object);
            Assert.AreSame(companies, actionResult.Object);
            Assert.Null(actionResult.ErrorMessage);
            companyService.Verify(m => m.GetCompaniesAsync(), Times.Once);
        }

        [Test]
        public async Task GetAllCompaniesAsync_When_CompanyServiceThrowsException_Return_BadRequestAndErrorMessage()
        {
            var errorMessage = "ErrorMessage";
            var companyService = new Mock<ICompanyService>();
            companyService
                .Setup(m => m.GetCompaniesAsync())
                .ThrowsAsync(new Exception(errorMessage));
            var webService = new GetCompanyService(companyService.Object);

            var actionResult = await webService.GetAllCompaniesAsync();

            Assert.NotNull(actionResult);
            Assert.False(actionResult.IsSuccess);
            Assert.AreEqual(HttpStatusCode.BadRequest, actionResult.StatusCode);
            Assert.Null(actionResult.Object);
            Assert.NotNull(actionResult.ErrorMessage);
            Assert.AreEqual(errorMessage, actionResult.ErrorMessage);
            companyService.Verify(m => m.GetCompaniesAsync(), Times.Once);
        }

        [Test]
        public async Task GetCompanyByIdAsync_When_CompanyServiceIsCalled_Return_OkAndObject()
        {
            var company = new CompanyDto();
            var companyService = new Mock<ICompanyService>();
            companyService
                .Setup(m => m.FindCompanyByIdCompanyAsync(It.IsAny<int>()))
                .ReturnsAsync(company);
            var webService = new GetCompanyService(companyService.Object);

            var actionResult = await webService.GetCompanyByIdAsync(1);

            Assert.NotNull(actionResult);
            Assert.True(actionResult.IsSuccess);
            Assert.AreEqual(HttpStatusCode.OK, actionResult.StatusCode);
            Assert.NotNull(actionResult.Object);
            Assert.AreSame(company, actionResult.Object);
            Assert.Null(actionResult.ErrorMessage);
            companyService.Verify(m => m.FindCompanyByIdCompanyAsync(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task GetCompanyByIdAsync_When_CompanyServiceThrowsRequestedResourceNotFoundException_Return_NotFound()
        {
            var companyService = new Mock<ICompanyService>();
            companyService
                .Setup(m => m.FindCompanyByIdCompanyAsync(It.IsAny<int>()))
                .ThrowsAsync(new RequestedResourceNotFoundException());
            var webService = new GetCompanyService(companyService.Object);

            var actionResult = await webService.GetCompanyByIdAsync(1);

            Assert.NotNull(actionResult);
            Assert.False(actionResult.IsSuccess);
            Assert.AreEqual(HttpStatusCode.NotFound, actionResult.StatusCode);
            Assert.Null(actionResult.Object);
            Assert.NotNull(actionResult.ErrorMessage);
            companyService.Verify(m => m.FindCompanyByIdCompanyAsync(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task GetCompanyByIdAsync_When_CompanyServiceThrowsException_Return_BadRequest()
        {
            var errorMessage = "Error Message";
            var companyService = new Mock<ICompanyService>();
            companyService
                .Setup(m => m.FindCompanyByIdCompanyAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception(errorMessage));
            var webService = new GetCompanyService(companyService.Object);

            var actionResult = await webService.GetCompanyByIdAsync(1);

            Assert.NotNull(actionResult);
            Assert.False(actionResult.IsSuccess);
            Assert.AreEqual(HttpStatusCode.BadRequest, actionResult.StatusCode);
            Assert.Null(actionResult.Object);
            Assert.NotNull(actionResult.ErrorMessage);
            Assert.AreEqual(errorMessage, actionResult.ErrorMessage);
            companyService.Verify(m => m.FindCompanyByIdCompanyAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
