using PetHotel.Common;
using PetHotel.Common.EntityConstants;
using System.ComponentModel.DataAnnotations;

namespace PetHotel.Core.Models.UserModels
{
    public class RegisterViewModel
    {
        //TODO add validation for unique username
        [Required]
        [StringLength(UserConstants.UserNameMax, MinimumLength = UserConstants.UserNameMin, ErrorMessage = ErrorMessagesConstants.firstNameInvalid)]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(UserConstants.FirstNameMax, MinimumLength = UserConstants.FirstNameMin, ErrorMessage = ErrorMessagesConstants.firstNameInvalid)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(UserConstants.LastNameMax, MinimumLength = UserConstants.LastNameMin, ErrorMessage = ErrorMessagesConstants.lastNameInvalid)]
        public string LastName { get; set; } = null!;

        //todo add unique email validation
        [Required]
        [Phone]
        [StringLength(UserConstants.PhoneMax, MinimumLength = UserConstants.PhoneMin, ErrorMessage = ErrorMessagesConstants.phoneInvalid)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(UserConstants.PasswordMax, MinimumLength = UserConstants.PasswordMin, ErrorMessage = ErrorMessagesConstants.passwordInvalid)]
        public string Password { get; set; } = null!;
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
