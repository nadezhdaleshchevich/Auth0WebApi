using DataAccess.Services.Interfaces;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;
using DataAccess.Services.Exceptions;
using Moq;
using Web.Services.Companies.Implementation;

namespace Web.Services.Tests.Companies.Implementation
{
    [TestFixture]
    public class DeleteCompanyServiceTests
    {
        [Test]
        public void Ctor_When_CompanyServiceIsNull_Throw_ArgumentNullException()
        {
            Action action = () => new DeleteCompanyService(null);

            Assert.Throws<ArgumentNullException>(() => action());
        }

        [Test]
        public async Task DeleteCompanyService_When_CompanyServiceIsCalled_Return_Ok()
        {
            var companyService = new Mock<ICompanyService>();
            var webService = new DeleteCompanyService(companyService.Object);

            var actionResult = await webService.DeleteCompanyAsync(1);

            Assert.NotNull(actionResult);
            Assert.True(actionResult.IsSuccess);
            Assert.AreEqual(HttpStatusCode.OK, actionResult.StatusCode);
            Assert.Null(actionResult.Object);
            Assert.Null(actionResult.ErrorMessage);
            companyService.Verify(m => m.DeleteCompanyAsync(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task DeleteCompanyService_When_CompanyServiceThrowsRequestedResourceNotFoundException_Return_NotFound()
        {
            var companyService = new Mock<ICompanyService>();
            companyService
                .Setup(m => m.DeleteCompanyAsync(It.IsAny<int>()))
                .ThrowsAsync(new RequestedResourceNotFoundException());
            var webService = new DeleteCompanyService(companyService.Object);

            var actionResult = await webService.DeleteCompanyAsync(1);

            Assert.NotNull(actionResult);
            Assert.False(actionResult.IsSuccess);
            Assert.AreEqual(HttpStatusCode.NotFound, actionResult.StatusCode);
            Assert.Null(actionResult.Object);
            Assert.NotNull(actionResult.ErrorMessage);
            companyService.Verify(m => m.DeleteCompanyAsync(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task DeleteCompanyService_When_CompanyServiceThrowsException_Return_BadRequest()
        {
            var errorMessage = "Error Message";
            var companyService = new Mock<ICompanyService>();
            companyService
                .Setup(m => m.DeleteCompanyAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception(errorMessage));
            var webService = new DeleteCompanyService(companyService.Object);

            var actionResult = await webService.DeleteCompanyAsync(1);

            Assert.NotNull(actionResult);
            Assert.False(actionResult.IsSuccess);
            Assert.AreEqual(HttpStatusCode.BadRequest, actionResult.StatusCode);
            Assert.Null(actionResult.Object);
            Assert.NotNull(actionResult.ErrorMessage);
            Assert.AreEqual(errorMessage, actionResult.ErrorMessage);
            companyService.Verify(m => m.DeleteCompanyAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
