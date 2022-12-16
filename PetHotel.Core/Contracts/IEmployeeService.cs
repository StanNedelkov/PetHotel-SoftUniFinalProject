using PetHotel.Core.Models.HotelModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Contracts
{
    public interface IEmployeeService
    {
        Task ManageStatusAsync(int reservationId, string newStatus);

        bool VerifyStatusAsync(string newStatus);

        IEnumerable<string> GetStatusList();

        Task<GuestDetailedViewModel> GetReservationAsync(int reservationId);

        
    }
}
