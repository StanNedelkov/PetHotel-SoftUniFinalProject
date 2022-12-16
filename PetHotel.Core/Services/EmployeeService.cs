using Microsoft.EntityFrameworkCore;
using PetHotel.Common;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.HotelModels;
using PetHotel.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly PetHotelDbContext context;
        public EmployeeService(PetHotelDbContext _context)
        {
            this.context = _context;
        }

        public async Task<GuestDetailedViewModel> GetReservationAsync(int reservationId)
        {
            var reservation = await context.Schedules
                .Where(x => x.Id == reservationId)
                .AsNoTracking()
                .Select(x => new GuestDetailedViewModel()
                {
                    ReservationId = x.Id,
                    CheckInDate = x.AdmissionDate.ToString("f"),
                    CheckOutDate = x.DepartureDate.ToString("f"),
                    PetId = x.PetID,
                    PetName= x.PetName,
                    Status = x.Status,
                     
                }).FirstOrDefaultAsync();

            string? type = await context
                .Pets
                .Include(x => x.PetType)
                .AsNoTracking()
                .Where(x => x.Id == reservation!.PetId)
                .Select(x => x.PetType.Name)
                .FirstOrDefaultAsync();

            reservation!.PetType = type!;

            var userName = await context
                .Pets
                .Include(x => x.User)
                .AsNoTracking()
                .Where(x => x.Id == reservation!.PetId)
                .Select(x => x.User.UserName)
                .FirstOrDefaultAsync();

            reservation.UserName = userName!;

            var userId = await context
               .Pets
               .Include(x => x.User)
               .AsNoTracking()
               .Where(x => x.Id == reservation!.PetId)
               .Select(x => x.User.Id)
               .FirstOrDefaultAsync();

            reservation.UserId = userId!;

           return reservation;
        }

        public IEnumerable<string> GetStatusList()
        {
           var list = new List<string>() 
           {
               GlobalConstants.InProgressStatus, 
               GlobalConstants.ExpectedStatus ,
               GlobalConstants.CanceledStatus,
               GlobalConstants.CompletedStatus
           };

            return list;
        }

        public async Task ManageStatusAsync(int reservationId, string newStatus)
        {
            var reservation = await context.Schedules
                .FirstOrDefaultAsync(x => x.Id == reservationId);

            if (reservation == null)  throw new ArgumentNullException(reservation!.ToString()); 


            reservation.Status = newStatus;
            await context.SaveChangesAsync();
           
        }

        public bool VerifyStatusAsync(string newStatus)
         => newStatus.ToUpper() == GlobalConstants.InProgressStatus.ToUpper() ||
            newStatus.ToUpper() == GlobalConstants.ExpectedStatus.ToUpper(); /*||
            newStatus.ToUpper() == GlobalConstants.CanceledStatus.ToUpper() ||
            newStatus.ToUpper() == GlobalConstants.CompletedStatus.ToUpper();*/
    }
}
