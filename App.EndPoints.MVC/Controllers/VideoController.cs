using App.Domain.Core.YoutubeVideos.AppServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace App.EndPoints.MVC.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class VideoController : Controller
    {
        private readonly IYoutubeVideoAppService _youtubeVideoAppService;
        public VideoController(IYoutubeVideoAppService youtubeVideoAppService)
        {
            _youtubeVideoAppService = youtubeVideoAppService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> getVideoDetails(string? videoId, CancellationToken cancellationToken)
        {
            var dataModel = await _youtubeVideoAppService.getVideoDetails(videoId, cancellationToken);

            return View(dataModel);


        }

        //[AllowAnonymous]
        //[HttpGet("{videoId}")]
        //public async Task<IActionResult> getVideoDetails(string videoId, CancellationToken cancellationToken)
        //{
        //    var data = await _youtubeVideoAppService.getVideoDetails(videoId, cancellationToken);
        //    return View(data);
        //}
    }
}
