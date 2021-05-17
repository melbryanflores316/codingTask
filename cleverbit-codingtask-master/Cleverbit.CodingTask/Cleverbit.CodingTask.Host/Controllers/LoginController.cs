using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cleverbit.CodingTask.Data;
using Microsoft.AspNetCore.Authorization;

namespace Cleverbit.CodingTask.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly CodingTaskContext _context;

        public LoginController(CodingTaskContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Login()
        {
            return Ok(new
            {
                isLoggedIn = true
            });
        }
    }
}
