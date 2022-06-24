using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using DataAccess.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Users.Interfaces;
using WebAPI.Controllers;
using WebAPI.Factories.Interfaces;
using ActionResult = Web.Services.Models.ActionResult;

namespace WebAPI.Tests.Controllers
{
    [TestFixture]
    public class UsersControllerTests
    {
        private readonly Mock<IGetUserService> mockGetUserService = new Mock<IGetUserService>();
        private readonly Mock<ICreateUserService> mockCreateUserService = new Mock<ICreateUserService>();
        private readonly Mock<IUpdateUserService> mockUpdateUserService = new Mock<IUpdateUserService>();
        private readonly Mock<IDeleteUserService> mockDeleteUserService = new Mock<IDeleteUserService>();

        [Test]
        public async Task GetUserAsync_ServiceAndFactoryAreCalled_ActionResult()
        {
            var mockActionResult = new Mock<IActionResult>();
            var mockFactory = CreateActionResultFactory(mockActionResult.Object);
            var controller = CreateUsersController(mockFactory.Object);

            var result = await controller.GetUserAsync("Auth0Id");

            Assert.AreSame(mockActionResult.Object, result);
            mockGetUserService.Verify(m => m.GetUserByAuth0IdAsync(It.IsAny<string>()), Times.Once);
            mockFactory.Verify(m => m.CreateActionResult(It.IsAny<ActionResult>()), Times.Once);
        }

        [Test]
        public async Task CreateUserAsync_ServiceAndFactoryAreCalled_ActionResult()
        {
            var mockActionResult = new Mock<IActionResult>();
            var mockFactory = CreateActionResultFactory(mockActionResult.Object);
            var controller = CreateUsersController(mockFactory.Object);

            var result = await controller.CreateUserAsync(new UpdateUserDto());

            Assert.AreSame(mockActionResult.Object, result);
            mockCreateUserService.Verify(m => m.CreateUserAsync(It.IsAny<UpdateUserDto>()), Times.Once);
            mockFactory.Verify(m => m.CreateActionResult(It.IsAny<ActionResult>()), Times.Once);
        }

        [Test]
        public async Task UpdateUserAsync_ServiceAndFactoryAreCalled_ActionResult()
        {
            var mockActionResult = new Mock<IActionResult>();
            var mockFactory = CreateActionResultFactory(mockActionResult.Object);
            var controller = CreateUsersController(mockFactory.Object);

            var result = await controller.UpdateUserAsync(1, new UpdateUserDto());

            Assert.AreSame(mockActionResult.Object, result);
            mockUpdateUserService.Verify(m => m.UpdateUserAsync(It.IsAny<int>(), It.IsAny<UpdateUserDto>()), Times.Once);
            mockFactory.Verify(m => m.CreateActionResult(It.IsAny<ActionResult>()), Times.Once);
        }

        [Test]
        public async Task DeleteUserAsync_ServiceAndFactoryAreCalled_ActionResult()
        {
            var mockActionResult = new Mock<IActionResult>();
            var mockFactory = CreateActionResultFactory(mockActionResult.Object);
            var controller = CreateUsersController(mockFactory.Object);

            var result = await controller.DeleteUserAsync(1);

            Assert.AreSame(mockActionResult.Object, result);
            mockDeleteUserService.Verify(m => m.DeleteUserAsync(It.IsAny<int>()), Times.Once);
            mockFactory.Verify(m => m.CreateActionResult(It.IsAny<ActionResult>()), Times.Once);
        }

        private UsersController CreateUsersController(IActionResultFactory factory)
        {
            return new UsersController(
                mockGetUserService.Object,
                mockCreateUserService.Object,
                mockUpdateUserService.Object,
                mockDeleteUserService.Object,
                factory);
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
