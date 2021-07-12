using Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Note
    {
        public int? NoteId { get; set; }

        [Required]
        public int? ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }

        [StringLength(50)]
        public string Owner { get; set; }

        [Note_EnsureReportDatePresent]
        public DateTime? ReportDate { get; set; }

        [Note_EnsureDueDatePresent]
        [Note_EnsureDueDateAfterReportDate]
        [Note_EnsureFutureDueDateOnCreation]
        public DateTime? DueDate { get; set; }

        public Project Project { get; set; }

        public bool ValidateDescription()
        {
            return !string.IsNullOrWhiteSpace(Description);
        }
        

        public bool ValidateFutureDueDate()
        {
            if (NoteId.HasValue) return true;
            if (!DueDate.HasValue) return true;

            return (DueDate.Value > DateTime.Now);
        }

        public bool ValidateReportDatePresence()
        {
            if (string.IsNullOrWhiteSpace(Owner)) return true;

            return ReportDate.HasValue;
        }

        public bool ValidatDueDatePresence()
        {
            if (string.IsNullOrWhiteSpace(Owner)) return true;

            return DueDate.HasValue;
        }

        public bool ValidateDueDateAfterReportDate()
        {
            if (!DueDate.HasValue || !ReportDate.HasValue) return true;

            return DueDate.Value.Date >= ReportDate.Value.Date;
        }
    }
}
