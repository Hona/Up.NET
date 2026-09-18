using System.ComponentModel;
using ModelContextProtocol;
using ModelContextProtocol.Server;
using Up.NET.Api;
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

namespace Up.NET.Mcp;

[McpServerToolType]
public sealed class UpTools(IUpApi upApi)
{
    [McpServerTool(Name = "up_list_accounts", ReadOnly = true),
     Description("Lists Up Bank accounts, optionally filtered by account and ownership type.")]
    public Task<PaginatedDataResponse<AccountResource>> ListAccountsAsync(
        [Description("Positive number of accounts per page.")] int? pageSize = null,
        [Description("Account type: Saver, Transactional, or HomeLoan.")] AccountType? accountType = null,
        [Description("Ownership type: Individual or Joint.")] OwnershipType? ownershipType = null)
    {
        ValidatePageSize(pageSize);
        return UnwrapAsync(upApi.GetAccountsAsync(pageSize, accountType, ownershipType));
    }

    [McpServerTool(Name = "up_get_account", ReadOnly = true), Description("Gets one Up Bank account by ID.")]
    public Task<DataResponse<AccountResource>> GetAccountAsync(
        [Description("Up account ID.")] string id) =>
        UnwrapAsync(upApi.GetAccountAsync(Required(id, nameof(id))));

    [McpServerTool(Name = "up_list_attachments", ReadOnly = true),
     Description("Lists attachments for Up Bank transactions.")]
    public Task<PaginatedDataResponse<AttachmentResource>> ListAttachmentsAsync(
        [Description("Positive number of attachments per page.")] int? pageSize = null)
    {
        ValidatePageSize(pageSize);
        return UnwrapAsync(upApi.GetAttachmentsAsync(pageSize));
    }

    [McpServerTool(Name = "up_get_attachment", ReadOnly = true), Description("Gets one attachment by ID.")]
    public Task<DataResponse<AttachmentResource>> GetAttachmentAsync(
        [Description("Up attachment ID.")] string id) =>
        UnwrapAsync(upApi.GetAttachmentAsync(Required(id, nameof(id))));

    [McpServerTool(Name = "up_list_categories", ReadOnly = true),
     Description("Lists Up Bank transaction categories, optionally below a parent category.")]
    public Task<DataResponse<List<CategoriesResource>>> ListCategoriesAsync(
        [Description("Optional parent category ID.")] string? parentId = null) =>
        UnwrapAsync(upApi.GetCategoriesAsync(Optional(parentId)));

    [McpServerTool(Name = "up_get_category", ReadOnly = true), Description("Gets one transaction category by ID.")]
    public Task<DataResponse<CategoriesResource>> GetCategoryAsync(
        [Description("Up category ID.")] string id) =>
        UnwrapAsync(upApi.GetCategoryAsync(Required(id, nameof(id))));

    [McpServerTool(Name = "up_categorize_transaction", ReadOnly = false, Destructive = true, Idempotent = true),
     Description("Sets the category of an Up Bank transaction.")]
    public async Task<string> CategorizeTransactionAsync(
        [Description("Up transaction ID.")] string transactionId,
        [Description("Up category ID. Omit or set to null to remove the current category.")] string? categoryId = null)
    {
        categoryId = Optional(categoryId);
        await EnsureSuccessAsync(upApi.CategorizeTransactionAsync(
            Required(transactionId, nameof(transactionId)),
            categoryId!));
        return categoryId is null ? "Transaction category removed." : "Transaction category updated.";
    }

    [McpServerTool(Name = "up_list_tags", ReadOnly = true), Description("Lists Up Bank transaction tags.")]
    public Task<PaginatedDataResponse<TagResource>> ListTagsAsync(
        [Description("Positive number of tags per page.")] int? pageSize = null)
    {
        ValidatePageSize(pageSize);
        return UnwrapAsync(upApi.GetTagsAsync(pageSize));
    }

    [McpServerTool(Name = "up_add_transaction_tags", ReadOnly = false, Destructive = false, Idempotent = true),
     Description("Adds one or more tags to an Up Bank transaction.")]
    public async Task<string> AddTransactionTagsAsync(
        [Description("Up transaction ID.")] string transactionId,
        [Description("One or more Up tag IDs.")] string[] tagIds)
    {
        var tags = RequiredTags(tagIds);
        if (tags.Length > 6)
        {
            throw new McpException("A transaction can have no more than 6 tags.");
        }

        await EnsureSuccessAsync(upApi.AddTagsToTransactionAsync(Required(transactionId, nameof(transactionId)), tags));
        return "Tags added to the transaction.";
    }

