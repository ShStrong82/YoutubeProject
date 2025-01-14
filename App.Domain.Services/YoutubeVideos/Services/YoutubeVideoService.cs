using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using App.Domain.Core.YoutubeVideos.Data.Repositories;
using App.Domain.Core.YoutubeVideos.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Python.Runtime;
using LanguageDetection;
using static System.Net.Mime.MediaTypeNames;
using App.Domain.Core.YoutubeVideos.DTOs;


namespace App.Domain.Services.YoutubeVideos.Services;

public class YoutubeVideoService : IYoutubeVideoService
{
    private readonly IYoutubeVideoDetailsAPI _youtubeVideoDetailsAPI;
    private readonly IYoutubeVideoTranscriptAPI _youtubeVideoTranscriptAPI;
    private readonly ITranscriptTranslateAPI _transcriptTranslateAPI;

    public YoutubeVideoService(IYoutubeVideoDetailsAPI youtubeVideoDetailsAPI, IYoutubeVideoTranscriptAPI youtubeVideoTranscriptAPI, ITranscriptTranslateAPI transcriptTranslateAPI)
    {
        _youtubeVideoDetailsAPI = youtubeVideoDetailsAPI;
        _youtubeVideoTranscriptAPI = youtubeVideoTranscriptAPI;
        _transcriptTranslateAPI = transcriptTranslateAPI;
    }



    //get Video Dto
    public async Task<YoutubeVideoDto?> getVideoDetails(string videoId, CancellationToken cancellationToken) //get videoId from Endpoint of link that user set
    {

        var videoDetails = await _youtubeVideoDetailsAPI.getVideoDetails(videoId, cancellationToken);

        //video details
        var titleVideo = getTitle(videoDetails);

        var thumbnailUrl = getThumbnailUrl(videoDetails);

        var subtitleUrl = getSubtitleUrl(videoDetails);


        var youtubeVideoTranscript = await _youtubeVideoTranscriptAPI.getSubtitleTranscript(subtitleUrl, cancellationToken);

        var original_Transcript = getNormalizedTranscriptJson(youtubeVideoTranscript);



        // set condition for when original_Transcript is not persian translate, else if doesn't translate
        var detector = new LanguageDetector();

        detector.AddAllLanguages();

        var detectedLang = detector.Detect(original_Transcript);

        var translated_Transcript = "";

        if (detectedLang != "fas")
        {
            var transcriptTranslate = await _transcriptTranslateAPI.getTranslatedTranscript(original_Transcript, cancellationToken);
            translated_Transcript = getTranslationAI(transcriptTranslate);
        }


        var youtubeVideoDto = new YoutubeVideoDto()
        {
            Title = titleVideo,
            ThumbnailSrc = thumbnailUrl,
            OrginalTranscript = original_Transcript,
            TranslatedTranscript = translated_Transcript,
        };


        return youtubeVideoDto;

    }






    // Additional methods to get specefic data
    public string? getTitle(string jsonData) 
    {
        // Convert the string to a byte array using the default encoding
        byte[] defaultEncodedBytes = Encoding.Default.GetBytes(jsonData);

        // Convert the byte array to a UTF-8 encoded string
        string data = Encoding.UTF8.GetString(defaultEncodedBytes);


        var jsonObject = JObject.Parse(data);

        string result = jsonObject["title"]?.ToString();

        return result;
    }

    public string? getThumbnailUrl(string jsonData)
    {
        var jsonObject = JObject.Parse(jsonData);


        string thumbnailUrl = jsonObject["thumbnails"]?
                .FirstOrDefault(t => t["width"]?.ToString() == "1920" && t["height"]?.ToString() == "1080")?["url"]?.ToString();

        return thumbnailUrl;
    }

    public string? getSubtitleUrl(string jsonData)
    {
        var jsonObject = JObject.Parse(jsonData);


        string subtitlesUrl = jsonObject["subtitles"]?["items"]?[0]?["url"]?.ToString();

        return subtitlesUrl;
    }

    //Ai Translation | method getText
    public string? getTranslationAI(string jsonData)
    {
        // Convert the string to a byte array using the default encoding
        byte[] defaultEncodedBytes = Encoding.Default.GetBytes(jsonData);

        // Convert the byte array to a UTF-8 encoded string
        string data = Encoding.UTF8.GetString(defaultEncodedBytes);

        var json = JObject.Parse(data);

        // Extract the "texts" array
        var textsArray = json["texts"]?.ToObject<string[]>();

        // Join the array elements into a single string (if needed)
        // For example, join with a newline or space
        string result = textsArray != null ? string.Join("\n", textsArray) : string.Empty;

        return result;
    }


    //Gemini Translation
    public string? getTranslationTextGemini(string jsonData)
    {
        // Convert the string to a byte array using the default encoding
        byte[] defaultEncodedBytes = Encoding.Default.GetBytes(jsonData);

        // Convert the byte array to a UTF-8 encoded string
        string data = Encoding.UTF8.GetString(defaultEncodedBytes);

        var json = JObject.Parse(data);

        var item = json["translations"]?.ToString();

        return item;
    }

    
    public string? getNormalizedTranscriptJson(string jsonData)
    {
        try
        {
            //string jsonContent = File.ReadAllText(path, Encoding.UTF8);

            // Convert the string to a byte array using the default encoding
            byte[] defaultEncodedBytes = Encoding.Default.GetBytes(jsonData);

            // Convert the byte array to a UTF-8 encoded string
            string jsonContent = Encoding.UTF8.GetString(defaultEncodedBytes);

            // Parse the JSON using Newtonsoft.Json
            JArray jsonArray = JArray.Parse(jsonContent);

            // Initialize a StringBuilder to concatenate the text
            StringBuilder normalizedText = new StringBuilder();

            // Iterate through each object in the array
            foreach (var item in jsonArray)
            {
                // Extract the "text" field and normalize it
                string text = item["text"]?.ToString() ?? string.Empty;

                // Add normalized text to the StringBuilder
                normalizedText.Append(text).Append(" ");
            }

            // Return the concatenated and trimmed result
            return normalizedText.ToString().Trim();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return string.Empty;
        }
    }



    // get Audio of youtube by save on path

    //private static bool _isPythonInitialized = false;
    //public static void InitializePython()
    //{
    //    if (!_isPythonInitialized) // Ensure Python is initialized only once
    //    {
    //        Runtime.PythonDLL = @"C:\Program Files\Python312\python312.dll";
    //        PythonEngine.Initialize();
    //        _isPythonInitialized = true;
    //    }
    //}

    //get video audio as mp3 and save it on python files | scriptName is python file name
    //public void getAudioVidoeAsMp3(string scriptName, string video_Url)
    //{
    //    InitializePython();
    //    //Runtime.PythonDLL = @"C:\Program Files\Python312\python312.dll";
    //    //PythonEngine.Initialize();

    //    using (Py.GIL())
    //    {
    //        dynamic sys = Py.Import("sys");
    //        sys.path.append(@"D:\Final_Project_Daneshgah\Youtube_Project\YoutubeProject\App.Domain.Services\YoutubeVideos\Services");

    //        PyObject pythonScript = Py.Import(scriptName);


    //        PyString videoUrl = new PyString(video_Url);



    //        PyObject pyObjResult = pythonScript.InvokeMethod("download_audio_as_mp3", new PyObject[] { videoUrl });
    //    }
    //}

}
