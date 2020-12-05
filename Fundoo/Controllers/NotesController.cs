using BusinessLayer.Interface;
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
        public IActionResult GetNotes()
        {
            List<Notes> notes = await _service.GetNote();
            if (notes == null)
            {
                return Ok("No notes created");
            }
            return Ok(notes);
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(Note note)
        {
            Note addednote = await _service.AddNote(note);
            if (addednote == null)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
