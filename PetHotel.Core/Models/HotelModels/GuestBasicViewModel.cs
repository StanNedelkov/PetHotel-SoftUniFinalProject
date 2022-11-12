using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Models.HotelModels
{
    public class GuestBasicViewModel
    {
        public int PetId { get; set; }
        public int OwnerId { get; set; }
        public string PetName { get; set; } = null!;
        public string OwnerName { get; set; } = null!;

        public string PetType { get; set; } = null!;
    }
}
