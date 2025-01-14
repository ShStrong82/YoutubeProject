using Microsoft.AspNetCore.Mvc;

namespace App.Infra.Data.Repos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YoutubeVideoController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
