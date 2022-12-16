using PetHotel.Core.Models.UserModels;
using PetHotel.Infrastructure.Data.Entities;

namespace PetHotel.Core.Contracts
{
    public interface IUserService
    {
        Task RegisterNewUserAsync(RegisterViewModel model);

        Task LoginUserAsync(LoginViewModel model);

        Task SignOutUserAsync();

        Task<ProfileViewModel> GetProfileAsync(string userId);

        Task<bool> UserOwnsPet(string userId, int petId);
        Task<ICollection<MyOwnPetsViewModel>>GetMyPetsAsync(string userId);

        Task<User> EmployeeUser(string userName);
    }
}
