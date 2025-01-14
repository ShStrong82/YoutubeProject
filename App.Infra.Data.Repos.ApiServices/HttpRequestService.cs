using System.Net.Http.Headers;

namespace App.Infra.Data.Repos.ApiServices;

public class HttpRequestService
{
    private readonly HttpClient _httpClient;

    public HttpRequestService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> SendRequestAsync(HttpRequestMessage request)
    {

        try
        {
            // Send the request
            var response = await _httpClient.SendAsync(request);

            // Ensure the response was successful
            response.EnsureSuccessStatusCode();


            // Read and return the response content
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            // Log or handle the exception as needed
            throw new Exception($"Request failed: {ex.Message}", ex);
        }

        

    }
}
