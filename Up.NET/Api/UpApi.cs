using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Up.NET.Models;

#nullable enable

namespace Up.NET.Api;

public partial class UpApi : IUpApi
{
    private static readonly string BaseUrl = "https://api.up.com.au/api/v1";
    private readonly HttpClient _httpClient;

    private readonly string _accessToken;
    private readonly bool _enableRetry;
    private readonly int _maxRetries;

    public UpApi(string accessToken, HttpClient? httpClient = null, bool enableRetry = true, int maxRetries = 5)
    {
        _accessToken = accessToken;
        _httpClient = httpClient ?? new HttpClient();
        _enableRetry = enableRetry;
        _maxRetries = maxRetries;
    }

    public async Task<UpResponse<T>> SendRequestAsync<T>(HttpMethod httpMethod, string relativeUrl,
        Dictionary<string, string>? queryParameters = null, object? content = null, bool urlIsAbsolute = false)
        where T : class
    {
        var uri = urlIsAbsolute
            ? new Uri(relativeUrl)
            : new Uri($"{BaseUrl}{relativeUrl}");

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
            var jsonTypeInfo = UpJsonContext.Default.GetTypeInfo(content.GetType())
                ?? throw new InvalidOperationException($"Type {content.GetType()} is not registered in UpJsonContext");
            var json = JsonSerializer.Serialize(content, jsonTypeInfo);
            httpRequestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        var currentRetryCount = 0;
        do
        {
            var response = await _httpClient.SendAsync(httpRequestMessage);

            // On POST just check HTTP status code
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                // NoResponse is the only type that can be returned for NoContent
                // We create it directly instead of using Activator.CreateInstance
                if (typeof(T) == typeof(NoResponse))
                {
                    return UpResponse.FromSuccess((T)(object)new NoResponse());
                }
                
                throw new InvalidOperationException($"Received NoContent response but expected type {typeof(T).Name}");
            }

            var outputStream = await response.Content.ReadAsStreamAsync();

            if (response.IsSuccessStatusCode)
            {
                var jsonTypeInfo = (JsonTypeInfo<T>?)UpJsonContext.Default.GetTypeInfo(typeof(T))
                    ?? throw new InvalidOperationException($"Type {typeof(T)} is not registered in UpJsonContext");
                var output = await JsonSerializer.DeserializeAsync(outputStream, jsonTypeInfo);

                return UpResponse.FromSuccess(output!);
            }

            // If its a user error, don't retry (401 unauthorized etc)
            if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500 ||
                !_enableRetry || currentRetryCount >= _maxRetries)
            {
                var output = await JsonSerializer.DeserializeAsync(outputStream, UpJsonContext.Default.ErrorWrapper);

                return UpResponse.FromFail<T>(output?.Errors);
            }

            currentRetryCount++;
        } while (true);
    }

    public async Task<UpResponse<PaginatedDataResponse<T>>> SendPaginatedRequestAsync<T>(HttpMethod httpMethod,
        string relativeUrl, Dictionary<string, string>? queryParameters = null, object? content = null,
        bool urlIsAbsolute = false) where T : class
    {
        var output =
            await SendRequestAsync<PaginatedDataResponse<T>>(httpMethod, relativeUrl, queryParameters, content,
                urlIsAbsolute);

        if (output.Response is not null)
        {        
            output.Response.UpApi = this;
        }
        

        return output;
    }
}
