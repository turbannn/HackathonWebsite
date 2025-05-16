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
        [HttpGet("/Tasks/DetailedTask/{id}")]
        public async Task<IActionResult> TaskDetailedView(int id)
        {
            var task = await _taskService.GetProfileTaskByIdAsync(id);

            if (task is null)
                return NotFound("Task not found");

            return View(task);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost("/Tasks/CreateTask")]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto hackathonDto)
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

        [Authorize(Roles = "Teacher,Admin")]
        [HttpPut("/Tasks/Rate")]
        public async Task<IActionResult> Rate([FromBody] TaskRatingDto dto)
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

            dto.TeacherIdRatedBy = id;

            var updateResult = await _taskService.UpdateRatingAsync(dto);

            if (!updateResult)
            {
                return Json(new
                {
                    success = false,
                    message = "Rating must be between 0 and 100."
                });
            }

            return Json(new { success = true, redirectUrl = Url.Action("RedirectToIndex", "Rating") });
        }


        [Authorize(Roles = "User,Admin")]
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

        [Authorize(Roles = "User,Admin")]
        [HttpDelete("Tasks/DeleteTask/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _taskService.GetProfileTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound("Task not found");
            }

            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out int userId))
            {
                return Json(new
                {
                    success = false,
                    message = "Invalid user ID"
                });
            }

            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && task.UserId != userId)
            {
                return Json(new
                {
                    success = false,
                    message = "You can only delete your own tasks"
                });
            }

            var deleteResult = await _taskService.DeleteTaskAsync(id);
            if (!deleteResult)
            {
                return Json(new
                {
                    success = false,
                    message = "Task deletion failed"
                });
            }

            if(isAdmin)
                return Json(new { success = true, redirectUrl = Url.Action("RedirectToIndex", "Rating") });

            return Json(new { success = true, redirectUrl = Url.Action("UserProfileView", "Users") });
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
