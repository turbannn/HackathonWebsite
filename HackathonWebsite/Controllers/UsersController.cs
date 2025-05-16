using HackathonWebsite.BLL.DtoEntities.UserDtos;
using HackathonWebsite.BLL.Services;
using HackathonWebsite.DAL.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HackathonWebsite.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _userService;
        private readonly TokenProvider _tokenProvider;

        public UsersController(UserService userService, TokenProvider tokenProvider)
        {
            _userService = userService;
            _tokenProvider = tokenProvider;
        }

        [HttpPost("/User/Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("_t");
            return Ok();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/User/SubmitLogin")]
        public async Task<IActionResult> SubmitLogin([FromBody] UserLogin userLogin)
        {
            var user = await _userService.GetUserByNameAndPasswordAsync(userLogin.Username, userLogin.Password);

            if (user is null)
            {
                return Json(new
                {
                    success = false,
                    message = "User not found Or password does not match"
                });
            }

            var tokenstr = _tokenProvider.CreateAccessToken(user.Id, user.Username, user.Role);

            Response.Cookies.Append("_t", tokenstr, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddMinutes(15)
            });

            return Json(new { success = true, redirectUrl = Url.Action("UserProfileView", "Users") });
        }

        [Authorize(Roles = "User,Teacher,Admin")]
        [HttpGet("/User/UserProfile")]
        public async Task<IActionResult> UserProfileView()
        {
            var idStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "no";

            if (!int.TryParse(idStr, out int id))
            {
                return Json(new
                {
                    success = false,
                    message = "Id parse error"
                });
            }

            var user = await _userService.GetUserByIdAsync(id);

            if (user is null)
            {
                return Json(new
                {
                    success = false,
                    message = "User not found"
                });
            }

            return View(user);
        }

        [Authorize(Roles = "Teacher,Admin")]
        [HttpGet("/User/TeacherProfile")]
        public async Task<IActionResult> TeacherProfileView()
        {
            var idStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "no";

            if (!int.TryParse(idStr, out int id))
            {
                return Json(new
                {
                    success = false,
                    message = "Id parse error"
                });
            }

            var user = await _userService.GetTeacherByIdAsync(id);

            if (user is null)
            {
                return Json(new
                {
                    success = false,
                    message = "Teacher not found"
                });
            }

            return View(user);
        }

        [HttpPost("/User/SubmitRegistration")]
        public async Task<IActionResult> SubmitRegistration([FromBody] UserCreateDto userCreateDto)
        {
            var existingUser = await _userService.GetUserByUsernameAsync(userCreateDto.Username);

            if (existingUser is not null)
            {
                return Json(new
                {
                    success = false,
                    message = "User already exists!"
                });
            }

            var addResult = await _userService.AddUserAsync(userCreateDto);

            if (!addResult)
            {
                return Json(new
                {
                    success = false,
                    message = "User hasn't been added"
                });
            }

            var user = await _userService.GetUserByNameAndPasswordAsync(userCreateDto.Username, userCreateDto.Password);

            if (user is null)
            {
                return Json(new
                {
                    success = false,
                    message = "User not found"
                });
            }

            var tokenstr = _tokenProvider.CreateAccessToken(user.Id, user.Username, user.Role);

            Response.Cookies.Append("_t", tokenstr, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddMinutes(15)
            });

            return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
        }
    }
}
