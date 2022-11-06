using PetHotel.Common;
using System.ComponentModel.DataAnnotations;

namespace PetHotel.Core.Models.UserModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage =ErrorMessagesConstants.requiredField)]
        public string UserName { get; set; } = null!;
        [Required(ErrorMessage = ErrorMessagesConstants.requiredField)]
        public string Password { get; set; } = null!;
    }
}
