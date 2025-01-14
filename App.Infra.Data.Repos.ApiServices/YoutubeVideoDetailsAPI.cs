using App.Domain.Core.YoutubeVideos.Data.Repositories;
using System.Net.Http.Headers;

namespace App.Infra.Data.Repos.ApiServices;

public class YoutubeVideoDetailsAPI : IYoutubeVideoDetailsAPI
{
    private readonly HttpRequestService _httpRequestService;

    public YoutubeVideoDetailsAPI(HttpRequestService httpRequestService)
    {
        _httpRequestService = httpRequestService;
    }

    // VideoDetails as json format
    // Get Video Details (title, thumbnail, subtitle url)
    public async Task<string> getVideoDetails(string videoId, CancellationToken cancellationToken)
    {
        // Create the HttpRequestMessage
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://youtube-media-downloader.p.rapidapi.com/v2/video/details?videoId={videoId}"),
            Headers =
            {
                { "x-rapidapi-key", "807ed0bfb4mshf54491a9baeeb29p1ff6ffjsn290f07202933" },
                { "x-rapidapi-host", "youtube-media-downloader.p.rapidapi.com" },
            },
        };

        // Send the request using HttpRequestService
        var responseContent = await _httpRequestService.SendRequestAsync(request);

        return responseContent;
    }

}

