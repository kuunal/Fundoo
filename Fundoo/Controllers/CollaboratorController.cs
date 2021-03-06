﻿using BusinessLayer.Interface;
using Fundoo.Utilities;
using Greeting.TokenAuthentication;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using ModelLayer.DTOs.CollaboratorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        [TokenAuthenticationFilter]
        public async Task<IActionResult> AddCollabortor([FromBody] CollaboratorRequestDto collaborator)
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            string email = (string)HttpContext.Items["email"];

            var addedCollaborator = await _service.AddCollaborator(email, userId, collaborator);
            return Ok(new
            {
                Data = collaborator,
                StatusCode = (int)HttpStatusCode.Created,
                Message = ResponseMessages.SUCCESS
            });
        }

        [HttpGet]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> GetCollaboratorsAsync()
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            var collaborators = await _service.GetCollaborators(userId);
            if (collaborators == null)
            {
                return NotFound(new
                {
                    Data = (string)null,
                    StatusCode = (int)HttpStatusCode.NotFound,
                    ResponseMessages.NO_COLLABORATOR
                });
            }
            return Ok(new
            {
                Data = collaborators,
                StatusCode = (int)HttpStatusCode.OK,
                ResponseMessages.SUCCESS
            });
        }

        [HttpDelete]
        [Route("{id}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> RemoveCollaborator(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            var removedCollaborator = await _service.RemoveCollaborator(id, userId);
            if (removedCollaborator == null)
            {
                return NotFound(new
                {
                    Data = (string)null,
                    StatusCode = (int)HttpStatusCode.NotFound,
                    ResponseMessages.NO_SUCH_COLLABORATOR
                });
            }
            return Ok(new
            {
                Data = removedCollaborator,
                StatusCode = (int)HttpStatusCode.OK,
                ResponseMessages.DELETED
            });
        }
    }
}
