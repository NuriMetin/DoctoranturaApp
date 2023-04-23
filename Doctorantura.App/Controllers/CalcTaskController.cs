using Doctorantura.App.Models;
using Doctorantura.App.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Doctorantura.App.Controllers
{
    public class CalcTaskController : Controller
    {
        private readonly CalcTaskManager _calcTaskManager;
        private readonly UserManager<AppUser> _userManager;

        public CalcTaskController(CalcTaskManager calcTaskManager, UserManager<AppUser> userManager)
        {
            _calcTaskManager = calcTaskManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> TaskList()
        {
            var data = await _calcTaskManager.GetAllAsync();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string taskName)
        {
            var user = await  _userManager.FindByNameAsync(User.Identity.Name);

            await _calcTaskManager.CreateTaskAsync(taskName, user.Id);

            return RedirectToAction("TaskList","CalcTask");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int taskId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            await _calcTaskManager.DeleteTaskByIdAsync(taskId, user.Id);

            return RedirectToAction("TaskList", "CalcTask");
        }
    }
}
