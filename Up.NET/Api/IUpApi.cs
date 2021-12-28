using Up.NET.Api.Accounts;
using Up.NET.Api.Categories;
using Up.NET.Api.Tags;
using Up.NET.Api.Transactions;
using Up.NET.Api.Utilities;
using Up.NET.Api.Webhooks;
using Up.NET.Api.Webhooks.Events;
using Up.NET.Api.Webhooks.Logs;
using Up.NET.Models;

namespace Up.NET.Api;

public interface IUpApi
{
    Task<UpResponse<T>> SendRequestAsync<T>(HttpMethod httpMethod, string relativeUrl,
        Dictionary<string, string> queryParameters = null, object content = null, bool urlIsAbsolute = false)
        where T : class;

    Task<UpResponse<PaginatedDataResponse<T>>> SendPaginatedRequestAsync<T>(HttpMethod httpMethod,
        string relativeUrl, Dictionary<string, string> queryParameters = null, object content = null,
        bool urlIsAbsolute = false) where T : class;

    Task<UpResponse<PaginatedDataResponse<TagResource>>> GetTagsAsync(int? pageSize = null);
    Task<UpResponse<NoResponse>> AddTagsToTransactionAsync(string transactionId, params string[] tagIds);
    Task<UpResponse<NoResponse>> RemoveTagsFromTransactionAsync(string transactionId, params string[] tagIds);

    Task<UpResponse<PaginatedDataResponse<TransactionResource>>> GetTransactionsAsync(int? pageSize = null,
        TransactionStatus? status = null, DateTime? since = null, DateTime? until = null, string category = null,
        string tag = null);

    Task<UpResponse<PaginatedDataResponse<TransactionResource>>> GetTransactionsAsync(string accountId,
        int? pageSize = null, TransactionStatus? status = null, DateTime? since = null, DateTime? until = null,
        string category = null, string tag = null);

    Task<UpResponse<DataResponse<TransactionResource>>> GetTransactionAsync(string id, int? pageSize = null,
        TransactionStatus? status = null, DateTime? since = null, DateTime? until = null, string category = null,
        string tag = null);

    Task<UpResponse<PaginatedDataResponse<WebhookResource>>> GetWebhooksAsync(int? pageSize = null);
    Task<UpResponse<DataResponse<WebhookResource>>> GetWebhooksAsync(string id);
    Task<UpResponse<DataResponse<WebhookResource>>> CreateWebhookAsync(WebhookInputResource webhook);
    Task<UpResponse<NoResponse>> DeleteWebhookAsync(string id);
    Task<UpResponse<WebhookEventResource>> PingWebhookAsync(string webhookId);
    Task<UpResponse<PaginatedDataResponse<WebhookDeliveryLogResource>>> GetWebhookLogsAsync(string webhookId);
    Task<UpResponse<PingResponse>> GetPingAsync();
    Task<UpResponse<PaginatedDataResponse<AccountResource>>> GetAccountsAsync(int? pageSize = null);
    Task<UpResponse<DataResponse<AccountResource>>> GetAccountAsync(string id);
    Task<UpResponse<PaginatedDataResponse<CategoriesResource>>> GetCategoriesAsync(string parentId = null);
    Task<UpResponse<PaginatedDataResponse<CategoriesResource>>> GetCategoryAsync(string id);
}