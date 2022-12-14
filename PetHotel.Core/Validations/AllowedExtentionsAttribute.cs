using Microsoft.AspNetCore.Http;
using PetHotel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHotel.Core.Validations
{
    public class AllowedExtentionsAttribute : ValidationAttribute
    {
        private readonly string[] extensions;
        public AllowedExtentionsAttribute(string[] _extensions)
        {
            this.extensions = _extensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!extensions.Contains(extension.ToLower())) return new ValidationResult(ErrorMessagesConstants.fileExtentionInvalid);
                
            }

            return ValidationResult.Success;
        }

     
    }
}
