using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Infrastructure.Data.Entities
{
    public class Hotel
    {
        public Hotel()
        {
            this.Pets = new HashSet<Pet>();
            this.Schedules = new HashSet<Schedule>();
            
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int CapacityID { get; set; }
        [Required]
        [ForeignKey(nameof(CapacityID))]
        public Capacity Capacity { get; set; } = null!;

        public ICollection<Pet> Pets { get; set; }
        public ICollection<Schedule> Schedules{ get; set; }
    }
}
