namespace PetHotel.Common
{
    public class ErrorMessagesConstants
    {
        public const string requiredField = "Required";
        public const string registrationFailed = "An unexpected error accured and registration failed!";
        public const string roleFailed = "An unexpected error accured. User rights couldn't be added to your account, please contact support";
        public const string userNotFound = "User not found or wrong username.";
        public const string usernameExists = "Username already exists.";
        public const string usernameInvalid = "Invalid Username.";
        public const string passwordInvalid = $"Invalid Password. Required 6-32 characters, one uppercase/number/symbol";
        public const string usernameOrPasswordInvalid = "Wrong Username and/or Password.";
        public const string phoneInvalid = "Invalid Phone Number. Required 10 digits.";
        public const string emailExist = "Email already exists.";
        public const string emailInvalid = "Invalid Email.";
        public const string petNameInvalid = "Invalid Pet Name. Required atleast 2 characters WITHOUT digits/symbols";
        public const string petTypeInvalid = "Invalid Pet Type.";
        public const string dateInvalid = "Invalid Date.";
        public const string firstNameInvalid = "Invalid First Namme. Required atleast 2 characters WITHOUT digits/symbols";
        public const string lastNameInvalid = "Invalid Last Name. Required atleast 2 characters WITHOUT digits/symbols";
    }
}
