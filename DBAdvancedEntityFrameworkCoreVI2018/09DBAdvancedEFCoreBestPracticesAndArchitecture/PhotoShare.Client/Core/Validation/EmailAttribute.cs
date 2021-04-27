using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoShare.Client.Core.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal class EmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string email = value as string;

            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            return email.Contains("@");
        }
    }
}