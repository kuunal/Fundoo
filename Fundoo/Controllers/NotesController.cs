using BusinessLayer.Interface;
using Caching;
using Fundoo.Utilities;
using Greeting.TokenAuthentication;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using ModelLayer.DTOs.NoteDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                return Ok(new
                {
                    Data = (string)null,
                    StatusCode = HttpStatusCode.OK,
                    Message = ResponseMessages.NO_NOTES
                });
            }
            return Ok(new { Data = notes, StatusCode = (int)HttpStatusCode.OK, ResponseMessages.SUCCESS });
        }

        [HttpGet]
        [Route("{noteId}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> RetrieveNoteAsync(int noteId)
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            var note = await _service.GetNote(noteId, userId);
            if (note == null)
            {
                return NotFound(new
                {
                    Data = (string)null,
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = ResponseMessages.NO_SUCH_NOTES
                });
            }
            return Ok(note);
        }

        [HttpPost]
        [TokenAuthenticationFilter]
        [Cached(600)]
        public async Task<IActionResult> AddNoteASync([FromBody] NoteRequestDto note)
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            var addednote = await _service.AddNote(note, userId);
            return Ok(new
            {
                Data = note,
                StatusCode = (int)HttpStatusCode.Created,
                Message = ResponseMessages.CREATED
            });
        }

        [HttpDelete]
        [Route("delete/{noteId}")]
        [TokenAuthenticationFilter]
        [Cached(600)]
        public async Task<IActionResult> DeleteNoteAsync(int noteId)
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            var result = await _service.DeleteNote(noteId, userId);
            if (result == null)
            {
                return NotFound(new
                {
                    Data = (string)null,
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = ResponseMessages.NO_SUCH_NOTES
                });
            }
            return Ok(new
            {
                Data = result,
                StatusCode = (int)HttpStatusCode.OK,
                Message = ResponseMessages.DELETED
            });
        }


        [HttpPost]
        [Route("{noteId}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> UpdateNoteAsync(int noteId, [FromBody] NoteRequestDto note)
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            var updatedNote = await _service.UpdateNote(userId, noteId, note);
            if (updatedNote == null)
            {
                return NotFound(new
                {
                    Data = (string)null,
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = ResponseMessages.NO_SUCH_NOTES
                });
            }
            return Ok(new
            {
                Data = updatedNote,
                StatusCode = (int)HttpStatusCode.OK,
                Message = ResponseMessages.UPDATED
            });
        }
    }
}
