namespace App.Domain.Core.YoutubeVideos.Data.Repositories;

public interface IYoutubeVideoTranscriptAPI
{
    public Task<string> getSubtitleTranscript(string subtitleUrl, CancellationToken cancellationToken);
}
