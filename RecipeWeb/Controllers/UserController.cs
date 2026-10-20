using AutoMapper;
using BLL;
using BLL.Services;
using DAL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecipeWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        // POST api/user/register
        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            if (!_userService.Register(dto))
                return Conflict("Email is already registered.");

            // ההרשמה רק יוצרת את המשתמש. את הטוקן מקבלים בהתחברות
            return Ok();
        }

        // POST api/user/login
        [HttpPost("login")]
        public ActionResult<AuthResponseDto> Login(LoginDto dto)
        {
            var result = _userService.Login(dto);
            if (result is null)
                return Unauthorized("Wrong email or password.");

            return Ok(result);
        }
    }
}

