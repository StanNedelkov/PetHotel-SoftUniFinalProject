using PetHotel.Core.Models.AdminModels;
using PetHotel.Core.Models.UserModels;

namespace PetHotel.Core.Contracts
{
    public interface IAdminService
    {
        Task <ICollection<ProfileViewModel>> GetAllUsersAsync();
    }
}
