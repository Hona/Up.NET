using Up.NET.Api.Accounts;
using Up.NET.Models;

namespace Up.NET.Api;

public partial class UpApi
{
    public async Task<UpResponse<PaginatedDataResponse<AccountResource>>> GetAccountsAsync(
        int? pageSize = null,
        AccountType? accountType = null,
        OwnershipType? ownershipType = null)
    {
        var queryParams = new Dictionary<string, string>();

        if (pageSize.HasValue)
        {
            queryParams.Add("page[size]", pageSize.ToString());
        }

        if (accountType.HasValue)
        {
            queryParams.Add("filter[accountType]", accountType.Value.ToApiString());
        }

        if (ownershipType.HasValue)
        {
            queryParams.Add("filter[ownershipType]", ownershipType.Value.ToApiString());
        }

        return await SendPaginatedRequestAsync<AccountResource>(
            HttpMethod.Get, "/accounts", queryParams);
    }

    public async Task<UpResponse<DataResponse<AccountResource>>> GetAccountAsync(string id)
        => await SendRequestAsync<DataResponse<AccountResource>>(HttpMethod.Get, $"/accounts/{id}");
}