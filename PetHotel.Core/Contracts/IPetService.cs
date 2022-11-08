using PetHotel.Core.Models.PetModels;

namespace PetHotel.Core.Contracts
{
    public interface IPetService
    {
        Task<IEnumerable<PetTypeViewModel>> GetAllPetTypesAsync();
        Task AddPetAsync(CreatePetViewModel model, string userId);

        Task DeletePetAsync(int Id);
    }
}
