namespace App.Domain.Core.YoutubeVideos.Data.Repositories;

public interface ITranscriptTranslateAPI
{
    public Task<string> getTranslatedTranscript(string textToTranslate, CancellationToken cancellationToken);
}
