using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Reading all the projects.");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"Reading project #{id}.");
        }

        [HttpGet]
        [Route("/api/projects/{pid}/notes")]
        public IActionResult GetProjectNote(int pId, [FromQuery] int nId)
        {
            if(nId == 0)
            {
                return Ok($"Reading all the notes belonging to project #{pId}.");
            }
            return Ok($"Reading project #{pId}, note #{nId}.");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Creating a new project.");
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok("Updating a project.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Deleting project #{id}.");
        }
    }
}
