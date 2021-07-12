using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIprojects.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly NotesContext db;
        public ProjectsController(NotesContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {

            return Ok(db.Projects.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) //GetById ??
        {
            var project = db.Projects.Find(id);
            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpGet]
        [Route("/api/projects/{pid}/notes")]
        public IActionResult GetProjectNotes(int pId)
        {
            var notes = db.Notes.Where(n => n.ProjectId == pId).ToList();
            if (notes == null || notes.Count <= 0)
                return NotFound();

            return Ok(notes);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Project project)
        {
            db.Projects.Add(project);
            db.SaveChanges();

            return CreatedAtAction(nameof(GetById),
                    new { id = project.ProjectId },
                    project
                );
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Project project)
        {
            if (id != project.ProjectId) return BadRequest();

            db.Entry(project).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                if (db.Projects.Find(id) == null)
                    return NotFound();

                throw;
            }
            

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = db.Projects.Find(id);
            if (project == null) return NotFound();

            db.Projects.Remove(project);
            db.SaveChanges();

            return Ok(project);
        }
    }
}
