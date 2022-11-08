using Microsoft.EntityFrameworkCore;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.AdminModels;
using PetHotel.Core.Models.PetModels;
using PetHotel.Core.Models.UserModels;
using PetHotel.Infrastructure.Data;
using PetHotel.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly PetHotelDbContext context;
        public AdminService(PetHotelDbContext _context)
        {
            this.context = _context;
        }
        public async Task<ICollection<ProfileViewModel>> GetAllUsersAsync()
        {
            var users = await context
                .Users
                .Include(x => x.Pets)
                .ToListAsync();

            if (users == null)
            {
                throw new ArgumentNullException();
            }

            var usersDto = new List<ProfileViewModel>();

            foreach (var item in users)
            {
                var profile = new ProfileViewModel() 
                {
                    UserName = item.UserName,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber
                };

                var petList = new List<PetViewModel>();

                foreach (var pet in item.Pets)
                {
                    var petDto = new PetViewModel()
                    {
                        Name = pet.Name,
                        Age = pet.Age,
                        Id = pet.Id,
                        Alergies = pet.Alergies,
                        PetTypeID = pet.PetTypeID,
                        UserID = pet.UserID,
                    };
                    petList.Add(petDto);
                }

                profile.Pets = petList;
                usersDto.Add(profile);
            }

            return usersDto;
        }
    }
}
