using DataAccess.Services.Interfaces;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;
using DataAccess.Services.Exceptions;
using DataAccess.Services.Models;
using Moq;
using Web.Services.Companies.Implementation;

namespace Web.Services.Tests.Companies.Implementation
{
    [TestFixture]
    public class UpdateCompanyServiceTests
    {
        [Test]
        public void Ctor_When_CompanyServiceIsNull_Throw_ArgumentNullException()
        {
            Action action = () => new UpdateCompanyService(null);

            Assert.Throws<ArgumentNullException>(() => action());
        }

        [Test]
        public async Task UpdateCompanyAsync_When_CompanyServiceIsCalled_Return_OkAndObject()
        {
            var companyDto = new CompanyDto();
            var companyService = new Mock<ICompanyService>();
            companyService
                .Setup(m => m.UpdateCompanyAsync(It.IsAny<int>(), It.IsAny<UpdateCompanyDto>()))
                .ReturnsAsync(companyDto);
            var webService = new UpdateCompanyService(companyService.Object);
            var updateCompanyDto = new UpdateCompanyDto();

            var actionResult = await webService.UpdateCompanyAsync(1, updateCompanyDto);

            Assert.NotNull(actionResult);
            Assert.True(actionResult.IsSuccess);
            Assert.AreEqual(HttpStatusCode.OK, actionResult.StatusCode);
            Assert.NotNull(actionResult.Object);
            Assert.AreSame(companyDto, actionResult.Object);
            Assert.Null(actionResult.ErrorMessage);
            companyService
                .Verify(m => m.UpdateCompanyAsync(It.IsAny<int>(), It.IsAny<UpdateCompanyDto>()), Times.Once);
        }

        [Test]
        public async Task UpdateCompanyAsync_When_CompanyServiceThrowsRequestedResourceNotFoundException_Return_NotFound()
        {
            var companyService = new Mock<ICompanyService>();
            companyService
                .Setup(m => m.UpdateCompanyAsync(It.IsAny<int>(), It.IsAny<UpdateCompanyDto>()))
                .ThrowsAsync(new RequestedResourceNotFoundException());
            var webService = new UpdateCompanyService(companyService.Object);
            var dto = new UpdateCompanyDto();

            var actionResult = await webService.UpdateCompanyAsync(1, dto);

            Assert.NotNull(actionResult);
            Assert.False(actionResult.IsSuccess);
            Assert.AreEqual(HttpStatusCode.NotFound, actionResult.StatusCode);
            Assert.Null(actionResult.Object);
            Assert.NotNull(actionResult.ErrorMessage);
            companyService
                .Verify(m => m.UpdateCompanyAsync(It.IsAny<int>(), It.IsAny<UpdateCompanyDto>()), Times.Once);
        }

        [Test]
        public async Task UpdateCompanyAsync_When_CompanyServiceThrowsException_Return_BadRequest()
        {
            var errorMessage = "Error Message";
            var companyService = new Mock<ICompanyService>();
            companyService
                .Setup(m => m.UpdateCompanyAsync(It.IsAny<int>(), It.IsAny<UpdateCompanyDto>()))
                .ThrowsAsync(new Exception(errorMessage));
            var webService = new UpdateCompanyService(companyService.Object);
            var dto = new UpdateCompanyDto();

            var actionResult = await webService.UpdateCompanyAsync(1, dto);

            Assert.NotNull(actionResult);
            Assert.False(actionResult.IsSuccess);
            Assert.AreEqual(HttpStatusCode.BadRequest, actionResult.StatusCode);
            Assert.Null(actionResult.Object);
            Assert.NotNull(actionResult.ErrorMessage);
            Assert.AreEqual(errorMessage, actionResult.ErrorMessage);
            companyService
                .Verify(m => m.UpdateCompanyAsync(It.IsAny<int>(), It.IsAny<UpdateCompanyDto>()), Times.Once);
        }
    }
}
