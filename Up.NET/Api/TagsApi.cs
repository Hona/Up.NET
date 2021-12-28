using Up.NET.Api.Tags;
using Up.NET.Models;

namespace Up.NET.Api;

public partial class UpApi
{
    public async Task<UpResponse<PaginatedDataResponse<TagResource>>> GetTagsAsync(int? pageSize = null)
    {
        var queryParams = new Dictionary<string, string>();

        if (pageSize != null)
        {
            queryParams.Add("page[size]", pageSize.ToString());
        }

        return await SendPaginatedRequestAsync<TagResource>(
            HttpMethod.Get, "/tags", queryParams);
    }

    private async Task<UpResponse<NoResponse>> TransactionTagsAsync(HttpMethod httpMethod, string transactionId, params string[] tagIds)
    {
        var postBody = new DataResponse<List<TagInputResourceIdentifier>>
        {
            Data = new List<TagInputResourceIdentifier>()
        };

        foreach (var tagId in tagIds)
        {
            postBody.Data.Add(new TagInputResourceIdentifier()
            {
                Id = tagId
            });
        }

        return await SendRequestAsync<NoResponse>(httpMethod, $"/transactions/{transactionId}/relationships/tags", content: postBody);
    }

    public async Task<UpResponse<NoResponse>> AddTagsToTransactionAsync(string transactionId, params string[] tagIds)
        => await TransactionTagsAsync(HttpMethod.Post, transactionId, tagIds);

    public async Task<UpResponse<NoResponse>> RemoveTagsFromTransactionAsync(string transactionId, params string[] tagIds)
        => await TransactionTagsAsync(HttpMethod.Delete, transactionId, tagIds);
}