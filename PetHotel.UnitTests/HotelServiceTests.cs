using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using PetHotel.Common;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.HotelModels;
using PetHotel.Core.Services;
using PetHotel.Infrastructure.Data;
using PetHotel.Infrastructure.Data.Entities;

namespace PetHotel.UnitTests
{
    [TestFixture]
    public class HotelServiceTests
    {
        private PetHotelDbContext context;
        private IHotelService hotelService;
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
        public async Task TestGuestAdd()
        {
            hotelService = new HotelService(context);

            await hotelService.AddGuestAsync(new AddGuestViewModel() 
            { 
                Id = 2,
                CheckInDate = "2022-12-29",
                CheckOutDate = "2022-12-30",
                Name= "Doggy"
            });

            var guest = await context
                .Schedules
                .FindAsync(1);

            Assert.That(guest.PetID, Is.EqualTo(2));
        }

        [Test]
        public async Task TestCancelReservation()
        {
            hotelService = new HotelService(context);

            await hotelService.AddGuestAsync(new AddGuestViewModel()
            {
                Id = 2,
                CheckInDate = "2022-12-29",
                CheckOutDate = "2022-12-30",
                Name = "Doggy"
            });

            await hotelService.CancelHotelStayAsync(1);

            var guest = await context
                .Schedules
                .FirstOrDefaultAsync(x => x.PetID == 2);

            Assert.That(guest.Status, Is.EqualTo("Canceled"));
        }


        [Test]
        public async Task TestGuestModelById()
        {
            hotelService = new HotelService(context);

            string petName = "test";

            var pet = new Pet()
            {
                Name = petName,
                UserID = "3cf9137f-a0fe-4da0-9cf6-798c97639ade",
                PetTypeID = 1,
                HotelID = 1,
                Alergies = "none"
            };

            //test method needs also a pet type
            var type = new PetType()
            {
                Name = "Test"
            };

            await context.Pets.AddAsync(pet);
            await context.PetTypes.AddAsync(type);
            await context.SaveChangesAsync();

            //there should be only one pet in db. Finding it with the context in case its given a different Id
            var petResult = await context
                .Pets
                .AsNoTracking()
                .FirstOrDefaultAsync();


            var model = await hotelService.GetGuestToAddAsync(petResult.Id);

            Assert.That(model.Id, Is.EqualTo(petResult.Id));
            Assert.That(model.Name, Is.EqualTo(petName));
        }

        [Test]
        public async Task TestEditGuestModelById()
        {
            hotelService = new HotelService(context);

            string petName = "testEdit";
            int petId = 3;
            int reservationId = 1;

            var reservationModel = new Reservation()
            {
                PetID = petId,
                PetName = petName,
                AdmissionDate = DateTime.Now,
                DepartureDate = DateTime.Now,
                Status = GlobalConstants.ExpectedStatus,
                HotelID = 1
            };
            await context.Schedules.AddAsync(reservationModel);
            await context.SaveChangesAsync();


            var model = await hotelService.GetGuestToEditAsync(reservationId); 

            Assert.That(model.Id, Is.EqualTo(petId));
            Assert.That(model.Name, Is.EqualTo(petName));
        }

