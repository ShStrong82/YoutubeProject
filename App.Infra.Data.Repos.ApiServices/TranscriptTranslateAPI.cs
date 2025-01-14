using App.Domain.Core.YoutubeVideos.Data.Repositories;
using System;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

namespace App.Infra.Data.Repos.ApiServices;

public class TranscriptTranslateAPI : ITranscriptTranslateAPI
{
    private readonly HttpRequestService _httpRequestService;

    public TranscriptTranslateAPI(HttpRequestService httpRequestService)
    {
        _httpRequestService = httpRequestService;
    }

    // get Translation as json format
    public async Task<string> getTranslatedTranscript(string textToTranslate, CancellationToken cancellationToken)
    {
        // Create the HttpRequestMessage


        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://ai-translate.p.rapidapi.com/translate"),
            Headers =
            {
                { "x-rapidapi-key", "807ed0bfb4mshf54491a9baeeb29p1ff6ffjsn290f07202933" },
                { "x-rapidapi-host", "ai-translate.p.rapidapi.com" },
            },
            Content = new StringContent($"{{\"texts\":[\"{textToTranslate}\"],\"tl\":\"fa\",\"sl\":\"auto\"}}")
            {
                Headers =
                {   
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
            }
        };

        var responseContent = await _httpRequestService.SendRequestAsync(request);

        return responseContent;



        //Gemini Translate
        //var request = new HttpRequestMessage
        //{
        //    Method = HttpMethod.Post,
        //    RequestUri = new Uri("https://gemini-translate.p.rapidapi.com/translate"),
        //    Headers =
        //    {
        //        { "x-rapidapi-key", "807ed0bfb4mshf54491a9baeeb29p1ff6ffjsn290f07202933" },
        //        { "x-rapidapi-host", "gemini-translate.p.rapidapi.com" },
        //    },
        //    Content = new StringContent($"{{\"text\":\"{textToTranslate}\",\"to\":\"persian\"}}")
        //    {
        //        Headers =
        //        {
        //            ContentType = new MediaTypeHeaderValue("application/json")
        //        }
        //    }
        //};

        //// Send the request using HttpRequestService

        //var responseContent = await _httpRequestService.SendRequestAsync(request);


        //return responseContent;
    }


}


