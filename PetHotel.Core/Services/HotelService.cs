using Microsoft.EntityFrameworkCore;
using PetHotel.Common;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.HotelModels;
using PetHotel.Core.Models.PetModels;
using PetHotel.Infrastructure.Data;
using PetHotel.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        /// <summary>
        /// List of all pets in the hotel.
        /// To be used by the employee account.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>

        public async Task<ICollection<GuestBasicViewModel>> GetAllGuestsAsync()
        {

            List<int> guests = await context
                .Schedules
                .Select(x => x.PetID)
                .ToListAsync();

            if (guests == null) return new List<GuestBasicViewModel>();

            var petDto = await context
                .Pets
                .Where(x => guests.Contains(x.Id))
                .Include(x => x.User)
                .Include(x => x.PetType)
                .AsNoTracking()
                .Select(x => new GuestBasicViewModel()
                {
                    PetId = x.Id,
                    OwnerId = x.UserID,
                    OwnerName = x.User.FirstName,
                    PetName = x.Name,
                    PetType = x.PetType.Name
                })
                .ToListAsync();

            if (petDto == null) throw new ArgumentNullException();

           return petDto;
        }
        /// <summary>
        ///Owner of pet in hotel details.
        ///To be used by the employee account
        /// </summary>
        /// <param name="id"></param>
        /// <returns>specific user's details</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<GuestOwnerViewModel> GetGuestOwnerAsync(int id)
        {
            var user = 
                await context.Pets
                .Where(x => x.Id == id)
                .Include(x => x.User)
                .Include(x => x.PetType)
                .AsNoTracking()
                .Select(x => new GuestOwnerViewModel()
                {
                    Id = x.User.Id,
                    Email = x.User.Email,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    PhoneNumber = x.User.PhoneNumber,
                    UserName = x.User.UserName
                })
                .FirstOrDefaultAsync();
            if (user == null) throw new ArgumentNullException();

            return user;
        }
        /// <summary>
        /// Checks if pet is in DB and sends a DTO to be filled/edited
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<AddGuestViewModel> GetGuestToAddAsync(int id)
        {
            var pet = await context
                .Pets
                .Include(x => x.PetType)
                .AsNoTracking()
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

        public async Task<PetViewModel> GuestDetailsAsync(int id)
        {
            var petDto = await context.Pets
                .Where(x => x.Id == id)
                .Include(x => x.PetType)
                .AsNoTracking()
                .Select(x => new PetViewModel() 
                { 
                    Id = x.Id,
                    Age = x.Age,
                    Alergies = x.Alergies,
                    Name = x.Name,
                    PetType = x.PetType.Name,
                    PetTypeID = x.PetTypeID,
                    UserID = x.UserID
                }).FirstOrDefaultAsync();

            if (petDto == null) throw new ArgumentNullException();

            return petDto;
        }
    }
}
