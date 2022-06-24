using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using DataAccess.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Companies.Interfaces;
using WebAPI.Controllers;
using WebAPI.Factories.Interfaces;
using ActionResult = Web.Services.Models.ActionResult;

namespace WebAPI.Tests.Controllers
{
    [TestFixture]
    public class CompaniesControllerTests
    {
        private readonly Mock<IGetCompanyService> mockGetService = new Mock<IGetCompanyService>();
        private readonly Mock<ICreateCompanyService> mockCreateService = new Mock<ICreateCompanyService>();
        private readonly Mock<IUpdateCompanyService> mockUpdateService = new Mock<IUpdateCompanyService>();
        private readonly Mock<IDeleteCompanyService> mockDeleteService = new Mock<IDeleteCompanyService>();

        [Test]
        public async Task GetCompaniesAsync_ServiceAndFactoryAreCalled_ActionResult()
        {
            var mockActionResult = new Mock<IActionResult>();
            var mockFactory = CreateActionResultFactory(mockActionResult.Object);
            var controller = CreateCompaniesController(mockFactory.Object);

            var result = await controller.GetCompaniesAsync();

            Assert.AreSame(mockActionResult.Object, result);
            mockGetService.Verify(m => m.GetAllCompaniesAsync(), Times.Once);
            mockFactory.Verify(m => m.CreateActionResult(It.IsAny<ActionResult>()), Times.Once);
        }

        [Test]
        public async Task GetCompanyAsync_ServiceAndFactoryAreCalled_ActionResult()
        {
            var mockActionResult = new Mock<IActionResult>();
            var mockFactory = CreateActionResultFactory(mockActionResult.Object);
            var controller = CreateCompaniesController(mockFactory.Object);

            var result = await controller.GetCompanyAsync(1);

            Assert.AreSame(mockActionResult.Object, result);
            mockGetService.Verify(m => m.GetCompanyByIdAsync(It.IsAny<int>()), Times.Once);
            mockFactory.Verify(m => m.CreateActionResult(It.IsAny<ActionResult>()), Times.Once);
        }

        [Test]
        public async Task CreateCompanyAsync_ServiceAndFactoryAreCalled_ActionResult()
        {
            var mockActionResult = new Mock<IActionResult>();
            var mockFactory = CreateActionResultFactory(mockActionResult.Object);
            var controller = CreateCompaniesController(mockFactory.Object);

            var result = await controller.CreateCompanyAsync(new UpdateCompanyDto());

            Assert.AreSame(mockActionResult.Object, result);
            mockCreateService.Verify(m => m.CreateCompanyAsync(It.IsAny<UpdateCompanyDto>()), Times.Once);
            mockFactory.Verify(m => m.CreateActionResult(It.IsAny<ActionResult>()), Times.Once);
        }

        [Test]
        public async Task UpdateCompanyAsync_ServiceAndFactoryAreCalled_ActionResult()
        {
            var mockActionResult = new Mock<IActionResult>();
            var mockFactory = CreateActionResultFactory(mockActionResult.Object);
            var controller = CreateCompaniesController(mockFactory.Object);

            var result = await controller.UpdateCompanyAsync(1, new UpdateCompanyDto());

            Assert.AreSame(mockActionResult.Object, result);
            mockUpdateService.Verify(m => m.UpdateCompanyAsync(It.IsAny<int>(), It.IsAny<UpdateCompanyDto>()), Times.Once);
            mockFactory.Verify(m => m.CreateActionResult(It.IsAny<ActionResult>()), Times.Once);
        }

        [Test]
        public async Task DeleteCompanyAsync_ServiceAndFactoryAreCalled_ActionResult()
        {
            var mockActionResult = new Mock<IActionResult>();
            var mockFactory = CreateActionResultFactory(mockActionResult.Object);
            var controller = CreateCompaniesController(mockFactory.Object);

            var result = await controller.DeleteCompanyAsync(1);

            Assert.AreSame(mockActionResult.Object, result);
            mockDeleteService.Verify(m => m.DeleteCompanyAsync(It.IsAny<int>()), Times.Once);
            mockFactory.Verify(m => m.CreateActionResult(It.IsAny<ActionResult>()), Times.Once);
        }

        private CompaniesController CreateCompaniesController(IActionResultFactory actionResultFactory)
        {
            return new CompaniesController(
                mockGetService.Object,
                mockCreateService.Object,
                mockUpdateService.Object,
                mockDeleteService.Object,
                actionResultFactory);
        }

        private Mock<IActionResultFactory> CreateActionResultFactory(IActionResult actionResult)
        {
            var mockFactory = new Mock<IActionResultFactory>();

            mockFactory.Setup(m => m.CreateActionResult(It.IsAny<ActionResult>()))
                .Returns(actionResult);

            return mockFactory;
        }

    }
}
