using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projectSuperMarket.core.Entities;
using projectSuperMarket.Servies.Servies;

namespace projectSuperMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SupplierRegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var token = await _authService.RegisterAsync(model);
            if (token == null)
                return BadRequest("Supplier already exists.");
            return Ok(new { Token = token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SupplierLoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _authService.LoginAsync(model);

            if (token == null)
                return Unauthorized("שם משתמש או סיסמה שגויים");

            return Ok(new { Token = token });
        }
    }
}
