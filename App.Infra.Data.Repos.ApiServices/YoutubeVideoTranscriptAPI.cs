using App.Domain.Core.YoutubeVideos.Data.Repositories;
using System.Net.Http.Headers;
using System.Web;

namespace App.Infra.Data.Repos.ApiServices;

public class YoutubeVideoTranscriptAPI : IYoutubeVideoTranscriptAPI
{
    private readonly HttpRequestService _httpRequestService;

    public YoutubeVideoTranscriptAPI(HttpRequestService httpRequestService)
    {
        _httpRequestService = httpRequestService;
    }


    // Get subtitle as json format and Pass subtitle url to it
    public async Task<string> getSubtitleTranscript(string subtitleUrl, CancellationToken cancellationToken)
    {
        // Create the HttpRequestMessage

        string encodedSubtitleUrl = HttpUtility.UrlEncode(subtitleUrl);

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://youtube-media-downloader.p.rapidapi.com/v2/video/subtitles?subtitleUrl={encodedSubtitleUrl}&format=json&fixOverlap=true"),
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