using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIprojects.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/notes")] //Remove [controller] and add: notes
    public class NotesController : ControllerBase
    {
        private readonly NotesContext db;
        public NotesController(NotesContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await db.Notes.ToListAsync());
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
