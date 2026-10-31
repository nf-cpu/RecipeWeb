using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DTO.Validation
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxMb;

        public MaxFileSizeAttribute(int maxMb)
        {
            _maxMb = maxMb;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file && file.Length > _maxMb * 1024L * 1024L)
            {
                return new ValidationResult($"File size cannot exceed {_maxMb}MB");
            }

            return ValidationResult.Success;
        }
    }
}
