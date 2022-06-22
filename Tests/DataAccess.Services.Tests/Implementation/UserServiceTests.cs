using AutoMapper;
using DataAccess.Interfaces;
using DataAccess.Services.Implementation;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Services.Exceptions;
using DataAccess.Services.MapProfiles;
using DataAccess.Services.Models;
using DataAccess.Services.Tests.Extensions;
using DbUser = DataAccess.Models.User;
using DbCompany = DataAccess.Models.Company;

namespace DataAccess.Services.Tests.Implementation
{
    [TestFixture]
    public class UserServiceTests
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
            Action action = () => new UserService(null, _mapper);

            Assert.Throws<ArgumentNullException>(() => action());
        }

        [Test]
        public void Ctor_When_MapperIsNull_Throw_ArgumentNullException()
        {
            var mockedContext = new Mock<IUserContext>();

            Action action = () => new UserService(mockedContext.Object, null);

            Assert.Throws<ArgumentNullException>(() => action());
        }

        [Test]
        public async Task CreateUserAsync_When_UserIsNull_Throw_ArgumentNullException()
        {
            var mockedContext = new Mock<IUserContext>();
            var userService = new UserService(mockedContext.Object, _mapper);

            var action = async () => await userService.CreateUserAsync(null);

            Assert.ThrowsAsync<ArgumentNullException>(() => action());
        }

        [Test]
        public async Task CreateUserAsync_When_UserExists_Throw_RequestedResourceHasConflictException()
        {
            var mockedContext = new Mock<IUserContext>();
            mockedContext.Setup(m => m.Users).ReturnsEntitySet(new List<DbUser>()
            {
                new DbUser
                {
                    Auth0Id = "Auth0Id"
                }
            });
            var userService = new UserService(mockedContext.Object, _mapper);
            var userDto = new UpdateUserDto
            {
                Auth0Id = "Auth0Id"
            };

            var action = async () => await userService.CreateUserAsync(userDto);

            Assert.ThrowsAsync<RequestedResourceHasConflictException>(() => action());
        }

        [Test]
        public async Task CreateUserAsync_When_UserIsValid_Then_UserIsAdded()
        {
            var mockedEntitySet = new Mock<IEntitySet<DbUser>>();
            var mockedContext = new Mock<IUserContext>();
            mockedContext.SetupSequence(m => m.Users)
                .ReturnsEntitySet(new List<DbUser>())
                .Returns(mockedEntitySet.Object);
            var userService = new UserService(mockedContext.Object, _mapper);
            var updateUserDto = CreateUpdateUserDto("Auth0Id", "First Name", "Last Name", "email@email.com", "195.1.1.1", 1);

            var userDto = await userService.CreateUserAsync(updateUserDto);

            Assert.NotNull(userDto);
            Assert.AreEqual(updateUserDto.Auth0Id, userDto.Auth0Id);
            Assert.AreEqual(updateUserDto.FirstName, userDto.FirstName);
            Assert.AreEqual(updateUserDto.LastName, userDto.LastName);
            Assert.AreEqual(updateUserDto.Email, userDto.Email);
            Assert.AreEqual(updateUserDto.IpAddress, userDto.IpAddress);
            Assert.AreEqual(updateUserDto.CompanyId, userDto.CompanyId);
            mockedContext.Verify(m => m.Users.Add(It.IsNotNull<DbUser>()), Times.Once);
            mockedContext.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        public async Task UpdateUserAsync_When_UserIsNull_Throw_ArgumentNullException()
        {
            var mockedContext = new Mock<IUserContext>();
            var userService = new UserService(mockedContext.Object, _mapper);

            var action = async () => await userService.UpdateUserAsync(1, null);

            Assert.ThrowsAsync<ArgumentNullException>(() => action());
        }

        [Test]
        public async Task UpdateUserAsync_When_UserDoesNotExist_Throw_RequestedResourceNotFoundException()
        {
            var mockedContext = new Mock<IUserContext>();
            mockedContext.Setup(m => m.Users).ReturnsEntitySet(new List<DbUser>());
            var userService = new UserService(mockedContext.Object, _mapper);
            var updateUserDto = CreateUpdateUserDto("Auth0Id", "First Name", "Last Name", "email@email.com", "195.1.1.1", 1);

            var action = async () => await userService.UpdateUserAsync(1, updateUserDto);

            Assert.ThrowsAsync<RequestedResourceNotFoundException>(() => action());
        }

        [Test]
        public async Task UpdateUserAsync_When_HasConflict_Throw_RequestedResourceHasConflictException()
        {
            var mockedContext = new Mock<IUserContext>();
            mockedContext.Setup(m => m.Users).ReturnsEntitySet(new List<DbUser>()
            {
                new DbUser
                {
                    Id = 1,
                    Auth0Id = "Old Auth0Id"
                },
                new DbUser
                {
                    Id = 2,
                    Auth0Id = "New Auth0Id"
                }
            });
            var userService = new UserService(mockedContext.Object, _mapper);
            var updateUserDto = new UpdateUserDto
            {
                Auth0Id = "New Auth0Id"
            };

            var action = async () => await userService.UpdateUserAsync(1, updateUserDto);

            Assert.ThrowsAsync<RequestedResourceHasConflictException>(() => action());
        }


        [Test]
        public async Task UpdateUserAsync_When_UserIsValid_Then_UserIsUpdated()
        {
            var mockedContext = new Mock<IUserContext>();
            mockedContext.Setup(m => m.Users).ReturnsEntitySet(new List<DbUser>()
            {
                new DbUser
                {
                    Id = 1,
                    Auth0Id = "Old Auth0Id",
                    FirstName = "Old First Name",
                    LastName = "Old Last Name",
                    Email = "old_email@email.com",
                    IpAddress = "195.1.1.2",
                    CompanyId = 1
                }
            });
            var userService = new UserService(mockedContext.Object, _mapper);
            var updateUserDto = CreateUpdateUserDto("New Auth0Id", "New First Name", "New Last Name", "new_email@email.com", "195.1.1.2", 2);

            var userDto =  await userService.UpdateUserAsync(1, updateUserDto);

            Assert.NotNull(userDto);
            Assert.NotNull(userDto);
            Assert.AreEqual(updateUserDto.Auth0Id, userDto.Auth0Id);
            Assert.AreEqual(updateUserDto.FirstName, userDto.FirstName);
            Assert.AreEqual(updateUserDto.LastName, userDto.LastName);
            Assert.AreEqual(updateUserDto.Email, userDto.Email);
            Assert.AreEqual(updateUserDto.IpAddress, userDto.IpAddress);
            Assert.AreEqual(updateUserDto.CompanyId, userDto.CompanyId);
            mockedContext.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task DeleteUserAsync_When_UserDoesNotExist_Throw_RequestedResourceNotFoundException()
        {
            var mockedContext = new Mock<IUserContext>();
            mockedContext.Setup(m => m.Users).ReturnsEntitySet(new List<DbUser>());
            var userService = new UserService(mockedContext.Object, _mapper);

            var action = async () => await userService.DeleteUserAsync(1);

            Assert.ThrowsAsync<RequestedResourceNotFoundException>(() => action());
        }

        [Test]
        public async Task DeleteUserAsync_When_UserExists_UsersIsDeleted()
        {
            var mockedContext = new Mock<IUserContext>();
            var mockedEntitySet = new Mock<IEntitySet<DbUser>>();
            mockedContext.SetupSequence(m => m.Users)
                .ReturnsEntitySet(new List<DbUser>()
                {
                    new DbUser
                    {
                        Id = 1
                    }
                })
                .Returns(mockedEntitySet.Object);
            var userService = new UserService(mockedContext.Object, _mapper);

            await userService.DeleteUserAsync(1);

            mockedContext.Verify(m => m.Users.Remove(It.IsNotNull<DbUser>()), Times.Once);
            mockedContext.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task FindUserByIdAsync_UserDoesNotExist_Trow_RequestedResourceNotFoundException()
        {
            var mockedContext = new Mock<IUserContext>();
            mockedContext.Setup(m => m.Users).ReturnsEntitySet(new List<DbUser>());
            var userService = new UserService(mockedContext.Object, _mapper);

            var action = async () => await userService.FindUserByIdAsync(1);

            Assert.ThrowsAsync<RequestedResourceNotFoundException>(() => action());
        }

        [Test]
        public async Task FindUserByIdAsync_UserExists_Return_UserDto()
        {
            var dbUser = new DbUser
            {
                Id = 1,
                Auth0Id = "Auth0Id",
                FirstName = "First Name",
                LastName = "Last Name",
                Email = "email@email.co,",
                IpAddress = "195.1.1.1",
                CompanyId = 1,
                Company = new DbCompany
                {
                    Id = 1,
                    Name = "Company Name"
                }
            };

            var mockedContext = new Mock<IUserContext>();
            mockedContext.Setup(m => m.Users).ReturnsEntitySet(new List<DbUser>()
            {
                dbUser
            });
            var userService = new UserService(mockedContext.Object, _mapper);

            var userDto = await userService.FindUserByIdAsync(1);

            Assert.IsNotNull(userDto);
            Assert.AreEqual(dbUser.Id, userDto.Id);
            Assert.AreEqual(dbUser.FirstName, userDto.FirstName);
            Assert.AreEqual(dbUser.LastName, userDto.LastName);
            Assert.AreEqual(dbUser.Email, userDto.Email);
            Assert.AreEqual(dbUser.IpAddress, userDto.IpAddress);
            Assert.AreEqual(dbUser.CompanyId, userDto.CompanyId);
            Assert.AreEqual(dbUser.Company.Name, userDto.CompanyName);
        }

        [TestCase(null)]
        [TestCase("")]
        public async Task FindUserByAuth0IdAsync_Auth0IdIsNull_Throw_ArgumentNullException(string auth0Id)
        {
            var mockedContext = new Mock<IUserContext>();
            mockedContext.Setup(m => m.Users).ReturnsEntitySet(new List<DbUser>());
            var userService = new UserService(mockedContext.Object, _mapper);

            var action = async () => await userService.FindUserByAuth0IdAsync(auth0Id);

            Assert.ThrowsAsync<ArgumentNullException>(() => action());
        }

        [Test]
        public async Task FindUserByAuth0IdAsync_UserDoesNotExist_Trow_RequestedResourceNotFoundException()
        {
            var mockedContext = new Mock<IUserContext>();
            mockedContext.Setup(m => m.Users).ReturnsEntitySet(new List<DbUser>());
            var userService = new UserService(mockedContext.Object, _mapper);

            var action = async () => await userService.FindUserByAuth0IdAsync("Auth0Id");

            Assert.ThrowsAsync<RequestedResourceNotFoundException>(() => action());
        }

        [Test]
        public async Task FindUserByAuth0IdAsync_UserExists_Return_UserDto()
        {
            var dbUser = new DbUser
            {
                Id = 1,
                Auth0Id = "Auth0Id",
                FirstName = "First Name",
                LastName = "Last Name",
                Email = "email@email.co,",
                IpAddress = "195.1.1.1",
                CompanyId = 1,
                Company = new DbCompany
                {
                    Id = 1,
                    Name = "Company Name"
                }
            };

            var mockedContext = new Mock<IUserContext>();
            mockedContext.Setup(m => m.Users).ReturnsEntitySet(new List<DbUser>()
            {
                dbUser
            });
            var userService = new UserService(mockedContext.Object, _mapper);

            var userDto = await userService.FindUserByAuth0IdAsync("Auth0Id");

            Assert.IsNotNull(userDto);
            Assert.AreEqual(dbUser.Id, userDto.Id);
            Assert.AreEqual(dbUser.FirstName, userDto.FirstName);
            Assert.AreEqual(dbUser.LastName, userDto.LastName);
            Assert.AreEqual(dbUser.Email, userDto.Email);
            Assert.AreEqual(dbUser.IpAddress, userDto.IpAddress);
            Assert.AreEqual(dbUser.CompanyId, userDto.CompanyId);
            Assert.AreEqual(dbUser.Company.Name, userDto.CompanyName);
        }

        private UpdateUserDto CreateUpdateUserDto(
            string auth0Id,
            string firstName,
            string lastName,
            string email,
            string ipAddress,
            int? companyId)
        {
            return new UpdateUserDto
            {

                Auth0Id = auth0Id,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                IpAddress = ipAddress,
                CompanyId = companyId
            };
        }
    }
}
