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
    [Route("account/")]
    public class AccountController : ControllerBase
    {
        private IAccountService _service;

        public AccountController(IAccountService services)
        {
            _service = services;
        }

        [HttpGet()]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> Index()
        {
            int id = Convert.ToInt32(HttpContext.Items["userId"]);
            return Ok(await _service.Get(id));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddAccount([FromBody] Account account)
        {
            Account createdAccount = await _service.AddAccount(account);
            if (createdAccount == null)
            {
                return BadRequest("Account already exists");
            }
            return Ok(createdAccount);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(string id, string password)
        {
            if (id == null || password == null)
            {
                return BadRequest("Invalid inputs");
            }
            var (user, token) = await _service.Authenticate(id, password);
            return Ok(new { user = user, token = token });
        }

    }
}
