using System;
using System.ComponentModel.DataAnnotations;

namespace P01_BillsPaymentSystem.Data.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NonUnicodeAttribute : ValidationAttribute
    {
        private const string NullErrorMessage = "Value can not be null!";
        private const string NotASCIIErrorMessage = "Value can not contain all unicode characters, only ASCII symbols!";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(NullErrorMessage);
            }

            string valueString = (string)value;

            for (int i = 0; i < valueString.Length; i++)
            {
                if (valueString[i] > 255)
                {
                    return new ValidationResult(NotASCIIErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}