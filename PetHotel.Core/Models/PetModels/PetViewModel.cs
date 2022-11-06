using PetHotel.Common.EntityConstants;
using PetHotel.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Models.PetModels
{
    public class PetViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(PetConstants.NameMin)]
        public string Name { get; set; } = null!;
        [Required]
        [Range(PetConstants.AgeMin, PetConstants.DogAgeMax)]
        public int Age { get; set; }
        [MinLength(PetConstants.AlergiesMaxLength)]
        public string Alergies { get; set; } = null!;
        public int PetTypeID { get; set; }
        [Required]
        public string PetType { get; set; } = null!;
        public string UserID { get; set; } = null!;
    }
}
