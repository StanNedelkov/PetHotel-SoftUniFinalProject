using Microsoft.EntityFrameworkCore;
using PetHotel.Common;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.HotelModels;
using PetHotel.Core.Models.PetModels;
using PetHotel.Infrastructure.Data;
using PetHotel.Infrastructure.Data.Entities;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace PetHotel.Core.Services
{
    public class HotelService : IHotelService
    {
        private readonly PetHotelDbContext context;
        public HotelService(PetHotelDbContext _context)
        {
            this.context = _context;
        }

        private static DateTime CheckDateFormat(string checkDate)
        {
            DateTime date;
            bool isValidDate = DateTime
                .TryParseExact(checkDate,
                GlobalConstants.DateTimeFormatConst,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date);

            if (!isValidDate) return CheckAlternateDateFormat(checkDate);
            return date;
        }

        private static DateTime CheckAlternateDateFormat(string checkDate)
        {
            DateTime date;
            bool isValidDate = DateTime
                .TryParseExact(checkDate,
                GlobalConstants.DateTimeAlternateFormatConst,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date);

            if (!isValidDate) throw new ArgumentException();
            return date;
        }

        public async Task AddGuestAsync(AddGuestViewModel model)
        {
            DateTime admissionDate = CheckDateFormat(model.CheckInDate);

            DateTime departureDate = CheckDateFormat(model.CheckOutDate);

            var schedule = new Reservation()
            {
               AdmissionDate = admissionDate,
               DepartureDate = departureDate,
               HotelID = GlobalConstants.CatsDogsAndCrocsHotelId,
               PetID = model.Id,
               PetName = model.Name,
               Status = GlobalConstants.ExpectedStatus
            };

            await context.Schedules.AddAsync(schedule);
            await context.SaveChangesAsync();   

          
        }

        
        public async Task CancelHotelStayAsync(int id)
        {
            var petToCancel = await context.Schedules
                .FirstOrDefaultAsync(x => x.Id == id);

            if (petToCancel == null) throw new ArgumentNullException();

                petToCancel.Status = GlobalConstants.CanceledStatus;
                await context.SaveChangesAsync();
        }

        //edit reservation dates
        public async Task EditGuestAsync(AddGuestViewModel model)
        {
            DateTime admissionDate = CheckDateFormat(model.CheckInDate);

            DateTime departureDate = CheckDateFormat(model.CheckOutDate);

            if (admissionDate.Day < DateTime.Now.Day || departureDate.Day < DateTime.Now.Day || admissionDate.Day > departureDate.Day)
            {
                throw new ArgumentException();
            }

            var guest = await context
                .Schedules
                .Where(x => x.PetID == model.Id && x.Status == GlobalConstants.ExpectedStatus)
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
        public async Task<ICollection<GuestDetailedViewModel>> GetAllGuestsTodayAsync()
        {

            var expectedGuests = await context
                .Schedules
                .Where(x => x.AdmissionDate.Day == DateTime.Today.Day && 
                x.Status.ToLower() == GlobalConstants.ExpectedStatus.ToLower() &&
                x.AdmissionDate.Month == DateTime.Today.Month)
                .ToListAsync();

            

            if (expectedGuests == null) return Enumerable.Empty<GuestDetailedViewModel>().ToList();



            var all = new List<GuestDetailedViewModel>();
            foreach (var item in expectedGuests!)
            {
                var petDto = await context
                 .Pets
                 .Where(x => item.PetID == x.Id)
                 .Include(x => x.Hotel)
                 .ThenInclude(x => x.Schedules)
                 .Include(x => x.PetType)
                 .AsNoTracking()
                 .Select(x => new GuestDetailedViewModel()
                 {
                     PetId = x.Id,
                     PetName = x.Name,
                     PetType = x.PetType.Name,
                     UserId = x.User.Id,
                     UserName = x.User.UserName,
                     CheckInDate = item.AdmissionDate.ToString("M"),
                     CheckOutDate = item.DepartureDate.ToString("M"),
                     ReservationId = item.Id,
                     Status = item.Status
                 })
                 .FirstOrDefaultAsync();
                all.Add(petDto!);
            }

            

            if (all == null) throw new ArgumentNullException();

           return all;
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

        public async Task<AddGuestViewModel> GetGuestToEditAsync(int id)
        {
            var pet = await context
              .Schedules
              .AsNoTracking()
              .Where(x => x.Status.ToLower() == GlobalConstants.ExpectedStatus.ToLower())
              .FirstOrDefaultAsync(x => x.Id == id);

            if (pet == null)
            {
                throw new ArgumentNullException();
            }

            return new AddGuestViewModel()
            {
                Id = pet.PetID,
                Name = pet.PetName,
                CheckInDate = pet.AdmissionDate.ToString(GlobalConstants.DateTimeFormatConst),
                CheckOutDate = pet.DepartureDate.ToString(GlobalConstants.DateTimeFormatConst)
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
                    CheckInDate = x.AdmissionDate.ToString("M"),
                    CheckOutDate= x.DepartureDate.ToString("M"),
                    Status = x.Status
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
       

        public async Task<bool> IsGuestOwnedByUser(int reservationId, string userId)
        {
            int petId = await context
                .Schedules
                .Where(x => x.Id == reservationId)
                .Select(x => x.PetID)
                .FirstOrDefaultAsync();

            var user = await context
                .Users
                .Include(x => x.Pets)
                .FirstOrDefaultAsync(x => x.Id == userId);

            return user!.Pets.Any(x => x.Id == petId);
           
        }
    }
}
