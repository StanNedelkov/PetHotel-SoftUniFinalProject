using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using PetHotel.Common;
using PetHotel.Core.Contracts;
using PetHotel.Core.Services;
using PetHotel.Infrastructure.Data;
using PetHotel.Infrastructure.Data.Entities;

namespace PetHotel.UnitTests
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        private PetHotelDbContext context;
        private IEmployeeService employeeService;
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
        public async Task TestGetAllExpectedGuestEmtyResult()
        {
            employeeService = new EmployeeService(context);

           
                var modelList = await employeeService.GetAllAsync();

            Assert.That(modelList, Is.Empty);
            
        }

        [Test]
        public async Task TestGetAllExpectedGuest()
        {
            employeeService = new EmployeeService(context);




            var user = new User()
            {
                Email = "stenly.nedelkov@gmail.com",
                UserName = "stan",
                FirstName = "stan",
                LastName = "stan",
                IsActive = true,

            };
            var hasher = new PasswordHasher();
            user.PasswordHash = hasher.HashPassword("Parola1!");

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();


            var resultUser = await context.Users.FirstOrDefaultAsync();

            var pet = new Pet()
            {
                Name = "jack",
                UserID = resultUser.Id,
                PetTypeID = 1,
                HotelID = 1,
                Alergies = "none"
            };

            //test method needs also a pet type
            var type = new PetType()
            {
                Name = "Test"
            };


            var hotel = new Hotel()
            {
                Name = "Test",

            };

            var reservation = new Reservation()
            {
                AdmissionDate = DateTime.Now,
                DepartureDate = DateTime.Now,
                HotelID = 1,
                PetID = 1,
                PetName = "Test",
                Status = GlobalConstants.ExpectedStatus
            };

            await context.Pets.AddAsync(pet);
            await context.PetTypes.AddAsync(type);
            await context.Schedules.AddAsync(reservation);
            await context.Hotels.AddAsync(hotel);
            await context.SaveChangesAsync();


            var modelList = await employeeService.GetAllAsync();

            Assert.That(modelList.Count, Is.EqualTo(1));

        }


        [Test]
        public async Task TestGetAllExpectedGuestWithExceptionError()
        {
            employeeService = new EmployeeService(context);




            var user = new User()
            {
                Email = "stenly.nedelkov@gmail.com",
                UserName = "stan",
                FirstName = "stan",
                LastName = "stan",
                IsActive = true,

            };
            var hasher = new PasswordHasher();
            user.PasswordHash = hasher.HashPassword("Parola1!");

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();


            var resultUser = await context.Users.FirstOrDefaultAsync();

           

            //test method needs also a pet type
            var type = new PetType()
            {
                Name = "Test"
            };


            var hotel = new Hotel()
            {
                Name = "Test",

            };

            var reservation = new Reservation()
            {
                AdmissionDate = DateTime.Now,
                DepartureDate = DateTime.Now,
                HotelID = 1,
                PetID = 1,
                PetName = "Test",
                Status = GlobalConstants.ExpectedStatus
            };

            
            await context.PetTypes.AddAsync(type);
            await context.Schedules.AddAsync(reservation);
            await context.Hotels.AddAsync(hotel);
            await context.SaveChangesAsync();


            try
            {
                var modelList = await employeeService.GetAllAsync();
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {

                Assert.Pass();
            }
            
        }



        [Test]
        public async Task TestGetAllOverdueGuest()
        {
            employeeService = new EmployeeService(context);




            var user = new User()
            {
                Email = "stenly.nedelkov@gmail.com",
                UserName = "stan",
                FirstName = "stan",
                LastName = "stan",
                IsActive = true,

            };
            var hasher = new PasswordHasher();
            user.PasswordHash = hasher.HashPassword("Parola1!");

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();


            var resultUser = await context.Users.FirstOrDefaultAsync();

            var pet = new Pet()
            {
                Name = "jack",
                UserID = resultUser.Id,
                PetTypeID = 1,
                HotelID = 1,
                Alergies = "none"
            };

            //test method needs also a pet type
            var type = new PetType()
            {
                Name = "Test"
            };


            var hotel = new Hotel()
            {
                Name = "Test",

            };

            var reservation = new Reservation()
            {
                AdmissionDate = DateTime.Now,
                DepartureDate = DateTime.Now,
                HotelID = 1,
                PetID = 1,
                PetName = "Test",
                Status = GlobalConstants.ExpectedStatus
            };

            await context.Pets.AddAsync(pet);
            await context.PetTypes.AddAsync(type);
            await context.Schedules.AddAsync(reservation);
            await context.Hotels.AddAsync(hotel);
            await context.SaveChangesAsync();


            var modelList = await employeeService.GetOverdueAsync();

            Assert.That(modelList.Count, Is.EqualTo(0));

        }

        [Test]
        public async Task TestGetOverdueExpectedGuestWithEmptyResult()
        {
            employeeService = new EmployeeService(context);

            
                var modelList = await employeeService.GetOverdueAsync();
                Assert.That(modelList, Is.Empty);
        }

        [Test]
        public async Task TestGetDeparturesGuestWithEmptyResult()
        {
            employeeService = new EmployeeService(context);


            var modelList = await employeeService.GetDeparturesTodayAsync();
            Assert.That(modelList, Is.Empty);
        }

        [Test]
        public async Task TestGetDeparturesGuest()
        {
            employeeService = new EmployeeService(context);




            var user = new User()
            {
                Email = "stenly.nedelkov@gmail.com",
                UserName = "stan",
                FirstName = "stan",
                LastName = "stan",
                IsActive = true,

            };
            var hasher = new PasswordHasher();
            user.PasswordHash = hasher.HashPassword("Parola1!");

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();


            var resultUser = await context.Users.FirstOrDefaultAsync();

            var pet = new Pet()
            {
                Name = "jack",
                UserID = resultUser.Id,
                PetTypeID = 1,
                HotelID = 1,
                Alergies = "none"
            };

            //test method needs also a pet type
            var type = new PetType()
            {
                Name = "Test"
            };


            var hotel = new Hotel()
            {
                Name = "Test",

            };

            var reservation = new Reservation()
            {
                AdmissionDate = DateTime.Now,
                DepartureDate = DateTime.Now,
                HotelID = 1,
                PetID = 1,
                PetName = "Test",
                Status = GlobalConstants.InProgressStatus
            };

            await context.Pets.AddAsync(pet);
            await context.PetTypes.AddAsync(type);
            await context.Schedules.AddAsync(reservation);
            await context.Hotels.AddAsync(hotel);
            await context.SaveChangesAsync();


            var modelList = await employeeService.GetDeparturesTodayAsync();

            Assert.That(modelList.Count, Is.EqualTo(1));

        }


        [Test]
        public async Task TestReservationGuestModel()
        {
            employeeService = new EmployeeService(context);




            var user = new User()
            {
                Email = "stenly.nedelkov@gmail.com",
                UserName = "stan",
                FirstName = "stan",
                LastName = "stan",
                IsActive = true,

            };
            var hasher = new PasswordHasher();
            user.PasswordHash = hasher.HashPassword("Parola1!");

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();


            var resultUser = await context.Users.FirstOrDefaultAsync();

            var pet = new Pet()
            {
                Name = "jack",
                UserID = resultUser.Id,
                PetTypeID = 1,
                HotelID = 1,
                Alergies = "none"
            };

            //test method needs also a pet type
            var type = new PetType()
            {
                Name = "Test"
            };


            var hotel = new Hotel()
            {
                Name = "Test",

            };

            var reservation = new Reservation()
            {
                AdmissionDate = DateTime.Now,
                DepartureDate = DateTime.Now,
                HotelID = 1,
                PetID = 1,
                PetName = "Test",
                Status = GlobalConstants.InProgressStatus
            };

            await context.Pets.AddAsync(pet);
            await context.PetTypes.AddAsync(type);
            await context.Schedules.AddAsync(reservation);
            await context.Hotels.AddAsync(hotel);
            await context.SaveChangesAsync();


            var model = await employeeService.GetReservationAsync(1);

            Assert.That(model.PetId, Is.EqualTo(1));
        }

        [Test]
        public async Task TestReservationGuestModelErrorException()
        {
            employeeService = new EmployeeService(context);




            var user = new User()
            {
                Email = "stenly.nedelkov@gmail.com",
                UserName = "stan",
                FirstName = "stan",
                LastName = "stan",
                IsActive = true,

            };
            var hasher = new PasswordHasher();
            user.PasswordHash = hasher.HashPassword("Parola1!");

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();


            var resultUser = await context.Users.FirstOrDefaultAsync();

            var pet = new Pet()
            {
                Name = "jack",
                UserID = resultUser.Id,
                PetTypeID = 1,
                HotelID = 1,
                Alergies = "none"
            };

            //test method needs also a pet type
            var type = new PetType()
            {
                Name = "Test"
            };


            var hotel = new Hotel()
            {
                Name = "Test",

            };

            

            await context.Pets.AddAsync(pet);
            await context.PetTypes.AddAsync(type);
            await context.Hotels.AddAsync(hotel);
            await context.SaveChangesAsync();


           
            try
            {
                var model = await employeeService.GetReservationAsync(1);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {

               Assert.Pass();
            }
            
        }


        [Test]

        public void TestStatusList()
        {
            employeeService = new EmployeeService(context);
            var list =  employeeService.GetStatusList();

            Assert.IsNotNull(list);
            Assert.That(list.Count, Is.EqualTo(4));
        }


        [Test]
        public async Task TestEditReservationStatus()
        {
            employeeService = new EmployeeService(context);




            var user = new User()
            {
                Email = "stenly.nedelkov@gmail.com",
                UserName = "stan",
                FirstName = "stan",
                LastName = "stan",
                IsActive = true,

            };
            var hasher = new PasswordHasher();
            user.PasswordHash = hasher.HashPassword("Parola1!");

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();


            var resultUser = await context.Users.FirstOrDefaultAsync();

            var pet = new Pet()
            {
                Name = "jack",
                UserID = resultUser.Id,
                PetTypeID = 1,
                HotelID = 1,
                Alergies = "none"
            };

            //test method needs also a pet type
            var type = new PetType()
            {
                Name = "Test"
            };


            var hotel = new Hotel()
            {
                Name = "Test",

            };

            var reservation = new Reservation()
            {
                AdmissionDate = DateTime.Now,
                DepartureDate = DateTime.Now,
                HotelID = 1,
                PetID = 1,
                PetName = "Test",
                Status = GlobalConstants.InProgressStatus
            };

            await context.Pets.AddAsync(pet);
            await context.PetTypes.AddAsync(type);
            await context.Schedules.AddAsync(reservation);
            await context.Hotels.AddAsync(hotel);
            await context.SaveChangesAsync();


            await employeeService.ManageStatusAsync(1, "testStatus");

            var guest = await context.Schedules.FirstOrDefaultAsync();

            Assert.That(guest.Status, Is.EqualTo("testStatus"));
        }



        [Test]
        public async Task TestEditReservationStatusExceptionMessage()
        {
            employeeService = new EmployeeService(context);

            var user = new User()
            {
                Email = "stenly.nedelkov@gmail.com",
                UserName = "stan",
                FirstName = "stan",
                LastName = "stan",
                IsActive = true,

            };
            var hasher = new PasswordHasher();
            user.PasswordHash = hasher.HashPassword("Parola1!");

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();


            var resultUser = await context.Users.FirstOrDefaultAsync();

            var pet = new Pet()
            {
                Name = "jack",
                UserID = resultUser.Id,
                PetTypeID = 1,
                HotelID = 1,
                Alergies = "none"
            };

            //test method needs also a pet type
            var type = new PetType()
            {
                Name = "Test"
            };


            var hotel = new Hotel()
            {
                Name = "Test",

            };

            var reservation = new Reservation()
            {
                AdmissionDate = DateTime.Now,
                DepartureDate = DateTime.Now,
                HotelID = 1,
                PetID = 1,
                PetName = "Test",
                Status = GlobalConstants.InProgressStatus
            };

            await context.Pets.AddAsync(pet);
            await context.PetTypes.AddAsync(type);
            await context.Schedules.AddAsync(reservation);
            await context.Hotels.AddAsync(hotel);
            await context.SaveChangesAsync();

            int fakeId = 2;

           
            
           
            try
            {
                await employeeService.ManageStatusAsync(77, "testStatus");
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Pass();
            }
            
        }

        [Test]
        public void TestVerifyStatus()
        {
            employeeService = new EmployeeService(context);

            bool result = employeeService.VerifyStatusAsync(GlobalConstants.ExpectedStatus);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task TestGetAllInHotelGuest()
        {
            employeeService = new EmployeeService(context);




            var user = new User()
            {
                Email = "stenly.nedelkov@gmail.com",
                UserName = "stan",
                FirstName = "stan",
                LastName = "stan",
                IsActive = true,

            };
            var hasher = new PasswordHasher();
            user.PasswordHash = hasher.HashPassword("Parola1!");

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();


            var resultUser = await context.Users.FirstOrDefaultAsync();

            var pet = new Pet()
            {
                Name = "jack",
                UserID = resultUser.Id,
                PetTypeID = 1,
                HotelID = 1,
                Alergies = "none"
            };

            //test method needs also a pet type
            var type = new PetType()
            {
                Name = "Test"
            };


            var hotel = new Hotel()
            {
                Name = "Test",

            };

            var reservation = new Reservation()
            {
                AdmissionDate = DateTime.Now,
                DepartureDate = DateTime.Now,
                HotelID = 1,
                PetID = 1,
                PetName = "Test",
                Status = GlobalConstants.InProgressStatus
            };

            await context.Pets.AddAsync(pet);
            await context.PetTypes.AddAsync(type);
            await context.Schedules.AddAsync(reservation);
            await context.Hotels.AddAsync(hotel);
            await context.SaveChangesAsync();


            var modelList = await employeeService.GetAllInHotelAsync();

            Assert.That(modelList.Count, Is.EqualTo(1));

        }

        [Test]
        public async Task TestGetAllInHotelGuestEmptyResult()
        {
            employeeService = new EmployeeService(context);




            var user = new User()
            {
                Email = "stenly.nedelkov@gmail.com",
                UserName = "stan",
                FirstName = "stan",
                LastName = "stan",
                IsActive = true,

            };
            var hasher = new PasswordHasher();
            user.PasswordHash = hasher.HashPassword("Parola1!");

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();


            var resultUser = await context.Users.FirstOrDefaultAsync();

            var pet = new Pet()
            {
                Name = "jack",
                UserID = resultUser.Id,
                PetTypeID = 1,
                HotelID = 1,
                Alergies = "none"
            };

            //test method needs also a pet type
            var type = new PetType()
            {
                Name = "Test"
            };


            var hotel = new Hotel()
            {
                Name = "Test",

            };

            var reservation = new Reservation()
            {
                AdmissionDate = DateTime.Now,
                DepartureDate = DateTime.Now,
                HotelID = 1,
                PetID = 1,
                PetName = "Test",
                Status = GlobalConstants.ExpectedStatus
            };

            await context.Pets.AddAsync(pet);
            await context.PetTypes.AddAsync(type);
            await context.Schedules.AddAsync(reservation);
            await context.Hotels.AddAsync(hotel);
            await context.SaveChangesAsync();


            var modelList = await employeeService.GetAllInHotelAsync();

            Assert.That(modelList, Is.Empty);

        }

        [Test]
        public async Task TestGetAllInHotelGuestException()
        {
            employeeService = new EmployeeService(context);




            var user = new User()
            {
                Email = "stenly.nedelkov@gmail.com",
                UserName = "stan",
                FirstName = "stan",
                LastName = "stan",
                IsActive = true,

            };
            var hasher = new PasswordHasher();
            user.PasswordHash = hasher.HashPassword("Parola1!");

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();


            var resultUser = await context.Users.FirstOrDefaultAsync();

           

            //test method needs also a pet type
            var type = new PetType()
            {
                Name = "Test"
            };


            var hotel = new Hotel()
            {
                Name = "Test",

            };

            var reservation = new Reservation()
            {
                AdmissionDate = DateTime.Now,
                DepartureDate = DateTime.Now,
                HotelID = 1,
                PetID = 1,
                PetName = "Test",
                Status = GlobalConstants.InProgressStatus
            };

            
            await context.PetTypes.AddAsync(type);
            await context.Schedules.AddAsync(reservation);
            await context.Hotels.AddAsync(hotel);
            await context.SaveChangesAsync();


            

            try
            {
                var modelList = await employeeService.GetAllInHotelAsync();
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {

                Assert.Pass();
            }

        }
    }
}
