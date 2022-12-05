using PetHotel.Core.Models.HotelModels;
using PetHotel.Core.Models.PetModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Contracts
{
    public interface IHotelService
    {
        Task<ICollection<GuestBasicViewModel>> GetAllGuestsAsync();
        Task<PetViewModel> GuestDetailsAsync(int id);
        Task<GuestOwnerViewModel> GetGuestOwnerAsync(int id);

        Task AddGuestAsync(AddGuestViewModel model);
        Task<AddGuestViewModel> GetGuestToAddAsync(int id);

        Task<ICollection<GuestBasicViewModel>> GetMyAllGuestsAsync(string userId);

        Task CancelHotelStayAsync(int bookedId);

        Task PickUpearly(int bookedId, string newCollectionDate);
    

    }
}
