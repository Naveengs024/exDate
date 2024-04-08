using Microsoft.AspNetCore.Mvc;
using exDate.Service;
using System.Threading.Tasks;
using exDate.Core;
using exDate.Models;

namespace exDate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserDetails()
        {
            var details = await _loginService.GetUserDetails();
            return Ok(details);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductProcess(Login Details)
        {
            var details = await _loginService.CreateProductProcess(Details);
            return Ok(details);
        }
    }
}
