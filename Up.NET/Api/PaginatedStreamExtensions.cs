using System.Runtime.CompilerServices;
using Up.NET.Api.Accounts;
using Up.NET.Api.Attachments;
using Up.NET.Api.Tags;
using Up.NET.Api.Transactions;
using Up.NET.Api.Webhooks;
using Up.NET.Api.Webhooks.Logs;
using Up.NET.Models;

namespace Up.NET.Api;

/// <summary>
/// Extension methods for streaming paginated API responses using IAsyncEnumerable.
/// Enables automatic pagination with lazy evaluation and cancellation support.
/// </summary>
public static class PaginatedStreamExtensions
{
    /// <summary>
    /// Streams all transactions from the API with automatic pagination.
    /// Items are yielded individually; network calls are triggered only when the current page buffer is exhausted.
    /// </summary>
    /// <param name="upApi">The Up API instance</param>
    /// <param name="pageSize">Number of records per page (default: null for API default)</param>
    /// <param name="status">Filter by transaction status</param>
    /// <param name="since">Filter transactions since this date</param>
    /// <param name="until">Filter transactions until this date</param>
    /// <param name="category">Filter by category</param>
    /// <param name="tag">Filter by tag</param>
    /// <param name="cancellationToken">Cancellation token to cancel the enumeration</param>
    /// <returns>An async enumerable of transaction resources</returns>
    public static async IAsyncEnumerable<TransactionResource> GetTransactionsStreamAsync(
        this IUpApi upApi,
        int? pageSize = null,
        TransactionStatus? status = null,
        DateTime? since = null,
        DateTime? until = null,
        string category = null,
        string tag = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var response = await upApi.GetTransactionsAsync(pageSize, status, since, until, category, tag);
        
        if (!response.Success || response.Response == null)
        {
            yield break;
        }

        // Yield items from the first page
        foreach (var item in response.Response.Data)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return item;
        }