        [Test]
        public async Task TestAllGuestsByUser()
        {
            hotelService = new HotelService(context);
            string userId = "3cf9137f-a0fe-4da0-9cf6-798c97639ade";
            string petName = "testEdit";
            int petId = 1;
            

            var reservationModel = new Reservation()
            {
                PetID = petId,
                PetName = petName,
                AdmissionDate = DateTime.Now,
                DepartureDate = DateTime.Now,
                Status = GlobalConstants.ExpectedStatus,
                HotelID = 1
            };

            /*var hasher = new PasswordHasher();
            var userModel = new User()
            {
                Id = userId,
                UserName = "Stan",
                NormalizedUserName = "STAN",
                Email = "stenly.nedelkov@gmail.com",
                NormalizedEmail = "STENLY.NEDELKOV@GMAIL.COM",
                FirstName = "Stanislav",
                LastName = "Nedelkov",
                IsActive = true,
            };
            userModel.PasswordHash = hasher.HashPassword("Parola1!");*/
            var pet = new Pet()
            {
                Name = petName,
                UserID = userId,
                PetTypeID = 1,
                HotelID = 1,
                Alergies = "none"
            };
            var type = new PetType()
            {
                Name = "Test"
            };
           

            /*await context.Users.AddAsync(userModel);*/
            await context.Pets.AddAsync(pet);
            await context.PetTypes.AddAsync(type);
            await context.Schedules.AddAsync(reservationModel);
            await context.SaveChangesAsync();

            var petList = await hotelService.GetMyAllGuestsAsync(userId);

            Assert.That(petList.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task TestAllGuestsByUserEmtyResult()
        {
            hotelService = new HotelService(context);
            string userId = "3cf9137f-a0fe-4da0-9cf6-798c97639ade";
            string petName = "testEdit";
            int petId = 1;
            int differentPetId = 2;
            int reservationId = 1;

            var reservationModel = new Reservation()
            {
                PetID = differentPetId,
                PetName = petName,
                AdmissionDate = DateTime.Now,
                DepartureDate = DateTime.Now,
                Status = GlobalConstants.ExpectedStatus,
                HotelID = 1
            };

            /*var hasher = new PasswordHasher();
            var userModel = new User()
            {
                Id = userId,
                UserName = "Stan",
                NormalizedUserName = "STAN",
                Email = "stenly.nedelkov@gmail.com",
                NormalizedEmail = "STENLY.NEDELKOV@GMAIL.COM",
                FirstName = "Stanislav",
                LastName = "Nedelkov",
                IsActive = true,
            };
            userModel.PasswordHash = hasher.HashPassword("Parola1!");*/
            var pet = new Pet()
            {
                Name = petName,
                UserID = userId,
                PetTypeID = 1,
                HotelID = 1,
                Alergies = "none"
            };
            var type = new PetType()
            {
                Name = "Test"
            };


            /*await context.Users.AddAsync(userModel);*/
            await context.Pets.AddAsync(pet);
            await context.PetTypes.AddAsync(type);
            await context.Schedules.AddAsync(reservationModel);
            await context.SaveChangesAsync();

            var petList = await hotelService.GetMyAllGuestsAsync(userId);

            Assert.That(petList, Is.Empty);
        }

        [Test]

        public async Task TestGuestDetailsById()
        {
            hotelService = new HotelService(context);
            string petName = "testName";
            string userId = "3cf9137f-a0fe-4da0-9cf6-798c97639ade";
            int petId = 1;

            var pet = new Pet()
            {
                Name = petName,
                UserID = userId,
                PetTypeID = 1,
                HotelID = 1,
                Alergies = "none",
                Age= 5
            };
            var type = new PetType()
            {
                Name = "Test"
            };

            await context.Pets.AddAsync(pet);
            await context.PetTypes.AddAsync(type);
            await context.SaveChangesAsync();

            var model = await hotelService.GuestDetailsAsync(petId);

            Assert.That(model.Id, Is.EqualTo(petId));
        }

        [Test]

        public async Task TestGuestDetailsByIdException()
        {
            hotelService = new HotelService(context);
            string petName = "testName";
            string userId = "3cf9137f-a0fe-4da0-9cf6-798c97639ade";
            int petId = 1;
            int fakePetId = 2;

            var pet = new Pet()
            {
                Name = petName,
                UserID = userId,
                PetTypeID = 1,
                HotelID = 1,
                Alergies = "none",
                Age = 5
            };
            var type = new PetType()
            {
                Name = "Test"
            };

            await context.Pets.AddAsync(pet);
            await context.PetTypes.AddAsync(type);
            await context.SaveChangesAsync();

            try
            {
                var model = await hotelService.GuestDetailsAsync(fakePetId);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task TestUserOwnsPet()
        {

            hotelService = new HotelService(context);
            string petName = "testName";
          

            var hasher = new PasswordHasher();

            var userModel = new User()
            {
                UserName = "Stan",
                NormalizedUserName = "STAN",
                Email = "stenly.nedelkov@gmail.com",
                NormalizedEmail = "STENLY.NEDELKOV@GMAIL.COM",
                FirstName = "Stanislav",
                LastName = "Nedelkov",
                IsActive = true,
            };
            userModel.PasswordHash = hasher.HashPassword("Parola1!");

            await context.Users.AddAsync(userModel);
            await context.SaveChangesAsync();

            string userId = await context
                .Users
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            var pet = new Pet()
            {
                Name = petName,
                UserID = userId,
                PetTypeID = 1,
                HotelID = 1,
                Alergies = "none",
                Age = 5
            };
            var reservation = new Reservation()
            {
                HotelID = 1,
                PetID = 1,
                PetName = petName,
                Status = GlobalConstants.ExpectedStatus,
                AdmissionDate = DateTime.Now,
                DepartureDate = DateTime.Now,
            };
            await context.Pets.AddAsync(pet);
            await context.Schedules.AddAsync(reservation);
            await context.SaveChangesAsync();

            bool result = await hotelService.IsGuestOwnedByUser(1, userId);

            Assert.That(result, Is.True);


        }

        [Test]
        public async Task TestUserOwnsPetFalseResult()
        {

            hotelService = new HotelService(context);
            string petName = "testName";
            int fakeId = 54;


            var hasher = new PasswordHasher();

            var userModel = new User()
            {
                UserName = "Stan",
                NormalizedUserName = "STAN",
                Email = "stenly.nedelkov@gmail.com",
                NormalizedEmail = "STENLY.NEDELKOV@GMAIL.COM",
                FirstName = "Stanislav",
                LastName = "Nedelkov",
                IsActive = true,
            };
            userModel.PasswordHash = hasher.HashPassword("Parola1!");

            await context.Users.AddAsync(userModel);
            await context.SaveChangesAsync();

            string userId = await context
                .Users
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            var pet = new Pet()
            {
                Name = petName,
                UserID = userId,
                PetTypeID = 1,
                HotelID = 1,
                Alergies = "none",
                Age = 5
            };
            var reservation = new Reservation()
            {
                HotelID = 1,
                PetID = 1,
                PetName = petName,
                Status = GlobalConstants.ExpectedStatus,
                AdmissionDate = DateTime.Now,
                DepartureDate = DateTime.Now,
            };
            await context.Pets.AddAsync(pet);
            await context.Schedules.AddAsync(reservation);
            await context.SaveChangesAsync();

            bool result = await hotelService.IsGuestOwnedByUser(fakeId, userId);

            Assert.That(result, Is.False);


        }
    }
}