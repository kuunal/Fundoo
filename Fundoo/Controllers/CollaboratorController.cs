﻿using BusinessLayer.Interface;
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
    [Route("notes/[controller]")]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorService _service;

        public CollaboratorController(ICollaboratorService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("add")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> AddCollabortor([FromBody] Collaborator collaborator)
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            string email = (string) HttpContext.Items["email"];
            if (email == collaborator.email)
            {
                return BadRequest("Cannot collaborate with self");
            }
            Collaborator addedCollaborator = await _service.AddCollaborator(email, userId, collaborator);
            if (addedCollaborator == null)
            {
                return BadRequest("Invalid Note");
            }
            return Ok(collaborator);
        }

        [HttpGet]
        [Route("get")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> GetCollaboratorsAsync()
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            List<Collaborator> collaborators = await _service.GetCollaborators(userId);
            if (collaborators == null)
            {
                return BadRequest("No collaborators");
            }
            return Ok(collaborators);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> RemoveCollaborator(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            Collaborator removedCollaborator = await _service.RemoveCollaborator(id, userId);
            if (removedCollaborator == null)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}