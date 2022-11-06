using PetHotel.Common.EntityConstants;
using PetHotel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetHotel.Core.Models.PetModels;

namespace PetHotel.Core.Models.UserModels
{
    public class ProfileViewModel
    {
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHidden { get; set; } = null!;

        public IEnumerable<PetViewModel> Pets = new List<PetViewModel>();
        
    }
}
