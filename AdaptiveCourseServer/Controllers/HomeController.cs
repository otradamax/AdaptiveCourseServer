using AdaptiveCourseServer.CheckSolution;
using AdaptiveCourseServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdaptiveCourseServer.Controllers
{
    public class HomeController : Controller
    {
        private Context db;

        public HomeController(Context context)
        {
            db = context;
        }

        [Authorize]
        [HttpPost]
        public IActionResult LogicScheme([FromBody] CheckScheme checkScheme)
        {
            SchemeTask task = db.SchemeTasks.FirstOrDefault(t => t.Id == checkScheme.Id);
            bool result = CheckSolution.CheckSolution.Solution(checkScheme.OrientedGraph,
                task.ExpectedOutput, task.InputsNumber);
            return Content(result.ToString());
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetTask()
        {
            SchemeTask task = db.SchemeTasks.FirstOrDefault(t => t.Id == 5);
            return Json(task);
        }
    }
}
