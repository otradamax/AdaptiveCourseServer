using AdaptiveCourseServer.CheckSolution;
using AdaptiveCourseServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdaptiveCourseServer.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [HttpPost]
        public IActionResult LogicScheme([FromBody] Dictionary<string, List<string>> OrientedGraph)
        {
            bool result = CheckSolution.CheckSolution.Solution(OrientedGraph);
            return Content(result.ToString());
        }
    }
}
