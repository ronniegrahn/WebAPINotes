using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebAPIprojects.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly NotesContext db;
        public NotesController(NotesContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(db.Notes.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var note = db.Notes.Find(id);
            if (note == null)
                return NotFound();

            return Ok(note);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Note note)
        {
            db.Notes.Add(note);
            db.SaveChanges();

            return CreatedAtAction(nameof(GetById),
                    new { id = note.NoteId },
                    note
                );
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Note note)
        {
            if (id != note.NoteId) return BadRequest();

            db.Entry(note).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
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
        public IActionResult Delete(int id)
        {
            var note = db.Notes.Find(id);
            if (note == null) return NotFound();

            db.Notes.Remove(note);
            db.SaveChanges();

            return Ok(note);
        }
    }
}
