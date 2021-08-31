using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Up.NET.Api.Accounts;
using Up.NET.Api.Categories;
using Up.NET.Models;

namespace Up.NET.Api
{
    public partial class UpApi
    {
        public async Task<UpResponse<PaginatedDataResponse<CategoriesResource>>> GetCategoriesAsync(string parentId = null)
        {
            var queryParams = new Dictionary<string, string>();

            if (parentId != null)
            {
                queryParams.Add("filter[parent]", parentId);
            }
            
            return await SendRequestAsync<PaginatedDataResponse<CategoriesResource>>(
                HttpMethod.Get, "/categories", queryParams);
        }
        
        public async Task<UpResponse<PaginatedDataResponse<CategoriesResource>>> GetCategoryAsync(string id) 
            => await SendRequestAsync<PaginatedDataResponse<CategoriesResource>>(HttpMethod.Get, $"/categories/{id}");
    }
}