using PetHotel.Common.EntityConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Models.HotelModels
{
    public class AddGuestViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(PetConstants.NameMin)]
        public string Name { get; set; } = null!;
        public string CheckInDate { get; set; } = null!;
        public string CheckOutDate { get; set; } = null!;
    }
}
