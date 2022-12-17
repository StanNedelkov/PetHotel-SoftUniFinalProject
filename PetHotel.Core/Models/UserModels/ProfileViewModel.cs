using PetHotel.Core.Models.PetModels;

namespace PetHotel.Core.Models.UserModels
{
    public class ProfileViewModel
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string PasswordHidden { get; set; }

        public IEnumerable<PetViewModel> Pets = new List<PetViewModel>();
        
    }
}
