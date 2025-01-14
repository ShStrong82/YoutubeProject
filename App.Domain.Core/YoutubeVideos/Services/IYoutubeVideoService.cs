using App.Domain.Core.YoutubeVideos.DTOs;

namespace App.Domain.Core.YoutubeVideos.Services;

public interface IYoutubeVideoService
{
    public Task<YoutubeVideoDto?> getVideoDetails(string videoId, CancellationToken cancellationToken); //get videoId from Endpoint of link that user set





    // Addititonal Methods to get specefic Data

    //VideoDetails methods
    public string? getThumbnailUrl(string jsonData);
    public string? getTitle(string jsonData);
    //get subtitle url from json (get from api)
    public string? getSubtitleUrl(string jsonData);
    
    
    
    
    // Normalize Transcript (json) With Spaces To a Text (should pass the respond of YoutubeVideoTranscriptAPI that is in json format)
    public string? getNormalizedTranscriptJson(string jsonData);


    
    
    // get Translation (pass the json data and get the text property)
    public string? getTranslationTextGemini(string jsonData);

    public string? getTranslationAI(string jsonData);
}
