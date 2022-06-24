using DataAccess.Services.Interfaces;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;
using DataAccess.Services.Models;
using Moq;
using Web.Services.Companies.Implementation;

namespace Web.Services.Tests.Companies.Implementation
{
    [TestFixture]
    public class CreateCompanyServiceTests
    {
        [Test]
        public void Ctor_When_CompanyServiceIsNull_Throw_ArgumentNullException()
        {
            Action action = () => new CreateCompanyService(null);

            Assert.Throws<ArgumentNullException>(() => action());
        }

        [Test]
        public async Task CreateCompanyAsync_When_CompanyServiceIsCalled_Return_CreatedAndObject()
        {
            var companyDto = new CompanyDto();
            var companyService = new Mock<ICompanyService>();
            companyService
                .Setup(m => m.CreateCompanyAsync(It.IsAny<UpdateCompanyDto>()))
                .ReturnsAsync(companyDto);
            var webService = new CreateCompanyService(companyService.Object);
            var updateCompanyDto = new UpdateCompanyDto();

            var actionResult = await webService.CreateCompanyAsync(updateCompanyDto);

            Assert.NotNull(actionResult);
            Assert.True(actionResult.IsSuccess);
            Assert.AreEqual(HttpStatusCode.Created, actionResult.StatusCode);
            Assert.NotNull(actionResult.Object);
            Assert.AreSame(companyDto, actionResult.Object);
            Assert.Null(actionResult.ErrorMessage);
            companyService.Verify(m => m.CreateCompanyAsync(It.IsAny<UpdateCompanyDto>()), Times.Once);
        }

        [Test]
        public async Task CreateCompanyAsync_When_CompanyServiceThrowsException_Return_BadRequestAndErrorMessage()
        {
            var errorMessage = "ErrorMessage";
            var companyService = new Mock<ICompanyService>();
            companyService
                .Setup(m => m.CreateCompanyAsync(It.IsAny<UpdateCompanyDto>()))
                .ThrowsAsync(new Exception(errorMessage));
            var webService = new CreateCompanyService(companyService.Object);
            var updateCompanyDto = new UpdateCompanyDto();

            var actionResult = await webService.CreateCompanyAsync(updateCompanyDto);

            Assert.NotNull(actionResult);
            Assert.False(actionResult.IsSuccess);
            Assert.AreEqual(HttpStatusCode.BadRequest, actionResult.StatusCode);
            Assert.Null(actionResult.Object);
            Assert.NotNull(actionResult.ErrorMessage);
            Assert.AreEqual(errorMessage, actionResult.ErrorMessage);
            companyService.Verify(m => m.CreateCompanyAsync(It.IsAny<UpdateCompanyDto>()), Times.Once);
        }
    }
}
