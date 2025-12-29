using Up.NET.Api.Categories;
using Up.NET.Models;

namespace Up.NET.Api;

public partial class UpApi
{
    public async Task<UpResponse<DataResponse<List<CategoriesResource>>>> GetCategoriesAsync(string parentId = null)
    {
        var queryParams = new Dictionary<string, string>();

        if (parentId != null)
        {
            queryParams.Add("filter[parent]", parentId);
        }

        return await SendRequestAsync<DataResponse<List<CategoriesResource>>>(
            HttpMethod.Get, "/categories", queryParams);
    }

    public async Task<UpResponse<DataResponse<CategoriesResource>>> GetCategoryAsync(string id)
        => await SendRequestAsync<DataResponse<CategoriesResource>>(HttpMethod.Get, $"/categories/{id}");

    public async Task<UpResponse<NoResponse>> CategorizeTransactionAsync(string transactionId, string categoryId)
    {
        var content = new DataWrapper<CategoryInputResourceIdentifier>
        {
            Data = categoryId == null ? null : new CategoryInputResourceIdentifier
            {
                Type = "categories",
                Id = categoryId
            }
        };

        return await SendRequestAsync<NoResponse>(
            HttpMethod.Patch, 
            $"/transactions/{transactionId}/relationships/category", 
            content: content);
    }
}