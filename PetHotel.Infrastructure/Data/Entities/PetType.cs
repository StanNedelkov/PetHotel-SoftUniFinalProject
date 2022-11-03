using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Infrastructure.Data.Entities
{
    public class PetType
    {
        public PetType()
        {
            this.Pets = new HashSet<Pet>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;

        public ICollection<Pet> Pets { get; set; }

    }
}
