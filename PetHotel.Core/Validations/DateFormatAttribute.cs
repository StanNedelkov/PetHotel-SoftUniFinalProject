using PetHotel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Validations
{
    public class DateFormatAttribute : ValidationAttribute
    {
        
        public DateFormatAttribute()
        {
            
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var date = value as string;

            if (date != null)
            {
                if (!CheckDateFormat(date)) return new ValidationResult(ErrorMessagesConstants.dateInvalid);
            }
            return ValidationResult.Success;
        }

        private bool CheckDateFormat(string checkDate)
        {
            DateTime date;
            bool isValidDate = DateTime
                .TryParseExact(checkDate,
                GlobalConstants.DateTimeFormatConst,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date);

            if (!isValidDate) return CheckAlternateDateFormat(checkDate);
            return true;
        }

        private bool CheckAlternateDateFormat(string checkDate)
        {
            DateTime date;
            bool isValidDate = DateTime
                .TryParseExact(checkDate,
                GlobalConstants.DateTimeAlternateFormatConst,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date);
            
            return isValidDate;
        }
    }
}
