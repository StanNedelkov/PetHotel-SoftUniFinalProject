using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Infrastructure.Data.Entities
{
    public class GalleryImage
    {
        public GalleryImage()
        {
            this.Tags = new HashSet<ImageTag>();
        }
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Created { get; set; }
        public string Url { get; set; } = null!;
        public ICollection<ImageTag> Tags { get; set; }

        public int HotelID { get; set; }
        [Required]
        [ForeignKey(nameof(HotelID))]
        public Hotel Hotel { get; set; } = null!;
    }
}
