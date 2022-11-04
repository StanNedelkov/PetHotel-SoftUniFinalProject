using PetHotel.Common.EntityConstants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHotel.Infrastructure.Data.Entities
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PetID { get; set; }
        [Required]
        [MinLength(PetConstants.NameMax)]
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
