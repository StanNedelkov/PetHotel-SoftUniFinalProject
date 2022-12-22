using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Models.HomeModels
{
    public class PricesViewModel
    {
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
