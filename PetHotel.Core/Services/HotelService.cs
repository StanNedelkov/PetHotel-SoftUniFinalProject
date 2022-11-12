using Microsoft.EntityFrameworkCore;
using PetHotel.Common;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.HotelModels;
using PetHotel.Core.Models.PetModels;
using PetHotel.Infrastructure.Data;
using PetHotel.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Services
{
    public class HotelService : IHotelService
    {
        private readonly PetHotelDbContext context;
        public HotelService(PetHotelDbContext _context)
        {
            this.context = _context;
        }

        public async Task AddGuestAsync(AddGuestViewModel model)
        {
            DateTime admissionDate;
            bool isAdmissonDate = DateTime
                .TryParseExact(model.CheckInDate,
                GlobalConstants.DateTimeFormatConst,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out admissionDate);

            if (!isAdmissonDate) throw new ArgumentException();
            

            DateTime departureDate;
            bool isDeparture = DateTime
                .TryParseExact(model.CheckOutDate,
                GlobalConstants.DateTimeFormatConst,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out departureDate);

            if (!isDeparture) throw new ArgumentException();

            var schedule = new Schedule()
            {
               AdmissionDate = admissionDate,
               DepartureDate = departureDate,
               HotelID = 1,
               PetID = model.Id,
               PetName = model.Name
            };

            await context.Schedules.AddAsync(schedule);
            await context.SaveChangesAsync();   

          
        }

        public Task<ICollection<GuestBasicViewModel>> GetAllGuestsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GuestOwnerViewModel> GetGuestOwnerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AddGuestViewModel> GetGustToAddAsync(int id)
        {
            var pet = await context
                .Pets
                .Include(x => x.PetType)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (pet == null)
            {
                throw new ArgumentNullException();
            }

            return new AddGuestViewModel()
            {
                Id = id,
                Name = pet.Name,
                CheckInDate = DateTime.Now.ToString(GlobalConstants.DateTimeFormatConst),
                CheckOutDate = DateTime.Now.ToString(GlobalConstants.DateTimeFormatConst),
            };
        }

        public Task<PetViewModel> GuestDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
