



using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using TestApp;
using LanguageDetection;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.Xml.Linq;






string path = @"D:\Final_Project_Daneshgah\Backend\TranscriptServiceByPy\newResult.json";
string text_path = @"D:\Final_Project_Daneshgah\Backend\TranscriptServiceByPy\files\output.txt";





//get Subtitle url of Video
static string getSubtitleUrl(string jsonData)
{
    //var data = File.ReadAllText(path);


    var jsonObject = JObject.Parse(jsonData);


    string subtitlesUrl = jsonObject["subtitles"]?["items"]?[0]?["url"]?.ToString();

    return subtitlesUrl;
}

//get Thumbnail in 1080p x 1920p
static string getThumbnailUrl(string jsonData)
{
    //var data = File.ReadAllText(path);


    var jsonObject = JObject.Parse(jsonData);


    string thumbnailUrl = jsonObject["thumbnails"]?
            .FirstOrDefault(t => t["width"]?.ToString() == "1920" && t["height"]?.ToString() == "1080")?["url"]?.ToString();

    return thumbnailUrl;
}

//get title of Video
static string getTitle(string jsonData)
{
    //string data = File.ReadAllText(path, Encoding.UTF8);

    // Convert the string to a byte array using the default encoding
    byte[] defaultEncodedBytes = Encoding.Default.GetBytes(jsonData);

    // Convert the byte array to a UTF-8 encoded string
    string data = Encoding.UTF8.GetString(defaultEncodedBytes);


    var jsonObject = JObject.Parse(data);

    string result = jsonObject["title"]?.ToString();

    return result;
}





// should pass the respones of getSubtitle as json request
// Normalize Transcript (json) With Spaces To a Text (should pass the subtitleUrl to subtitle method of api and then pass the result here)
static string getNormalizedTranscriptJson(string jsonData)
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




// get translation (gemini)
static string getTranslation(string jsonData)
{
    // Convert the string to a byte array using the default encoding
    byte[] defaultEncodedBytes = Encoding.Default.GetBytes(jsonData);

    // Convert the byte array to a UTF-8 encoded string
    string data = Encoding.UTF8.GetString(defaultEncodedBytes);

    var json = JObject.Parse(data);

    var item = json["translations"]?.ToString();

    return item;
}

// get translation (ai translate)
static string getTranslationAI(string jsonData)
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













// Normalize Transcript (srt) With Spaces To a Text
static string normalizeTranscriptSrt(string text)
{
    //if input was null return null as result
    if (string.IsNullOrWhiteSpace(text))
    {
        return string.Empty;
    }

    // Split the input into lines
    string[] lines = text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

    // Use a StringBuilder to build the final result
    var result = new System.Text.StringBuilder();

    // Regular expression to detect time range lines
    string timeRangePattern = @"\d{2}:\d{2}:\d{2}\.\d{3} --> \d{2}:\d{2}:\d{2}\.\d{3}";

    foreach (var line in lines)
    {
        // Skip lines that are either numeric indexes or time ranges
        if (Regex.IsMatch(line, @"^\d+$") || Regex.IsMatch(line, timeRangePattern))
        {
            continue;
        }

        // Otherwise, treat it as a subtitle text line and append it
        result.Append(line.Trim()).Append(" ");
    }

    // Return the final result, trimmed to remove any trailing space
    return result.ToString().Trim();
}





