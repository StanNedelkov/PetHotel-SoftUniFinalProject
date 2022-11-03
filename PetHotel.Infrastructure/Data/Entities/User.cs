using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetHotel.Common.EntityConstants;

namespace PetHotel.Infrastructure.Data.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Pets = new HashSet<Pet>();
        }
        [Required]
        [MinLength(UserConstants.FirstNameMin)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MinLength(UserConstants.LastNameMin)]
        public string LastName { get; set; } = null!;
        public ICollection<Pet> Pets { get; set; }
    }
}
