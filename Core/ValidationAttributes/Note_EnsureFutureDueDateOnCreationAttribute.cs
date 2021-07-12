using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.ValidationAttributes
{
    class Note_EnsureFutureDueDateOnCreationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var note = (Note)validationContext.ObjectInstance;

            if (!note.ValidateFutureDueDate())
                return new ValidationResult("Due date has to be in the future. Do you own a DeLorean?");

            return ValidationResult.Success;
        }
    }
}
