using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundoo.Controllers
{
    public class AccountController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
