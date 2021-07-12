using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIprojects.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Reading all the notes.");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"Reading note #{id}.");
        }

        [HttpPost]
        public IActionResult PostV1([FromBody] Note note)
        {
            return Ok(note);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Note note)
        {
            return Ok(note);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Deleting note #{id}.");
        }
    }
}
