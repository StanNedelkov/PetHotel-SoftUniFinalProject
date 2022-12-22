using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using PetHotel.Core.Contracts;
using PetHotel.Core.Services;
using PetHotel.Infrastructure.Data;
using PetHotel.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.UnitTests
{
    [TestFixture]
    public class AdminServiceTests
    {
        private PetHotelDbContext context;
        private IAdminService adminService;
        private UserManager<User> userManager;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<PetHotelDbContext>()
                .UseInMemoryDatabase("PetHotelDB")
                .Options;

            this.context = new PetHotelDbContext(contextOptions);
            this.context.Database.EnsureDeleted();
            this.context.Database.EnsureCreated();
        }

        [Test]
        public async Task TestActivateUser()
        {
            var UserStoreMock = Mock.Of<IUserStore<User>>();

            var userManagerMock = new Mock<UserManager<User>>(UserStoreMock, null, null, null, null, null, null, null, null);
            userManager = userManagerMock.Object;

            adminService = new AdminService(context, userManager);

            var user = new User()
            {
                Email = "stenly.nedelkov@gmail.com",
                UserName = "stan",
                FirstName = "stan",
                LastName = "stan",
                IsActive = false,

            };
            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, "Parola1!");

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            string userId = await context
                .Users
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            await adminService.ActivateUserAsync(userId);

            bool userStatus = await context
                .Users
                .Select(x => x.IsActive)
                .FirstOrDefaultAsync();

            Assert.True(userStatus);
        }

        [Test]
        public async Task TestActivateUserExceptionMessage()
        {
            var UserStoreMock = Mock.Of<IUserStore<User>>();

            var userManagerMock = new Mock<UserManager<User>>(UserStoreMock, null, null, null, null, null, null, null, null);
            userManager = userManagerMock.Object;

            adminService = new AdminService(context, userManager);

            var user = new User()
            {
                Email = "stenly.nedelkov@gmail.com",
                UserName = "stan",
                FirstName = "stan",
                LastName = "stan",
                IsActive = false,

            };
            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, "Parola1!");

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            string userId = await context
                .Users
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            try
            {
                await adminService.ActivateUserAsync("someFakeId");
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.Pass();
            }
        }


        [Test]
        public async Task TestDeactivateUser()
        {
            var UserStoreMock = Mock.Of<IUserStore<User>>();

            var userManagerMock = new Mock<UserManager<User>>(UserStoreMock, null, null, null, null, null, null, null, null);
            userManager = userManagerMock.Object;

            adminService = new AdminService(context, userManager);

            var user = new User()
            {
                Email = "stenly.nedelkov@gmail.com",
                UserName = "stan",
                FirstName = "stan",
                LastName = "stan",
                IsActive = true,

            };
            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, "Parola1!");

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            string userId = await context
                .Users
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            await adminService.DeactivateUserAsync(userId);

            bool userStatus = await context
                .Users
                .Select(x => x.IsActive)
                .FirstOrDefaultAsync();

            Assert.False(userStatus);
        }

        [Test]
        public async Task TestDeactivateUserExceptionMessage()
        {
            var UserStoreMock = Mock.Of<IUserStore<User>>();

            var userManagerMock = new Mock<UserManager<User>>(UserStoreMock, null, null, null, null, null, null, null, null);
            userManager = userManagerMock.Object;

            adminService = new AdminService(context, userManager);

            var user = new User()
            {
                Email = "stenly.nedelkov@gmail.com",
                UserName = "stan",
                FirstName = "stan",
                LastName = "stan",
                IsActive = true,

            };
            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, "Parola1!");

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            string userId = await context
                .Users
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            try
            {
                await adminService.DeactivateUserAsync("someFakeId");
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task TestGetAllUsersException()
        {
            var UserStoreMock = Mock.Of<IUserStore<User>>();

            var userManagerMock = new Mock<UserManager<User>>(UserStoreMock, null, null, null, null, null, null, null, null);
            userManager = userManagerMock.Object;

            adminService = new AdminService(context, userManager);

            var user = new User()
            {
                Email = "stenly.nedelkov@gmail.com",
                UserName = "stan",
                FirstName = "stan",
                LastName = "stan",
                IsActive = true,

            };
            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, "Parola1!");

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            try
            {
                var modelList = await adminService.GetAllUsersAsync();
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {

                Assert.Pass();
            }
        }
    }
}