    [McpServerTool(Name = "up_remove_transaction_tags", ReadOnly = false, Destructive = true, Idempotent = true),
     Description("Removes one or more tags from an Up Bank transaction.")]
    public async Task<string> RemoveTransactionTagsAsync(
        [Description("Up transaction ID.")] string transactionId,
        [Description("One or more Up tag IDs.")] string[] tagIds)
    {
        var tags = RequiredTags(tagIds);
        await EnsureSuccessAsync(upApi.RemoveTagsFromTransactionAsync(Required(transactionId, nameof(transactionId)), tags));
        return "Tags removed from the transaction.";
    }

    [McpServerTool(Name = "up_list_transactions", ReadOnly = true),
     Description("Lists Up Bank transactions with optional status, date, category, and tag filters.")]
    public Task<PaginatedDataResponse<TransactionResource>> ListTransactionsAsync(
        [Description("Positive number of transactions per page.")] int? pageSize = null,
        [Description("Transaction status: Held or Settled.")] TransactionStatus? status = null,
        [Description("Return transactions since this ISO 8601 date and time.")] DateTime? since = null,
        [Description("Return transactions until this ISO 8601 date and time.")] DateTime? until = null,
        [Description("Optional category ID.")] string? category = null,
        [Description("Optional tag ID.")] string? tag = null)
    {
        ValidateTransactionFilters(pageSize, since, until);
        return UnwrapAsync(upApi.GetTransactionsAsync(pageSize, status, since, until, Optional(category), Optional(tag)));
    }

    [McpServerTool(Name = "up_list_account_transactions", ReadOnly = true),
     Description("Lists transactions for one Up Bank account with optional filters.")]
    public Task<PaginatedDataResponse<TransactionResource>> ListAccountTransactionsAsync(
        [Description("Up account ID.")] string accountId,
        [Description("Positive number of transactions per page.")] int? pageSize = null,
        [Description("Transaction status: Held or Settled.")] TransactionStatus? status = null,
        [Description("Return transactions since this ISO 8601 date and time.")] DateTime? since = null,
        [Description("Return transactions until this ISO 8601 date and time.")] DateTime? until = null,
        [Description("Optional category ID.")] string? category = null,
        [Description("Optional tag ID.")] string? tag = null)
    {
        ValidateTransactionFilters(pageSize, since, until);
        return UnwrapAsync(upApi.GetTransactionsAsync(Required(accountId, nameof(accountId)), pageSize, status, since,
            until, Optional(category), Optional(tag)));
    }

    [McpServerTool(Name = "up_get_transaction", ReadOnly = true), Description("Gets one Up Bank transaction by ID.")]
    public Task<DataResponse<TransactionResource>> GetTransactionAsync(
        [Description("Up transaction ID.")] string id) =>
        UnwrapAsync(upApi.GetTransactionAsync(Required(id, nameof(id))));

    [McpServerTool(Name = "up_ping", ReadOnly = true), Description("Checks the Up API token and returns its owner.")]
    public Task<PingResponse> PingAsync() => UnwrapAsync(upApi.GetPingAsync());

    [McpServerTool(Name = "up_list_webhooks", ReadOnly = true), Description("Lists registered Up API webhooks.")]
    public Task<PaginatedDataResponse<WebhookResource>> ListWebhooksAsync(
        [Description("Positive number of webhooks per page.")] int? pageSize = null)
    {
        ValidatePageSize(pageSize);
        return UnwrapAsync(upApi.GetWebhooksAsync(pageSize));
    }

    [McpServerTool(Name = "up_get_webhook", ReadOnly = true), Description("Gets one Up API webhook by ID.")]
    public Task<DataResponse<WebhookResource>> GetWebhookAsync(
        [Description("Up webhook ID.")] string id) =>
        UnwrapAsync(upApi.GetWebhookAsync(Required(id, nameof(id))));

    [McpServerTool(Name = "up_create_webhook", ReadOnly = false, Destructive = false, Idempotent = false),
     Description("Creates an Up API webhook.")]
    public Task<DataResponse<WebhookResource>> CreateWebhookAsync(
        [Description("Public HTTP or HTTPS URL that receives webhook events; maximum 300 characters.")] string url,
        [Description("Optional description of the webhook's purpose; maximum 64 characters.")] string? description = null)
    {
        url = Required(url, nameof(url));
        description = Optional(description);
        if (url.Length > 300)
        {
            throw new McpException("url must not exceed 300 characters.");
        }

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
        {
            throw new McpException("url must be an absolute HTTP or HTTPS URL.");
        }

        var webhook = new WebhookInputResource
        {
            Attributes = new WebhookInputAttributes
            {
                Url = url,
                Description = description!
            }
        };

        if (description?.Length > 64)
        {
            throw new McpException("description must not exceed 64 characters.");
        }

        return UnwrapAsync(upApi.CreateWebhookAsync(webhook));
    }

    [McpServerTool(Name = "up_delete_webhook", ReadOnly = false, Destructive = true, Idempotent = true),
     Description("Permanently deletes an Up API webhook.")]
    public async Task<string> DeleteWebhookAsync([Description("Up webhook ID.")] string id)
    {
        await EnsureSuccessAsync(upApi.DeleteWebhookAsync(Required(id, nameof(id))));
        return "Webhook deleted.";
    }

