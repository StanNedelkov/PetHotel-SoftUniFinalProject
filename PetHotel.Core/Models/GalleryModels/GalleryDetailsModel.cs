using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Models.GalleryModels
{
    public class GalleryDetailsModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Created { get; set; }
        public string Url { get; set; } = null!;

        public List<string> Tags { get; set; }
    }
}
