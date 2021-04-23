using System;
using System.ComponentModel.DataAnnotations;

namespace P01_BillsPaymentSystem.Data.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class XorAttribute : ValidationAttribute
    {
        private const string PaimentMethodErrorMessage = "It is required to use one payment method!";

        private string xorTargetAttribute;

        public XorAttribute(string xorTargetAttribute)
        {
            this.xorTargetAttribute = xorTargetAttribute;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            object targetAttribute = validationContext.ObjectType.GetProperty(xorTargetAttribute)
                .GetValue(validationContext.ObjectInstance);

            if ((targetAttribute == null && value != null) || (targetAttribute != null && value == null))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(PaimentMethodErrorMessage);
            }
        }
    }
}