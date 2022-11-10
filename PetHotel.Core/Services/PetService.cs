using Microsoft.EntityFrameworkCore;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.PetModels;
using PetHotel.Infrastructure.Data;
using PetHotel.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Services
{
    public class PetService : IPetService
    {
        private readonly PetHotelDbContext context;
        public PetService(PetHotelDbContext _context)
        {
            this.context = _context;
        }

        public async Task AddPetAsync(CreatePetViewModel model, string userId)
        {
            var pet = new Pet()
            {
                Name = model.Name,
                PetTypeID = model.PetTypeID,
                Age = model.Age,
                Alergies = model.Alergies,
                UserID = userId,
                HotelID = 1
            };
            //to do check if null
            var capacity = await context.Capacities.FirstOrDefaultAsync(x => x.HotelID == 1);
            //to do constants or better way to get the Ids than just numbers!!!!
            if (pet.PetTypeID == 1) capacity.CurCapacityDogs++;
            if (pet.PetTypeID == 2) capacity.CurCapacityCats++;
            if (pet.PetTypeID == 3) capacity.CurCapacityGator++;
            await context.Pets.AddAsync(pet);
            await context.SaveChangesAsync();
        }

        //TODO change delete with bool is active, edit all entities
        public async Task DeletePetAsync(int Id)
        {
            var petToDelete = await context
                .Pets
                .FirstOrDefaultAsync(x => x.Id == Id);
            if (petToDelete == null)
            {
                //do something that makse sence
                throw new ArgumentNullException();
            }
            context.Pets.Remove(petToDelete);
            await context.SaveChangesAsync();

           
        }

        public async Task EditPetAsync(CreatePetViewModel model)
        {
            var pet = await context
                .Pets
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            if (pet == null)
            {
                throw new ArgumentNullException();
            }
            pet.Age = model.Age;
            pet.Alergies = model.Alergies;
            pet.Name = model.Name;
            pet.PetTypeID = model.PetTypeID;

            await context.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<PetTypeViewModel>> GetAllPetTypesAsync()
        {
            var petTypes = await context
                .PetTypes
                .Select(x => new PetTypeViewModel() { Id = x.Id, Name = x.Name})
                .ToListAsync();

            return petTypes;
        }

        public async Task<CreatePetViewModel> GetPetAsync(int Id)
        {
            var pet = await context
               .Pets
               .Include(x => x.PetType)
               .FirstOrDefaultAsync(x => x.Id == Id);
            if (pet == null)
            {
                throw new ArgumentNullException();
            }

            return new CreatePetViewModel()
            {
                Id = pet.Id,
                Name = pet.Name,
                Age = pet.Age,
                Alergies = pet.Alergies,
                PetTypeID = pet.PetTypeID
            };
        }
    }
}
