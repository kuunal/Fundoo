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
        public async Task<IActionResult> AddLabelAsync([FromBody] Label label)
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            Label createdLabel = await _service.AddLabelAsync(userId, label);
            return Ok(createdLabel);
        }

        [HttpGet]
        [Route("get")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> GetLabelsAsyc()
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            List<Label> labels = await _service.GetLabelAsync(userId);
            return Ok(labels);
        }

        [HttpDelete]
        [Route("delete/{labelId}")]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> RemoveLabelAsync(int labelId)
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            Label label = await _service.RemoveLabelAsync(userId, labelId);
            return Ok(label);
        }
    }
}
