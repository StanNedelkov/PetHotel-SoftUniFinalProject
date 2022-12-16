using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Models.EmployeeModels
{
    public class ManageViewModel
    {
        [Required]
        public int ReservationId { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string CheckIn { get; set; }
        [Required]
        public string CheckOut { get; set; }
       
    }
}
