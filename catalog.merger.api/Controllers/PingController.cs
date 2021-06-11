using Microsoft.AspNetCore.Mvc;

namespace catalog.merger.api.Controllers
{
    public class PingController : Controller
    {
        [HttpGet("ping")]
        public IActionResult Index()
        {
            return Ok("pong");
        }
    }
}