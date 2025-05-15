using HackathonWebsite.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HackathonWebsite.BLL.DtoEntities.HackathonTaskDtos;

namespace HackathonWebsite.Controllers
{
    public class TasksController : Controller
    {
        private readonly HackathonTaskService _taskService;

        public TasksController(HackathonTaskService taskService)
        {
            _taskService = taskService;
        }

        [Authorize(Roles = "User,Teacher,Admin")]
        [HttpPost("/Tasks/CreateTask")]
        public async Task<IActionResult> CreateExpense([FromBody] TaskCreateDto hackathonDto)
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

            hackathonDto.UserId = id;

            var creationTaskResult = await _taskService.AddTaskAsync(hackathonDto);

            if (!creationTaskResult)
            {
                return Json(new
                {
                    success = false,
                    message = "Internal server error"
                });
            }

            return Json(new { success = true, redirectUrl = Url.Action("UserProfileView", "Users") });
        }

        [Authorize(Roles = "User,Teacher,Admin")]
        [HttpGet("/Tasks/CreateTask")]
        public IActionResult CreateTask()
        {
            return View();
        }

        [Authorize(Roles = "User,Teacher,Admin")]
        [HttpPut("/Tasks/EditTask/{id}")]
        public async Task<IActionResult> EditTask([FromBody] TaskUpdateDto hackathonDto)
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

            hackathonDto.UserId = id;

            var updateTaskResult = await _taskService.UpdateTaskAsync(hackathonDto);

            if (!updateTaskResult)
                return BadRequest("Invalid data");

            return Json(new { success = true, redirectUrl = Url.Action("UserProfileView", "Users") });
        }

        [Authorize(Roles = "User,Teacher,Admin")]
        [HttpGet("/Tasks/EditTask/{id}")]
        public async Task<IActionResult> EditTask(int id)
        {
            var expense = await _taskService.GetProfileTaskByIdAsync(id);

            if (expense == null) return NotFound();

            return View(expense);
        }

        [Authorize(Roles = "User,Teacher,Admin")] //look
        [HttpGet("/Tasks/GetTask/{id}")]
        public async Task<IActionResult> GetExpense(int id)
        {
            var expense = await _taskService.GetProfileTaskByIdAsync(id);

            if (expense == null)
                return NotFound(new { success = false, message = "Expense not found" });

            return Ok(expense);
        }

        [Authorize(Roles = "User,Teacher,Admin")] //look
        [HttpDelete("Tasks/DeleteTask/{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var deleteTaskResult = await _taskService.DeleteTaskAsync(id);

            if (!deleteTaskResult)
                return BadRequest("Invalid data");

            return Json(new { success = true, redirectUrl = Url.Action("UserProfileView", "Users") });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
