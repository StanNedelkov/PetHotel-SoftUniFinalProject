using PetHotel.Core.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Contracts
{
    public interface IUserService
    {
        Task RegisterNewUser(RegisterViewModel model);
    }
}
