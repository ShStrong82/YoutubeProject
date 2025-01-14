using App.Domain.Core.YoutubeVideos.AppServices;
using App.Domain.Core.YoutubeVideos.DTOs;
using App.Domain.Core.YoutubeVideos.Services;

namespace App.Domain.AppServices.YoutubeVideos.AppServices;

public class YoutubeVideoAppService : IYoutubeVideoAppService
{
    private readonly IYoutubeVideoService _youtubeVideoService;
    public YoutubeVideoAppService(IYoutubeVideoService youtubeVideoService)
    {
        _youtubeVideoService = youtubeVideoService;
    }

    public async Task<YoutubeVideoDto?> getVideoDetails(string videoId, CancellationToken cancellationToken)
    {
        //var normalizedUrl = getNormalizedVideoUrl(videoId);

        return await _youtubeVideoService.getVideoDetails(videoId, cancellationToken);
    }



    public string getNormalizedVideoUrl(string videoUrl)
    {
        var result = videoUrl.Replace("https://www.youtube.com/watch?v=", "");

        return result;
    }
}
