using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Up.NET.Models;

namespace Up.NET.Api
{
    public partial class UpApi
    {
        private readonly HttpClient _httpClient;
        private string _accessToken;
        private bool _enableRetry;
        private int _maxRetries;
        private static readonly string BaseUrl = "https://api.up.com.au/api/v1";

        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        
        public UpApi(string accessToken, HttpClient httpClient = null, bool enableRetry = true, int maxRetries = 5)
        {
            _accessToken = accessToken;
            _httpClient = httpClient ?? new HttpClient();
            _enableRetry = enableRetry;
            _maxRetries = maxRetries;
        }

        private async Task<UpResponse<T>> SendRequestAsync<T>(HttpMethod httpMethod, string relativeUrl, Dictionary<string, string> queryParameters = null, object content = null) where T : class
        {
            var uri = new Uri($"{BaseUrl}{relativeUrl}");

            if (queryParameters != null)
            {
                foreach (var (key, value) in queryParameters)
                {
                    uri = uri.AddQueryParameter(key, value);
                }
            }

            var httpRequestMessage = new HttpRequestMessage(httpMethod, uri);
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            if (content != null)
            {
                httpRequestMessage.Content = JsonContent.Create(content);
            }
            
            var currentRetryCount = 0;
            do
            {
                var response = await _httpClient.SendAsync(httpRequestMessage);

                // On POST just check HTTP status code
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    // Will create a new EmptyResponse, so the IsSuccess property works
                    // Otherwise null for failure
                    return UpResponse.FromSuccess(Activator.CreateInstance<T>());
                }
                
                var outputStream = await response.Content.ReadAsStreamAsync();

                if (response.IsSuccessStatusCode)
                {
                    var output = await JsonSerializer.DeserializeAsync<T>(outputStream, _jsonSerializerOptions);

                    return UpResponse.FromSuccess(output);
                }
                
                // If its a user error, don't retry (401 unauthorized etc)
                if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500 || 
                    !_enableRetry || currentRetryCount >= _maxRetries)
                {
                    var output = await JsonSerializer.DeserializeAsync<ErrorWrapper>(outputStream, _jsonSerializerOptions);
                    
                    return UpResponse.FromFail<T>(output?.Errors);
                }

                currentRetryCount++;
            } while (true);
        }
        

    }
}