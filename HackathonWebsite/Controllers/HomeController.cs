using HackathonWebsite.BLL.Services;
using HackathonWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HackathonWebsite.BLL.DtoEntities.UserDtos;

namespace HackathonWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserService _userService;

        public HomeController(ILogger<HomeController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/Home/TryLogin")]
        public IActionResult Login()
        {
            var AT = Request.Cookies["_t"];

            if (AT is null)
            {
                return View();
            }

            return RedirectToAction("UserProfileView", "Users");
        }

        [HttpGet("/Home/Register")]
        public IActionResult RegistrationView()
        {
            return View("RegistrationView");
        }

        [HttpGet("/Home/LogRegView")]
        public IActionResult LogRegView()
        {
            return View();
        }
        public async Task<IActionResult> Test()
        {
            Console.WriteLine("Test Executed!");
            //await _userService.AddUserAsync(new UserCreateDto{Id = 0, Role = "User", Username = "testUser", Password = "qwerty123"});
            return Ok();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
