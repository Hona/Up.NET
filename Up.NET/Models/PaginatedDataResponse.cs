using Up.NET.Api;

namespace Up.NET.Models;

public class PaginatedDataResponse<T> where T : class
{
    public List<T> Data { get; set; }

    public PaginatedLinks Links { get; set; }
    internal IUpApi UpApi { get; set; }

    public async Task<UpResponse<PaginatedDataResponse<T>>> GetNextPageAsync() =>
        await UpApi.SendPaginatedRequestAsync<T>(HttpMethod.Get, Links.Next, urlIsAbsolute: true);

    public async Task<UpResponse<PaginatedDataResponse<T>>> GetPreviewPageAsync() =>
        await UpApi.SendPaginatedRequestAsync<T>(HttpMethod.Get, Links.Previous, urlIsAbsolute: true);
}