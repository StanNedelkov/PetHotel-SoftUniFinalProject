using PetHotel.Core.Models.EmployeeModels;
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

        Task<IEnumerable<GuestDetailedViewModel>> GetDeparturesTodayAsync();

        TasksCountViewModel Counter();

        Task<IEnumerable<GuestDetailedViewModel>> GetAllAsync();

        Task<IEnumerable<GuestDetailedViewModel>> GetOverdueAsync();

        Task<IEnumerable<GuestDetailedViewModel>> GetAllInHotelAsync();


    }
}
