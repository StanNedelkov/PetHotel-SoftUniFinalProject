using Microsoft.EntityFrameworkCore;
using PetHotel.Common;
using PetHotel.Core.Contracts;
using PetHotel.Core.Models.EmployeeModels;
using PetHotel.Core.Models.HotelModels;
using PetHotel.Infrastructure.Data;

namespace PetHotel.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly PetHotelDbContext context;
        public EmployeeService(PetHotelDbContext _context)
        {
            this.context = _context;
        }

        public TasksCountViewModel Counter()
        {
            int expected = context
                .Schedules
                .Where(x => x.AdmissionDate.Day == DateTime.Today.Day && 
                x.Status.ToLower() == GlobalConstants.ExpectedStatus.ToLower())
                .Count();

            int overdue = context
               .Schedules
               .Where(x => x.AdmissionDate.Day < DateTime.Today.Day &&
               x.Status.ToLower() == GlobalConstants.ExpectedStatus.ToLower())
               .Count();

            int departures = context
                .Schedules
                .Where(x => x.DepartureDate.Day == DateTime.Today.Day &&
                x.Status.ToLower() == GlobalConstants.InProgressStatus.ToLower())
                .Count();

            int total = context
                .Schedules
                .Where(x => x.Status.ToLower() == GlobalConstants.ExpectedStatus.ToLower())
                .Count();

            int inHotel = context
                .Schedules
                .Where(x => x.Status.ToLower() == GlobalConstants.InProgressStatus.ToLower())
                .Count();

            return new TasksCountViewModel() 
            { 
                DepartCount = departures, 
                TotalCount= total, 
                ExpectedCount = expected,
                OverdueCount= overdue,
                CurrentlyIn = inHotel
                
            };
        }

        public async Task<IEnumerable<GuestDetailedViewModel>> GetAllAsync()
        {
            var expectedGuests = await context
                 .Schedules
                 .Where(x => x.Status.ToLower() == GlobalConstants.ExpectedStatus.ToLower())
                 .OrderBy(x => x.AdmissionDate)
                 .ToListAsync();



            if (expectedGuests == null) Enumerable.Empty<GuestDetailedViewModel>().ToList();



            var all = new List<GuestDetailedViewModel>();
            foreach (var item in expectedGuests)
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
                     CheckInDate = item.AdmissionDate.ToString("F"),
                     CheckOutDate = item.DepartureDate.ToString("F"),
                     ReservationId = item.Id,
                     Status = item.Status
                 })
                 .FirstOrDefaultAsync();
                all.Add(petDto!);
            }
            if (all == null) throw new ArgumentNullException();

            return all;
        }

        public async Task<IEnumerable<GuestDetailedViewModel>> GetOverdueAsync()
        {
            var expectedGuests = await context
                 .Schedules
                 .Where(x => x.Status.ToLower() == GlobalConstants.ExpectedStatus.ToLower() &&
                 x.AdmissionDate.Day < DateTime.Now.Day && x.AdmissionDate.Month <= DateTime.Now.Month)
                 .OrderBy(x => x.AdmissionDate)
                 .ToListAsync();



            if (expectedGuests == null) Enumerable.Empty<GuestDetailedViewModel>().ToList();



            var all = new List<GuestDetailedViewModel>();
            foreach (var item in expectedGuests)
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
                     CheckInDate = item.AdmissionDate.ToString("F"),
                     CheckOutDate = item.DepartureDate.ToString("F"),
                     ReservationId = item.Id,
                     Status = item.Status
                 })
                 .FirstOrDefaultAsync();
                all.Add(petDto!);
            }
            if (all == null) throw new ArgumentNullException();

            return all;
        }

        public async Task<IEnumerable<GuestDetailedViewModel>> GetDeparturesTodayAsync()
        {
            var expectedGuests = await context
                 .Schedules
                 .Where(x => x.DepartureDate.Day == DateTime.Today.Day &&
                 x.Status.ToLower() == GlobalConstants.InProgressStatus.ToLower() &&
                 x.DepartureDate.Month == DateTime.Today.Month)
                 .ToListAsync();

            

            if (expectedGuests == null) Enumerable.Empty<GuestDetailedViewModel>().ToList();



            var all = new List<GuestDetailedViewModel>();
            foreach (var item in expectedGuests)
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
                     CheckInDate = item.AdmissionDate.ToString("F"),
                     CheckOutDate = item.DepartureDate.ToString("F"),
                     ReservationId = item.Id,
                     Status = item.Status
                 })
                 .FirstOrDefaultAsync();
                all.Add(petDto!);
            }
            if (all == null) throw new ArgumentNullException();

            return all;
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
        public async Task<IEnumerable<GuestDetailedViewModel>> GetAllInHotelAsync()
        {
            var expectedGuests = await context
                .Schedules
                .Where(x => x.Status.ToLower() == GlobalConstants.InProgressStatus.ToLower())
                .OrderBy(x => x.AdmissionDate)
                .ToListAsync();



            if (expectedGuests == null) Enumerable.Empty<GuestDetailedViewModel>().ToList();



            var all = new List<GuestDetailedViewModel>();
            foreach (var item in expectedGuests)
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
                     CheckInDate = item.AdmissionDate.ToString("F"),
                     CheckOutDate = item.DepartureDate.ToString("F"),
                     ReservationId = item.Id,
                     Status = item.Status
                 })
                 .FirstOrDefaultAsync();
                all.Add(petDto!);
            }
            if (all == null) throw new ArgumentNullException();

            return all;
        }
    }
}