    [McpServerTool(Name = "up_ping_webhook", ReadOnly = false, Destructive = false, Idempotent = false),
     Description("Sends a test event to a registered Up API webhook.")]
    public Task<DataResponse<WebhookEventResource>> PingWebhookAsync(
        [Description("Up webhook ID.")] string webhookId) =>
        UnwrapAsync(upApi.PingWebhookAsync(Required(webhookId, nameof(webhookId))));

    [McpServerTool(Name = "up_list_webhook_logs", ReadOnly = true),
     Description("Lists delivery logs for an Up API webhook.")]
    public Task<PaginatedDataResponse<WebhookDeliveryLogResource>> ListWebhookLogsAsync(
        [Description("Up webhook ID.")] string webhookId) =>
        UnwrapAsync(upApi.GetWebhookLogsAsync(Required(webhookId, nameof(webhookId))));

    [McpServerTool(Name = "up_get_next_page", ReadOnly = true),
     Description("Follows an opaque next or previous page URL returned by an Up list tool.")]
    public async Task<object> GetNextPageAsync(
        [Description("Resource type in the page: Accounts, Attachments, Tags, Transactions, Webhooks, or WebhookLogs.")]
        UpPageType pageType,
        [Description("Exact links.next or links.prev URL returned by an Up list tool.")] string pageUrl)
    {
        pageUrl = Required(pageUrl, nameof(pageUrl));
        if (!Uri.TryCreate(pageUrl, UriKind.Absolute, out var uri) ||
            uri.Scheme != Uri.UriSchemeHttps ||
            !string.Equals(uri.Host, "api.up.com.au", StringComparison.OrdinalIgnoreCase) ||
            !uri.AbsolutePath.StartsWith("/api/v1/", StringComparison.Ordinal))
        {
            throw new McpException("pageUrl must be an HTTPS Up API v1 pagination URL.");
        }

        return pageType switch
        {
            UpPageType.Accounts => await GetPageAsync<AccountResource>(pageUrl),
            UpPageType.Attachments => await GetPageAsync<AttachmentResource>(pageUrl),
            UpPageType.Tags => await GetPageAsync<TagResource>(pageUrl),
            UpPageType.Transactions => await GetPageAsync<TransactionResource>(pageUrl),
            UpPageType.Webhooks => await GetPageAsync<WebhookResource>(pageUrl),
            UpPageType.WebhookLogs => await GetPageAsync<WebhookDeliveryLogResource>(pageUrl),
            _ => throw new McpException($"Unsupported page type: {pageType}.")
        };
    }

    private Task<PaginatedDataResponse<T>> GetPageAsync<T>(string pageUrl) where T : class =>
        UnwrapAsync(upApi.SendPaginatedRequestAsync<T>(HttpMethod.Get, pageUrl, urlIsAbsolute: true));

    private static async Task<T> UnwrapAsync<T>(Task<UpResponse<T>> operation) where T : class
    {
        var result = await operation.ConfigureAwait(false);
        if (result.Success)
        {
            return result.Response;
        }

        throw ApiFailure(result.Errors);
    }

    private static async Task EnsureSuccessAsync(Task<UpResponse<NoResponse>> operation)
    {
        var result = await operation.ConfigureAwait(false);
        if (!result.Success)
        {
            throw ApiFailure(result.Errors);
        }
    }

    private static McpException ApiFailure(IEnumerable<ErrorResponse>? errors)
    {
        var messages = errors?.Select(error =>
            $"{(int)error.Status} {error.Title}: {error.Detail}".TrimEnd(' ', ':')).ToArray();
        return new McpException(messages is { Length: > 0 }
            ? $"Up API request failed: {string.Join("; ", messages)}"
            : "Up API request failed without an error response.");
    }

    private static string Required(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new McpException($"{parameterName} is required.");
        }

        return value.Trim();
    }

    private static string? Optional(string? value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

    private static string[] RequiredTags(string[]? tagIds)
    {
        var tags = tagIds?
            .Where(tag => !string.IsNullOrWhiteSpace(tag))
            .Select(tag => tag.Trim())
            .Distinct(StringComparer.Ordinal)
            .ToArray();

        if (tags is not { Length: > 0 })
        {
            throw new McpException("tagIds must contain at least one tag ID.");
        }

        return tags;
    }

    private static void ValidatePageSize(int? pageSize)
    {
        if (pageSize is < 1)
        {
            throw new McpException("pageSize must be positive.");
        }
    }

    private static void ValidateTransactionFilters(int? pageSize, DateTime? since, DateTime? until)
    {
        ValidatePageSize(pageSize);
        if (since > until)
        {
            throw new McpException("since must be earlier than or equal to until.");
        }
    }
}
