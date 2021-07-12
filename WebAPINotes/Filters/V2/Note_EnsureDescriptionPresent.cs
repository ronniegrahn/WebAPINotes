using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPINotes.Filters.V2
{
    public class Note_EnsureDescriptionPresent : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var note = context.ActionArguments["note"] as Note;
            if(note != null && !note.ValidateDescription())
            {
                context.ModelState.AddModelError("Description", "Description is required.");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
