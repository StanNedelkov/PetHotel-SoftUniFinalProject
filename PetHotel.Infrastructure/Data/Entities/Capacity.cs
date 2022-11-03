using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Infrastructure.Data.Entities
{
    public class Capacity
    {
        [Key]
        public int Id { get; set; }

        public int CurCapacityDogs { get; set; }
        [Required]
        public int MaxCapacityDogs { get; set; }

        public int CurCapacityCats { get; set; }
        [Required]
        public int MaxCapacityCats { get; set; }

        public int CurCapacityGator { get; set; }
        [Required]
        public int MaxCapacityGator { get; set; }

        public int HotelID { get; set; }
        [Required]
        [ForeignKey(nameof(HotelID))]
        public Hotel Hotel { get; set; } = null!;
    }
}
