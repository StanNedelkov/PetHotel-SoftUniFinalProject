using PetHotel.Common.EntityConstants;
using System.ComponentModel.DataAnnotations;

namespace PetHotel.Infrastructure.Data.Entities
{
    public class Capacity
    {
        [Key]
        public int Id { get; set; }
        [Range(PetConstants.DogMaxCapacity, 0)]
        public int CurCapacityDogs { get; set; }
        [Required]
        [Range(PetConstants.DogMaxCapacity, 0)]
        public int MaxCapacityDogs { get; set; }
        [Range(PetConstants.CatMaxCapacity, 0)]
        public int CurCapacityCats { get; set; }
        [Required]
        [Range(PetConstants.CatMaxCapacity, 0)]
        public int MaxCapacityCats { get; set; }
        [Range(PetConstants.GatorMaxCapacity, 0)]
        public int CurCapacityGator { get; set; }
        [Required]
        [Range(PetConstants.GatorMaxCapacity, 0)]
        public int MaxCapacityGator { get; set; }

        public int HotelID { get; set; }
       
    }
}
