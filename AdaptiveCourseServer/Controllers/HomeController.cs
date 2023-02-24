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
            bool result = CheckSolution.CheckSolution.Solution(checkScheme.OrientedGraph, 
                db.SchemeTasks.FirstOrDefault(t => t.Id == checkScheme.Id).ExpectedOutput);
            return Content(result.ToString());
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetTask()
        {
            SchemeTask task = db.SchemeTasks.FirstOrDefault(t => t.Id == 1);
            return Json(task);
        }
    }
}
