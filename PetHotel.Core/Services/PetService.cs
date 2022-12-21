using Microsoft.EntityFrameworkCore;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.PetModels;
using PetHotel.Infrastructure.Data;
using PetHotel.Infrastructure.Data.Entities;

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
          
            await context.Pets.AddAsync(pet);
            await context.SaveChangesAsync();
        }

       
        public async Task DeletePetAsync(int Id)
        {
            var petToDelete = await context
                .Pets
                .FirstOrDefaultAsync(x => x.Id == Id);
            if (petToDelete == null) throw new ArgumentNullException();
            
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
