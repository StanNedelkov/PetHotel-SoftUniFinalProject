using PetHotel.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Models.GalleryModels
{
    public class GalleryIndexModel
    {
        public GalleryIndexModel()
        {
            this.Images = new HashSet<GalleryImage>();
        }
        public IEnumerable<GalleryImage> Images { get; set; }
        public string SearchQuery { get; set; } = null!;
    }
}
