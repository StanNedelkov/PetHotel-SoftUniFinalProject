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

        private static DateTime CheckDateFormat(string checkinDate)
        {
            DateTime admissionDate;
            bool isAdmissonDate = DateTime
                .TryParseExact(checkinDate,
                GlobalConstants.DateTimeFormatConst,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out admissionDate);

            if (!isAdmissonDate) throw new ArgumentException();
            return admissionDate;
        }

        public async Task AddGuestAsync(AddGuestViewModel model)
        {
            DateTime admissionDate = CheckDateFormat(model.CheckInDate);

            DateTime departureDate = CheckDateFormat(model.CheckOutDate);

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

        //TODO add date time to collect your pet, update the dto. Return it or redirect to pets in hotel list
        //
        public async Task CancelHotelStayAsync(int id)
        {
            var petToCancel = await context.Schedules
                .FirstOrDefaultAsync(x => x.Id == id);

            if (petToCancel == null) throw new ArgumentNullException();

            if (petToCancel.AdmissionDate > DateTime.Now)
            {
                    context.Schedules.Remove(petToCancel);
                    await context.SaveChangesAsync();
               
            }

            //TODO change checkout date and move to new table archive or create bool inactive or delete
        }

        public async Task EditGuestAsync(AddGuestViewModel model)
        {
            DateTime admissionDate = CheckDateFormat(model.CheckInDate);

            DateTime departureDate = CheckDateFormat(model.CheckOutDate);

            var guest = await context
                .Schedules
                .Where(x => x.PetID == model.Id)
                .FirstOrDefaultAsync();

            guest!.AdmissionDate = admissionDate;
            guest!.DepartureDate = departureDate;

            await context.SaveChangesAsync();
        }



        /// <summary>
        /// List of all pets in the hotel.
        /// To be used by the employee account.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>

        //todo new DTO for more details
        public async Task<ICollection<GuestBasicViewModel>> GetAllGuestsAsync()
        {

            List<int> guests = await context
                .Schedules
                .Select(x => x.PetID)
                .ToListAsync();

            if (guests == null) Enumerable.Empty<GuestBasicViewModel>().ToList();

            var petDto = await context
                .Pets
                .Where(x => guests!.Contains(x.Id))
                .Include(x => x.Hotel)
                .ThenInclude(x => x.Schedules)
                .Include(x => x.PetType)
                .AsNoTracking()
                .Select(x => new GuestBasicViewModel()
                {
                    PetId = x.Id,
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
        /// by the client user.
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


        /// <summary>
        /// List of all user's pets in hotel.
        /// To be used by clients.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ICollection<GuestBasicViewModel>> GetMyAllGuestsAsync(string userId)
        {
            List<int> petsOwnedByUser = await context
                .Pets
                .Where(x => x.UserID == userId)
                .Select(x => x.Id)
                .ToListAsync();

            if (petsOwnedByUser == null) return Enumerable
                   .Empty<GuestBasicViewModel>()
                   .ToList();


            var reservedPets = await context
                .Schedules
                .Where(x => petsOwnedByUser.Contains(x.PetID))
                .Select(x => new GuestBasicViewModel()
                {
                    ReservationId = x.Id,
                    PetId= x.PetID,
                    CheckInDate = x.AdmissionDate.ToString(),
                    CheckOutDate= x.DepartureDate.ToString()
                })
                .ToListAsync();



            if (reservedPets == null)  return Enumerable
                    .Empty<GuestBasicViewModel>()
                    .ToList();



            foreach (var pet in reservedPets)
            {
                string? name = await context
                    .Pets
                    .Where(x => x.Id == pet.PetId)
                    .Select(x => x.Name)
                    .FirstOrDefaultAsync();
                pet.PetName = name!;

                string? type = await context
                    .Pets
                    .Include(x => x.PetType)
                    .Where(x => x.Id == pet.PetId)
                    .Select(x => x.PetType.Name)
                    .FirstOrDefaultAsync();
                pet.PetType = type!;
            }

            return reservedPets;
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
                })
                .FirstOrDefaultAsync();

            if (petDto == null) throw new ArgumentNullException();

            return petDto;
        }
        /// <summary>
        /// Change collection date to an earlier time
        /// </summary>
        /// <param name="bookedId"></param>
        /// <param name="newCollectionDate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task PickUpearly(int bookedId, string newCollectionDate)
        {
            var petToCancel = await context.Schedules
               .FirstOrDefaultAsync(x => x.Id == bookedId);

            if (petToCancel == null) throw new ArgumentNullException(); // check if pet guest exists

            if (petToCancel.AdmissionDate > DateTime.Now && 
                petToCancel.DepartureDate == DateTime.Now) throw new ArgumentException(); // check if pet guest is staying in the hotel



            DateTime departureDate = CheckDateFormat(newCollectionDate); // check if new collection date format is ok

            if (petToCancel.AdmissionDate > departureDate &&
           petToCancel.DepartureDate == departureDate &&
           departureDate >= DateTime.Now) throw new ArgumentException(); // check if new collection date is not earlier or later
                                                                         // than the stay in the hotel

            petToCancel.DepartureDate = departureDate;
            await context.SaveChangesAsync();
        }
    }
}
