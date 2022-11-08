using PetHotel.Common.EntityConstants;
using System.ComponentModel.DataAnnotations;

namespace PetHotel.Core.Models.PetModels
{
    public class CreatePetViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(PetConstants.NameMin)]
        public string Name { get; set; } = null!;
        [Required]
        [Range(PetConstants.AgeMin, PetConstants.DogAgeMax)]
        public int Age { get; set; }
        [MinLength(PetConstants.AlergiesMinLength)]
        public string Alergies { get; set; } = null!;
        public int PetTypeID { get; set; }
    }
}
