using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPINotes.QueryFilters
{
    public class NoteQueryFilter
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
