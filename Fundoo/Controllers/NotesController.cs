using BusinessLayer.Interface;
using Caching;
using Greeting.TokenAuthentication;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using ModelLayer.DTOs.NoteDTO;
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
        [Cached(600)]
        public async Task<IActionResult> GetNotesAsync()
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            List<NoteResponseDto> notes = await _service.GetNotes(userId);
            if (notes == null)
            {
                return Ok("No notes created");
            }
            return Ok(notes);
        }

        [HttpGet]
        [Route("get/{noteId}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> RetrieveNoteAsync(int noteId)
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            var note = await _service.GetNote(noteId, userId);
            if (note==null)
            {
                return BadRequest("No such note exists!");
            }
            return Ok(note);
        }

        [HttpPost]
        [Route("add")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> AddNoteASync([FromBody] NoteRequestDto note)
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            var addednote = await _service.AddNote(note, userId);
            if (addednote == null)
            {
                return BadRequest();
            }
            return Ok(note);
        }
    
        [HttpDelete]
        [Route("delete/{noteId}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> DeleteNoteAsync(int noteId)
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            var result = await _service.DeleteNote(noteId, userId);
            if (result == null)
            {
                return BadRequest("No such note Exists!");
            }    
            return Ok("Deleted");
        }
        

        [HttpPost]
        [Route("update/{noteId}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> UpdateNoteAsync(int noteId, [FromBody] NoteRequestDto note)
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            var updatedNote = await _service.UpdateNote(userId, noteId, note);
            if (updatedNote == null)
            {
                return BadRequest("No suh node exist");
            }
            return Ok(updatedNote);
        }
    }
}
