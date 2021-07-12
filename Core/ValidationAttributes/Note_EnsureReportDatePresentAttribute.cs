using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.ValidationAttributes
{
    class Note_EnsureReportDatePresentAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var note = (Note)validationContext.ObjectInstance;

            if (!note.ValidateReportDatePresence())
                return new ValidationResult("Report date is required.");

            return ValidationResult.Success;
        }
    }
}
