using PetHotel.Common.EntityConstants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHotel.Infrastructure.Data.Entities
{
    public class Pet
    {
        [Key]
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
        [Required]
        [ForeignKey(nameof(PetTypeID))]
        public PetType PetType { get; set; } = null!;
        public string UserID { get; set; } = null!;
        [Required]
        [ForeignKey(nameof(UserID))]
        public User User { get; set; } = null!;

        public int HotelID { get; set; }
        [ForeignKey(nameof(HotelID))]
        public Hotel Hotel { get; set; } = null!;


    }
}