        // Continue fetching and yielding from subsequent pages
        while (response.Response.Links?.HasNext == true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            response = await response.Response.GetNextPageAsync();
            
            if (!response.Success || response.Response == null)
            {
                yield break;
            }

            foreach (var item in response.Response.Data)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return item;
            }
        }
    }

    /// <summary>
    /// Streams all transactions for a specific account with automatic pagination.
    /// </summary>
    /// <param name="upApi">The Up API instance</param>
    /// <param name="accountId">The account ID to filter transactions by</param>
    /// <param name="pageSize">Number of records per page (default: null for API default)</param>
    /// <param name="status">Filter by transaction status</param>
    /// <param name="since">Filter transactions since this date</param>
    /// <param name="until">Filter transactions until this date</param>
    /// <param name="category">Filter by category</param>
    /// <param name="tag">Filter by tag</param>
    /// <param name="cancellationToken">Cancellation token to cancel the enumeration</param>
    /// <returns>An async enumerable of transaction resources</returns>
    public static async IAsyncEnumerable<TransactionResource> GetTransactionsStreamAsync(
        this IUpApi upApi,
        string accountId,
        int? pageSize = null,
        TransactionStatus? status = null,
        DateTime? since = null,
        DateTime? until = null,
        string category = null,
        string tag = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var response = await upApi.GetTransactionsAsync(accountId, pageSize, status, since, until, category, tag);
        
        if (!response.Success || response.Response == null)
        {
            yield break;
        }

        // Yield items from the first page
        foreach (var item in response.Response.Data)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return item;
        }

        // Continue fetching and yielding from subsequent pages
        while (response.Response.Links?.HasNext == true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            response = await response.Response.GetNextPageAsync();
            
            if (!response.Success || response.Response == null)
            {
                yield break;
            }

            foreach (var item in response.Response.Data)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return item;
            }
        }
    }

    /// <summary>
    /// Streams all accounts from the API with automatic pagination.
    /// </summary>
    /// <param name="upApi">The Up API instance</param>
    /// <param name="pageSize">Number of records per page (default: null for API default)</param>
    /// <param name="accountType">Filter by account type</param>
    /// <param name="ownershipType">Filter by ownership type</param>
    /// <param name="cancellationToken">Cancellation token to cancel the enumeration</param>
    /// <returns>An async enumerable of account resources</returns>
    public static async IAsyncEnumerable<AccountResource> GetAccountsStreamAsync(
        this IUpApi upApi,
        int? pageSize = null,
        AccountType? accountType = null,
        OwnershipType? ownershipType = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var response = await upApi.GetAccountsAsync(pageSize, accountType, ownershipType);
        
        if (!response.Success || response.Response == null)
        {
            yield break;
        }

        // Yield items from the first page
        foreach (var item in response.Response.Data)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return item;
        }

        // Continue fetching and yielding from subsequent pages
        while (response.Response.Links?.HasNext == true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            response = await response.Response.GetNextPageAsync();
            
            if (!response.Success || response.Response == null)
            {
                yield break;
            }

            foreach (var item in response.Response.Data)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return item;
            }
        }
    }

    /// <summary>
    /// Streams all tags from the API with automatic pagination.
    /// </summary>
    /// <param name="upApi">The Up API instance</param>
    /// <param name="pageSize">Number of records per page (default: null for API default)</param>
    /// <param name="cancellationToken">Cancellation token to cancel the enumeration</param>
    /// <returns>An async enumerable of tag resources</returns>
    public static async IAsyncEnumerable<TagResource> GetTagsStreamAsync(
        this IUpApi upApi,
        int? pageSize = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var response = await upApi.GetTagsAsync(pageSize);
        
        if (!response.Success || response.Response == null)
        {
            yield break;
        }

        // Yield items from the first page
        foreach (var item in response.Response.Data)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return item;
        }

        // Continue fetching and yielding from subsequent pages
        while (response.Response.Links?.HasNext == true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            response = await response.Response.GetNextPageAsync();
            
            if (!response.Success || response.Response == null)
            {
                yield break;
            }

            foreach (var item in response.Response.Data)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return item;
            }
        }
    }

    /// <summary>
    /// Streams all attachments from the API with automatic pagination.
    /// </summary>
    /// <param name="upApi">The Up API instance</param>
    /// <param name="pageSize">Number of records per page (default: null for API default)</param>
    /// <param name="cancellationToken">Cancellation token to cancel the enumeration</param>
    /// <returns>An async enumerable of attachment resources</returns>
    public static async IAsyncEnumerable<AttachmentResource> GetAttachmentsStreamAsync(
        this IUpApi upApi,
        int? pageSize = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var response = await upApi.GetAttachmentsAsync(pageSize);
        
        if (!response.Success || response.Response == null)
        {
            yield break;
        }

        // Yield items from the first page
        foreach (var item in response.Response.Data)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return item;
        }

        // Continue fetching and yielding from subsequent pages
        while (response.Response.Links?.HasNext == true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            response = await response.Response.GetNextPageAsync();
            
            if (!response.Success || response.Response == null)
            {
                yield break;
            }

            foreach (var item in response.Response.Data)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return item;
            }
        }
    }

    /// <summary>
    /// Streams all webhooks from the API with automatic pagination.
    /// </summary>
    /// <param name="upApi">The Up API instance</param>
    /// <param name="pageSize">Number of records per page (default: null for API default)</param>
    /// <param name="cancellationToken">Cancellation token to cancel the enumeration</param>
    /// <returns>An async enumerable of webhook resources</returns>
    public static async IAsyncEnumerable<WebhookResource> GetWebhooksStreamAsync(
        this IUpApi upApi,
        int? pageSize = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var response = await upApi.GetWebhooksAsync(pageSize);
        
        if (!response.Success || response.Response == null)
        {
            yield break;
        }

        // Yield items from the first page
        foreach (var item in response.Response.Data)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return item;
        }

        // Continue fetching and yielding from subsequent pages
        while (response.Response.Links?.HasNext == true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            response = await response.Response.GetNextPageAsync();
            
            if (!response.Success || response.Response == null)
            {
                yield break;
            }

            foreach (var item in response.Response.Data)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return item;
            }
        }
    }

    /// <summary>
    /// Streams all webhook logs for a specific webhook with automatic pagination.
    /// </summary>
    /// <param name="upApi">The Up API instance</param>
    /// <param name="webhookId">The webhook ID to get logs for</param>
    /// <param name="cancellationToken">Cancellation token to cancel the enumeration</param>
    /// <returns>An async enumerable of webhook delivery log resources</returns>
    public static async IAsyncEnumerable<WebhookDeliveryLogResource> GetWebhookLogsStreamAsync(
        this IUpApi upApi,
        string webhookId,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var response = await upApi.GetWebhookLogsAsync(webhookId);
        
        if (!response.Success || response.Response == null)
        {
            yield break;
        }

        // Yield items from the first page
        foreach (var item in response.Response.Data)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return item;
        }

        // Continue fetching and yielding from subsequent pages
        while (response.Response.Links?.HasNext == true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            response = await response.Response.GetNextPageAsync();
            
            if (!response.Success || response.Response == null)
            {
                yield break;
            }

            foreach (var item in response.Response.Data)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return item;
            }
        }
    }
}
