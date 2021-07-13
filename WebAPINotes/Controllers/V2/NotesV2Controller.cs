using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPINotes.Filters.V2;
using WebAPINotes.QueryFilters;

namespace WebAPIprojects.Controllers
{ 
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/notes")]
    public class NotesV2Controller : ControllerBase
    {
        private readonly NotesContext db;
        public NotesV2Controller(NotesContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] NoteQueryFilter noteQueryFilter)
        {
            IQueryable<Note> notes = db.Notes;
            
            if(noteQueryFilter != null)
            {
                if (noteQueryFilter.Id.HasValue)
                    notes = notes.Where(x => x.NoteId == noteQueryFilter.Id);

                if (!string.IsNullOrWhiteSpace(noteQueryFilter.TitleOrDescription))
                    notes = notes.Where(x => x.Title.Contains(noteQueryFilter.TitleOrDescription,
                        StringComparison.OrdinalIgnoreCase) ||
                        x.Description.Contains(noteQueryFilter.TitleOrDescription,
                        StringComparison.OrdinalIgnoreCase));
            }

            return Ok(await notes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var note = await db.Notes.FindAsync(id);
            if (note == null)
                return NotFound();

            return Ok(note);
        }

        [HttpPost]
        [Note_EnsureDescriptionPresent]
        public async Task<IActionResult> Post([FromBody] Note note)
        {
            db.Notes.Add(note);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                    new { id = note.NoteId },
                    note
                );
        }

        [HttpPut("{id}")]
        [Note_EnsureDescriptionPresent]
        public async Task<IActionResult> Put(int id, [FromBody] Note note)
        {
            if (id != note.NoteId) return BadRequest();

            db.Entry(note).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                if (db.Notes.Find(id) == null)
                    return NotFound();

                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await db.Notes.FindAsync(id);
            if (note == null) return NotFound();

            db.Notes.Remove(note);
            await db .SaveChangesAsync();

            return Ok(note);
        }
    }
}
