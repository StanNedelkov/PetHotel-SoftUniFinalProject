using Microsoft.EntityFrameworkCore;
using PetHotel.Core.Contracts;
using PetHotel.Core.Services;
using PetHotel.Infrastructure.Data;
using PetHotel.Infrastructure.Data.Entities;

namespace PetHotel.UnitTests
{
    [TestFixture]
    public class HomeServiceTests
    {
        private PetHotelDbContext context;
        private IHomeService homeService;
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
        public async Task TestGetPrices()
        {
            homeService = new HomeService(context);

            var type = new PetType()
            {
                Name = "Test",
                CostPerDay = 10,
            };

            await context.PetTypes.AddAsync(type);
            await context.SaveChangesAsync();

            var modelList = await homeService.GetPricesAsync();

            Assert.True(modelList.Any());
        }

        [Test]
        public async Task TestGetPricesNull()
        {
            homeService = new HomeService(context);

            

            var modelList = await homeService.GetPricesAsync();

            Assert.False(modelList.Any());
        }
    }
}
