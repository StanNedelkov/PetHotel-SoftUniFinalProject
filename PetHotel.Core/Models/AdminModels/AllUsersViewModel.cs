using PetHotel.Core.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Models.AdminModels
{
    public class AllUsersViewModel
    {
        public ICollection<ProfileViewModel> AllProfiles = new List<ProfileViewModel>();
    }
}
