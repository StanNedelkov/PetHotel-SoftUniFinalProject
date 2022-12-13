using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using PetHotel.Common;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.PetModels;
using PetHotel.Core.Models.UserModels;
using PetHotel.Infrastructure.Data;
using PetHotel.Infrastructure.Data.Entities;
using System.Security.Claims;

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

        public async Task<ICollection<MyOwnPetsViewModel>> GetMyPetsAsync(string userId)
        {
            return await context
                .Pets
                .Where(x => x.UserID == userId)
                .Select(x => new MyOwnPetsViewModel()
                {
                    Id = x.Id,
                    Type = x.PetType.Name,
                    Name = x.Name

                }).ToListAsync();
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
          
            var petsInDB = await context
                .Pets
                .Where(x => x.UserID == userId)
                .Select(x => new PetViewModel()
                {
                    Id = x.Id,
                    PetType = x.PetType.Name,
                    Age = x.Age,
                    Name = x.Name,
                    Alergies = x.Alergies
                }).ToListAsync();

            userProfile.Pets = petsInDB;
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

        public async Task<bool> UserOwnsPet(string userId, int petId)
            => await context
                .Pets
                .Where(x => x.Id == petId)
                .AsNoTracking()
                .AnyAsync(x => x.UserID == userId);
        
    }
}
