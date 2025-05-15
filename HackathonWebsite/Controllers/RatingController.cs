using AutoMapper;
using HackathonWebsite.BLL.DtoEntities.UserDtos;
using HackathonWebsite.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackathonWebsite.Controllers
{
    public class RatingController : Controller
    {
        private readonly RatingService _ratingService;
        public RatingController(RatingService ratingService)
        {
            _ratingService = ratingService;
        }

        public async Task<IActionResult> RedirectToIndex()
        {
            var AT = Request.Cookies["_t"];

            if (AT is null)
            {
                return RedirectToAction("Login", "Home");
            }

            var ratingTasks = await _ratingService.GetAllRatingTasksAsync();

            return View("Index", ratingTasks);
        }

        [Authorize(Roles = "User,Teacher,Admin")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
