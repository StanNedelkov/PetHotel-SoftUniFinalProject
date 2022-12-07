using PetHotel.Core.Models.UserModels;

namespace PetHotel.Core.Contracts
{
    public interface IUserService
    {
        Task RegisterNewUserAsync(RegisterViewModel model);

        Task LoginUserAsync(LoginViewModel model);

        Task SignOutUserAsync();

        Task<ProfileViewModel> GetProfileAsync(string userId);

        Task<bool> UserOwnsPet(string userId, int petId);
    }
}
