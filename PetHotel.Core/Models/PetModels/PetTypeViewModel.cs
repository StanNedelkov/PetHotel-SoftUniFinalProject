using System.ComponentModel.DataAnnotations;

namespace PetHotel.Core.Models.PetModels
{
    public class PetTypeViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}
