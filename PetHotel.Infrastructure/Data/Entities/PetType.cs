using Microsoft.EntityFrameworkCore;
using PetHotel.Common.EntityConstants;
using System.ComponentModel.DataAnnotations;

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
        [MinLength(PetConstants.PetTypeNameMax)]
        public string Name { get; set; } = null!;

        [Required]
        [Precision(18,2)]
        public decimal CostPerDay { get; set; }
        public ICollection<Pet> Pets { get; set; }

    }
}
