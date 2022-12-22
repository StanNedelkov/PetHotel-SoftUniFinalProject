/*using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using PetHotel.Core.Contracts;
using PetHotel.Core.Services;
using PetHotel.Infrastructure.Data;
using PetHotel.Infrastructure.Data.Entities;

namespace PetHotel.UnitTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private PetHotelDbContext context;
        private IUserService userService;
        private  UserManager<User> userManager;
        private  SignInManager<User> signInManager;
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
        public async Task TestGetEmployeeUser()
        {
            var UserStoreMock = Mock.Of<IUserStore<User>>();

            var userManagerMock = new Mock<UserManager<User>>(UserStoreMock, null, null, null, null, null, null, null, null);
            userManager = userManagerMock.Object;

            var signInManagerMock = new Mock<SignInManager<User>>();
            signInManager = signInManagerMock.Object;
            userService = new UserService(userManager, signInManager, context);

            var user = new User()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                FirstName = "Admin",
                LastName = "User",
                IsActive = true,

            };
            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user,"Parola1!");

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var resultUser = await userService.EmployeeUser("admin");

            Assert.That(resultUser, Is.Not.Null);
        }
    }
}*/
