using AutoMapper;
using HackathonWebsite.BLL.DtoEntities.UserDtos;
using HackathonWebsite.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackathonWebsite.Controllers
{
    public class RatingController : Controller
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;
        public RatingController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> RedirectToIndex()
        {
            var AT = Request.Cookies["_t"];

            if (AT is null)
            {
                return RedirectToAction("Login", "Home");
            }

            var users = await _userService.GetAllUsersAsync();

            var ratings = _mapper.Map<List<UserRating>>(users);

            return View("Index", ratings);
        }

        [Authorize(Roles = "User,Teacher,Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();

            var ratings = _mapper.Map<List<UserRating>>(users);

            return View(ratings);
        }
    }
}
