namespace PetHotel.Common
{
    public class ErrorMessagesConstants
    {
        public const string usernameExists = "Username already exists.";
        public const string usernameInvalid = "INVALID USERNAME.";
        public const string passwordInvalid = $"INVALID PASSWORD. Required 6-32 characters, one uppercase/number/symbol";
        public const string usernameOrPasswordWrong = "Wrong Username and/or Password.";
        public const string phoneInvalid = "INVALID PHONE NUMBER. Required 10 digits.";
        public const string emailExist = "Email already exists.";
        public const string emailInvalid = "INVALID EMAIL.";
        public const string petNameInvalid = "INVALID PET NAME. Required atleast 2 characters WITHOUT digits/symbols";
        public const string petTypeInvalid = "INVALID PET TYPE.";
        public const string dateInvalid = "INVALID DATE.";
        public const string firstNameInvalid = "INVALID FIRST NAME. Required atleast 2 characters WITHOUT digits/symbols";
        public const string lastNameInvalid = "INVALID LAST NAME. Required atleast 2 characters WITHOUT digits/symbols";
    }
}
