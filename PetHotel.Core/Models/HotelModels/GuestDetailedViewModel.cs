using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Models.HotelModels
{
    public class GuestDetailedViewModel
    {
        [Required]
        public int PetId { get; set; }

        public string PetName { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string PetType { get; set; } = null!;
        [Required]
        public string CheckInDate { get; set; } = null!;
        [Required]
        public string CheckOutDate { get; set; } = null!;
        [Required]
        public int ReservationId { get; set; }

        public string Status { get; set; } = null!;

        public decimal StayCost { get; set; }
        public string Allergies { get; set; } = null!;
    }
}
