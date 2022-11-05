using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PetHotel.Infrastructure.Data.Entities;
using PetHotel.Infrastructure.Data;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.UserModels;

namespace PetHotel.Core.Services
{
    public class UserService : IUserService
    {
        //reqiured for creating users
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly PetHotelDbContext context;

        public UserService(UserManager<User> _userManager, 
            SignInManager<User> _signInManager, 
            PetHotelDbContext _context)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
            this.context = _context;
        }

        public async Task RegisterNewUser(RegisterViewModel model)
        {

            var user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                PhoneNumber = model.PhoneNumber,
                LastName = model.LastName,

            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new ArgumentException();
            }
        }
    }
}
