using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.PetModels;
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
    public class PetServiceTests
    {
        private PetHotelDbContext context;
        private IPetService petService;
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
        public async Task TestAddPetToUser()
        {
            petService = new PetService(context);
            var model = new CreatePetViewModel()
            {
                Age = 1,
                Alergies = "something",
                Name = "Demon",
                PetTypeID = 1,
            };

            string userID = "3cf9137f-a0fe-4da0-9cf6-798c97639ade";

            await petService.AddPetAsync(model, userID);

            var resultModel = await context
                .Pets
                .FirstOrDefaultAsync(x => x.UserID == userID);

            Assert.IsNotNull(resultModel);
            Assert.That(resultModel.Name, Is.EqualTo("Demon"));

        }

        [Test]
        public async Task TestDeletePetFromUser() 
        {
            petService = new PetService(context);
            int deleteId = 2;
            var model = new CreatePetViewModel()
            {
                Age = 1,
                Alergies = "something",
                Name = "Demon",
                PetTypeID = 1,
            };

            var modelToDelete = new CreatePetViewModel()
            {
                Age = 1,
                Alergies = "something",
                Name = "Demon",
                PetTypeID = 1,
            };

            string userID = "3cf9137f-a0fe-4da0-9cf6-798c97639ade";

            await petService.AddPetAsync(model, userID);
            await petService.AddPetAsync(modelToDelete, userID);

            await petService.DeletePetAsync(deleteId); 
            var all = await context
                .Pets
                .ToListAsync();

            Assert.That(all.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task TestDeletePetFromUserNullException()
        {
            petService = new PetService(context);
            int deleteId = 1;
            int fakeId = 2;
            

            var modelToDelete = new CreatePetViewModel()
            {
                Age = 1,
                Alergies = "something",
                Name = "Demon",
                PetTypeID = 1,
            };

            string userID = "3cf9137f-a0fe-4da0-9cf6-798c97639ade";

           
            await petService.AddPetAsync(modelToDelete, userID);

            try
            {
                await petService.DeletePetAsync(fakeId);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
                Assert.Pass();
            }
           
        }

        [Test]
        public async Task TestEditPet()
        {
            petService = new PetService(context);
            int petId = 1;

            var model = new CreatePetViewModel()
            {
                Age = 1,
                Alergies = "something",
                Name = "Demon",
                PetTypeID = 1,
            };

            string userID = "3cf9137f-a0fe-4da0-9cf6-798c97639ade";


            await petService.AddPetAsync(model, userID);

            var editModel = new CreatePetViewModel()
            {
                Id= petId,
                Age = 2,
                Alergies = "none",
                Name = "Caraxes",
                PetTypeID = 1,
            };

            await petService.EditPetAsync(editModel);

            var resultModel = await context.Pets.FirstOrDefaultAsync();

            Assert.IsNotNull(resultModel);
            Assert.That(resultModel.Name, Is.EqualTo("Caraxes"));
        }

        [Test]
        public async Task TestPetTypes()
        {
            petService = new PetService(context);

            await context.PetTypes.AddAsync(new PetType() { Name = "Dragon", CostPerDay = 999 });
            await context.SaveChangesAsync();

            var listOfTypes = await petService.GetAllPetTypesAsync();

            Assert.That(listOfTypes.Count(), Is.EqualTo(1));
        }

        [Test]

        public async Task TestGetPetModelById()
        {
            petService = new PetService(context);
            int petId = 1;

            var model = new CreatePetViewModel()
            {
                Age = 1,
                Alergies = "something",
                Name = "Demon",
                PetTypeID = 1,
            };

            var modelTwo = new CreatePetViewModel()
            {
                Age = 3,
                Alergies = "none",
                Name = "Caraxes",
                PetTypeID = 1,
            };

            string userID = "3cf9137f-a0fe-4da0-9cf6-798c97639ade";

            await context.PetTypes.AddAsync(new PetType() { Name = "Dragon", CostPerDay = 999 });
            await context.SaveChangesAsync();

            await petService.AddPetAsync(model, userID);
            await petService.AddPetAsync(modelTwo, userID);

            var resultModel = await petService.GetPetAsync(petId);
            var resultModelTwo = await petService.GetPetAsync(2);

            Assert.That(resultModel.Id, Is.EqualTo(1));
            Assert.That(resultModelTwo.Id, Is.EqualTo(2));
        }

        [Test]

        public async Task TestGetPetModelByIdNullException()
        {
            petService = new PetService(context);
            int petId = 1;
            int fakeId = 3;

            var model = new CreatePetViewModel()
            {
                Age = 1,
                Alergies = "something",
                Name = "Demon",
                PetTypeID = 1,
            };

            var modelTwo = new CreatePetViewModel()
            {
                Age = 3,
                Alergies = "none",
                Name = "Caraxes",
                PetTypeID = 1,
            };

            string userID = "3cf9137f-a0fe-4da0-9cf6-798c97639ade";

            await context.PetTypes.AddAsync(new PetType() { Name = "Dragon", CostPerDay = 999 });
            await context.SaveChangesAsync();

            await petService.AddPetAsync(model, userID);
            await petService.AddPetAsync(modelTwo, userID);

            try
            {
                var resultModel = await petService.GetPetAsync(fakeId);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {

                Assert.Pass();
            }
            
           

            
        }
    }
}
