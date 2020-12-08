using BusinessLayer.Interface;
using Greeting.TokenAuthentication;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using ModelLayer.DTOs.LabelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundoo.Controllers
{
    [ApiController]
    [Route("/notes/[controller]")]
    public class LabelController : ControllerBase
    {
        private readonly ILabelService _service;

        public LabelController(ILabelService service)
        {
            _service = service;
        }

        [HttpPost]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> AddLabelAsync([FromBody] LabelRequestDto label)
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            var createdLabel = await _service.AddLabelAsync(userId, label);
            return Ok(createdLabel);
        }

        [HttpGet]
        [Route("get")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> GetLabelsAsyc()
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            var labels = await _service.GetLabelAsync(userId);
            return Ok(labels);
        }

        [HttpDelete]
        [Route("delete/{labelId}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> RemoveLabelAsync(int labelId)
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            var label = await _service.RemoveLabelAsync(userId, labelId);
            return Ok(label);
        }
    }
}
