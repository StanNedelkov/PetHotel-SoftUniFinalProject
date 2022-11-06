using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using System.Linq;

using PetHotel.Common;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.UserModels;
using PetHotel.Infrastructure.Data;
using PetHotel.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using PetHotel.Core.Models.PetModels;

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

        public async Task<ProfileViewModel> GetProfileAsync(string userId)
        {
            var user = await context
                .Users
                .FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new ArgumentNullException(ErrorMessagesConstants.userNotFound);
            }
            //todo password length without hash in *
            /*StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ; i++)
            {

            }*/
            var userProfile = new ProfileViewModel()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                PasswordHidden = "******"

            };
            List <PetViewModel> pets = new List<PetViewModel> ();
            foreach (var pet in user.Pets)
            {
                var userPet = new PetViewModel() 
                {
                    Id = pet.Id,
                    PetType = pet.PetType.Name,
                    Age = pet.Age,
                    Name = pet.Name,
                    Alergies = pet.Alergies
                };
                pets.Add(userPet);
            }

            userProfile.Pets = pets;
            return userProfile;
        }

        public async Task LoginUserAsync(LoginViewModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                throw new ArgumentException(ErrorMessagesConstants.usernameOrPasswordInvalid);
            }
            var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!result.Succeeded)
            {
                throw new ArgumentException(ErrorMessagesConstants.usernameOrPasswordInvalid);
            }
        }

        public async Task RegisterNewUserAsync(RegisterViewModel model)
        {
            var user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                PhoneNumber = model.PhoneNumber,
                LastName = model.LastName

            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new ArgumentException(ErrorMessagesConstants.registrationFailed);
            }

            var role = await userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);

            if (!role.Succeeded)
            {
                throw new ArgumentException(ErrorMessagesConstants.roleFailed);
            }
        }

        public async Task SignOutUserAsync()
            => await signInManager.SignOutAsync();
        
        
    }
}
