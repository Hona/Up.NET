using Up.NET.Api.Accounts;
using Up.NET.Api.Attachments;
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
    // Core request methods
    Task<UpResponse<T>> SendRequestAsync<T>(HttpMethod httpMethod, string relativeUrl,
        Dictionary<string, string> queryParameters = null, object content = null, bool urlIsAbsolute = false)
        where T : class;

    Task<UpResponse<PaginatedDataResponse<T>>> SendPaginatedRequestAsync<T>(HttpMethod httpMethod,
        string relativeUrl, Dictionary<string, string> queryParameters = null, object content = null,
        bool urlIsAbsolute = false) where T : class;

    // Accounts
    Task<UpResponse<PaginatedDataResponse<AccountResource>>> GetAccountsAsync(
        int? pageSize = null,
        AccountType? accountType = null,
        OwnershipType? ownershipType = null);
    Task<UpResponse<DataResponse<AccountResource>>> GetAccountAsync(string id);

    // Attachments
    Task<UpResponse<PaginatedDataResponse<AttachmentResource>>> GetAttachmentsAsync(int? pageSize = null);
    Task<UpResponse<DataResponse<AttachmentResource>>> GetAttachmentAsync(string id);

    // Categories
    Task<UpResponse<DataResponse<List<CategoriesResource>>>> GetCategoriesAsync(string parentId = null);
    Task<UpResponse<DataResponse<CategoriesResource>>> GetCategoryAsync(string id);
    Task<UpResponse<NoResponse>> CategorizeTransactionAsync(string transactionId, string categoryId);

    // Tags
    Task<UpResponse<PaginatedDataResponse<TagResource>>> GetTagsAsync(int? pageSize = null);
    Task<UpResponse<NoResponse>> AddTagsToTransactionAsync(string transactionId, params string[] tagIds);
    Task<UpResponse<NoResponse>> RemoveTagsFromTransactionAsync(string transactionId, params string[] tagIds);

    // Transactions
    Task<UpResponse<PaginatedDataResponse<TransactionResource>>> GetTransactionsAsync(int? pageSize = null,
        TransactionStatus? status = null, DateTime? since = null, DateTime? until = null, string category = null,
        string tag = null);

    Task<UpResponse<PaginatedDataResponse<TransactionResource>>> GetTransactionsAsync(string accountId,
        int? pageSize = null, TransactionStatus? status = null, DateTime? since = null, DateTime? until = null,
        string category = null, string tag = null);

    Task<UpResponse<DataResponse<TransactionResource>>> GetTransactionAsync(string id, int? pageSize = null,
        TransactionStatus? status = null, DateTime? since = null, DateTime? until = null, string category = null,
        string tag = null);

    // Utility
    Task<UpResponse<PingResponse>> GetPingAsync();

    // Webhooks
    Task<UpResponse<PaginatedDataResponse<WebhookResource>>> GetWebhooksAsync(int? pageSize = null);
    Task<UpResponse<DataResponse<WebhookResource>>> GetWebhookAsync(string id);
    Task<UpResponse<DataResponse<WebhookResource>>> CreateWebhookAsync(WebhookInputResource webhook);
    Task<UpResponse<NoResponse>> DeleteWebhookAsync(string id);
    Task<UpResponse<DataResponse<WebhookEventResource>>> PingWebhookAsync(string webhookId);
    Task<UpResponse<PaginatedDataResponse<WebhookDeliveryLogResource>>> GetWebhookLogsAsync(string webhookId);
}
