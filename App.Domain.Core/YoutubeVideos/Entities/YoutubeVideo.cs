namespace App.Domain.Core.YoutubeVideos.Entities;

public class YoutubeVideo
{
    //public int Id { get; set; }
    public string Title { get; set; }

    //public string AudioSrc { get; set; } //link of audio or path of audio

    public string ThumbnailSrc { get; set; } //link of thumbnail or path of thumbnail

    // original transcript
    public string OrginalTranscript { get; set; }

    // translated transcript
    public string TranslatedTranscript { get; set; }


    public string? VideoSrc { get; set; }


    // summary of original transcript
    //public string SummarizedTranscript { get; set; }

    // summary of translated transcript
    //public string SummarizedTranslatedTranscript { get; set; }
}
