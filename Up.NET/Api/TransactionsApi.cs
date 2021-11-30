using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Up.NET.Api.Tags;
using Up.NET.Api.Transactions;
using Up.NET.Models;

namespace Up.NET.Api
{
    public partial class UpApi
    {
        private async Task<UpResponse<T>> InternalGetTransactionsAsync<T>(string endpoint, int? pageSize = null, TransactionStatus? status = null, DateTime? since = null, DateTime? until = null, string category = null, string tag = null) where T : class
        {
            var queryParams = new Dictionary<string, string>();

            if (pageSize != null)
            {
                queryParams.Add("page[size]", pageSize.ToString());
            }

            if (status != null)
            {
                queryParams.Add("filter[status]", status.Value.GetEnumMemberValue());
            }

            if (since != null)
            {
                queryParams.Add("filter[since]", JsonSerializer.Serialize(since.Value));
            }
            
            if (until != null)
            {
                queryParams.Add("filter[until]", JsonSerializer.Serialize(until.Value));
            }
            
            if (category != null)
            {
                queryParams.Add("filter[category]", category);
            }
            
            if (tag != null)
            {
                queryParams.Add("filter[tag]", tag);
            }
            
            return await SendRequestAsync<T>(
                HttpMethod.Get, endpoint, queryParams);
        }
        
        public async Task<UpResponse<PaginatedDataResponse<TransactionResource>>> GetTransactionsAsync(int? pageSize = null, TransactionStatus? status = null, DateTime? since = null, DateTime? until = null, string category = null, string tag = null) 
            => await InternalGetTransactionsAsync<PaginatedDataResponse<TransactionResource>>("/transactions", pageSize, status, since, until, category, tag);

        public async Task<UpResponse<DataResponse<TransactionResource>>> GetTransactionAsync(string id, int? pageSize = null, TransactionStatus? status = null, DateTime? since = null, DateTime? until = null, string category = null, string tag = null) 
            => await InternalGetTransactionsAsync<DataResponse<TransactionResource>>($"/transactions/{id}", pageSize, status, since, until, category, tag);
        
        public async Task<UpResponse<PaginatedDataResponse<TransactionResource>>> GetTransactionsAsync(string accountId, int? pageSize = null, TransactionStatus? status = null, DateTime? since = null, DateTime? until = null, string category = null, string tag = null)
            => await InternalGetTransactionsAsync<PaginatedDataResponse<TransactionResource>>($"/accounts/{accountId}/transactions");
    }
}