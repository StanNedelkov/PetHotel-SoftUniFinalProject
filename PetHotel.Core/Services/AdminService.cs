﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.PetModels;
using PetHotel.Core.Models.UserModels;
using PetHotel.Infrastructure.Data;
using PetHotel.Infrastructure.Data.Entities;

namespace PetHotel.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly PetHotelDbContext context;
        private readonly UserManager<User> userManager;
        public AdminService(PetHotelDbContext _context, UserManager<User> _userManager)
        {
            this.context = _context;
            this.userManager = _userManager;
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
               string? role = userManager.GetRolesAsync(item).Result.FirstOrDefault();

                var profile = new ProfileViewModel()
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                    Role = role
                    
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
