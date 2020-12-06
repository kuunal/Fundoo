using BusinessLayer.Interface;
using Greeting.TokenAuthentication;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundoo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _service;

        public NotesController(INotesService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("get")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> GetNotesAsync()
        {
            int userid = Convert.ToInt32(HttpContext.Items["userId"]);
            List<Note> notes = await _service.GetNotes(userid);
            if (notes == null)
            {
                return Ok("No notes created");
            }
            return Ok(notes);
        }

        [HttpPost]
        [Route("add")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> AddNoteASync(Note note)
        {
            int userid = Convert.ToInt32(HttpContext.Items["userId"]);
            Note addednote = await _service.AddNote(note, userid);
            if (addednote == null)
            {
                return BadRequest();
            }
            return Ok(note);
        }
    
        [HttpDelete]
        [Route("delete/{id}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> DeleteNoteAsync(int id)
        {
            var result = await _service.DeleteNote(id);
            if (result == null)
            {
                return BadRequest("No such note Exists!");
            }    
            return Ok("Deleted");
        }

    }
}
