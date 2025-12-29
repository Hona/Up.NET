using Up.NET.Api.Transactions;
using Up.NET.Models;

namespace Up.NET.Api;

public partial class UpApi
{
    public async Task<UpResponse<PaginatedDataResponse<TransactionResource>>> GetTransactionsAsync(int? pageSize = null,
        TransactionStatus? status = null, DateTime? since = null, DateTime? until = null, string category = null,
        string tag = null)
        => await InternalGetTransactionsAsync<TransactionResource>("/transactions", pageSize, status, since, until,
            category, tag);

    public async Task<UpResponse<DataResponse<TransactionResource>>> GetTransactionAsync(string id)
        => await SendRequestAsync<DataResponse<TransactionResource>>(HttpMethod.Get, $"/transactions/{id}");

    public async Task<UpResponse<PaginatedDataResponse<TransactionResource>>> GetTransactionsAsync(string accountId,
        int? pageSize = null, TransactionStatus? status = null, DateTime? since = null, DateTime? until = null,
        string category = null, string tag = null)
        => await InternalGetTransactionsAsync<TransactionResource>($"/accounts/{accountId}/transactions", pageSize, status, since, until, category, tag);

    private async Task<UpResponse<PaginatedDataResponse<T>>> InternalGetTransactionsAsync<T>(string endpoint,
        int? pageSize = null, TransactionStatus? status = null, DateTime? since = null, DateTime? until = null,
        string category = null, string tag = null) where T : class
    {
        var queryParams = new Dictionary<string, string>();

        if (pageSize != null)
        {
            queryParams.Add("page[size]", pageSize.ToString());
        }

        if (status != null)
        {
            queryParams.Add("filter[status]", status.Value.ToApiString());
        }

        if (since != null)
        {
            queryParams.Add("filter[since]", since.Value.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK"));
        }

        if (until != null)
        {
            queryParams.Add("filter[until]", until.Value.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK"));
        }
        
        if (category != null)
        {
            queryParams.Add("filter[category]", category);
        }

        if (tag != null)
        {
            queryParams.Add("filter[tag]", tag);
        }

        return await SendPaginatedRequestAsync<T>(
            HttpMethod.Get, endpoint, queryParams);
    }
}
