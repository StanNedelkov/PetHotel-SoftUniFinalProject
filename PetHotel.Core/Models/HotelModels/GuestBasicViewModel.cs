using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Models.HotelModels
{
    public class GuestBasicViewModel
    {
        [Required]
        public int PetId { get; set; }
        [Required]
        public string PetName { get; set; } = null!;

        public string PetType { get; set; } = null!;
        [Required]
        public string CheckInDate { get; set; } = null!;
        [Required]
        public string CheckOutDate { get; set; } = null!;
        [Required]
        public int ReservationId { get; set; }
        [Required]
        public string Status { get; set;} = null!;

        public decimal CostForStay { get; set; }
    }
}
