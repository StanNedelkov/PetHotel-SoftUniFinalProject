using PetHotel.Common.EntityConstants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHotel.Infrastructure.Data.Entities
{
    public class Hotel
    {
        public Hotel()
        {
            this.Pets = new HashSet<Pet>();
            this.Schedules = new HashSet<Schedule>();
            this.GalleryImages = new HashSet<GalleryImage>();

        }
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(HotelConstants.HotelNameMax)]
        public string Name { get; set; } = null!;

        public int CapacityID { get; set; }
        [Required]
        [ForeignKey(nameof(CapacityID))]
        public Capacity Capacity { get; set; } = null!;

        public ICollection<Pet> Pets { get; set; }
        public ICollection<Schedule> Schedules{ get; set; }

        public int GalleryID { get; set; }
        public ICollection<GalleryImage> GalleryImages { get; set; }
    }
}
