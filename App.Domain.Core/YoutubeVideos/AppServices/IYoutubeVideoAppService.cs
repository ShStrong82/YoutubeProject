using App.Domain.Core.YoutubeVideos.DTOs;

namespace App.Domain.Core.YoutubeVideos.AppServices;

public interface IYoutubeVideoAppService
{
    public Task<YoutubeVideoDto?> getVideoDetails(string videoId, CancellationToken cancellationToken);



    public string getNormalizedVideoUrl(string videoUrl);
}
