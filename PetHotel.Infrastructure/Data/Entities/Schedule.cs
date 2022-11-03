using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Infrastructure.Data.Entities
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PetID { get; set; }
        [Required]
        public string PetName { get; set; } = null!;
        [Required]
        public DateTime AdmissionDate { get; set; }
        [Required]
        public DateTime DepartureDate { get; set; }

        public int HotelID { get; set; }
        [Required]
        [ForeignKey(nameof(HotelID))]
        public Hotel Hotel { get; set; } = null!;
    }
}
