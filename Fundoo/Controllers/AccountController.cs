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
    [Route("account/")]
    public class AccountController : ControllerBase
    {
        private IAccountService _service;

        public AccountController(IAccountService services)
        {
            _service = services;
        }

        public async Task<IActionResult> Index()
        {
            return Ok(await _service.Get());
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddAccount([FromBody] Account account)
        {
            Account createdAccount = await _service.AddAccount(account);
            if (createdAccount == null)
            {
                return BadRequest("Error");
            }
            return Ok(createdAccount);
        }
    }
}
