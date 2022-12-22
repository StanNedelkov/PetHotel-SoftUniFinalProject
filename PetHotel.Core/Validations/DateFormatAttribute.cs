using PetHotel.Common;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PetHotel.Core.Validations
{
    public class DateFormatAttribute : ValidationAttribute
    {
        private DateTime initialDate;
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var date = value as string;

            if (date != null)
            {
                if (!CheckDateFormat(date)) return new ValidationResult(ErrorMessagesConstants.dateInvalid);
                if (!DateCheck()) return new ValidationResult(ErrorMessagesConstants.dateInvalid);

            }



            return ValidationResult.Success;
        }


        private bool DateCheck()
        {
            if (initialDate.Day < DateTime.Now.Day && initialDate.Month <= DateTime.Now.Month)
            {
                return false;
            }
            if (initialDate.Month < DateTime.Now.Month && initialDate.Year <= DateTime.Now.Year)
            {
                return false;
            }

            return true;
        }
        private bool CheckDateFormat(string checkDate)
        {
            
            bool isValidDate = DateTime
                .TryParseExact(checkDate,
                GlobalConstants.DateTimeFormatConst,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out initialDate);

            if (!isValidDate) return CheckAlternateDateFormat(checkDate);
            return true;
        }

        private bool CheckAlternateDateFormat(string checkDate)
        {
            
            bool isValidDate = DateTime
                .TryParseExact(checkDate,
                GlobalConstants.DateTimeAlternateFormatConst,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out initialDate);
            
            return isValidDate;
        }
    }
}
