namespace App.Domain.Core.YoutubeVideos.Data.Repositories;

public interface IYoutubeVideoDetailsAPI
{
    public Task<string> getVideoDetails(string videoId, CancellationToken cancellationToken);
}
