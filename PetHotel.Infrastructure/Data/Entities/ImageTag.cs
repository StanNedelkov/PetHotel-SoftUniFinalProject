using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Infrastructure.Data.Entities
{
    public class ImageTag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; } = null!;
    }
}
