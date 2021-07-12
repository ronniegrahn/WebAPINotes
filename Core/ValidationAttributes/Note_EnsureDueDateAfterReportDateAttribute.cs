using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.ValidationAttributes
{
    class Note_EnsureDueDateAfterReportDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var note = (Note)validationContext.ObjectInstance;

            if (!note.ValidateDueDateAfterReportDate())
                return new ValidationResult("Due date has to be after Report date.");

            return ValidationResult.Success;
        }
    }
}
