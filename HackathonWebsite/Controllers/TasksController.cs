using Microsoft.AspNetCore.Mvc;

namespace HackathonWebsite.Controllers
{
    public class TasksController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
