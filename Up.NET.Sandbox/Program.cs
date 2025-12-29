using Microsoft.Extensions.Configuration;
using Up.NET.Api;
using Up.NET.Api.Accounts;

var configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var apiKey = configuration["UpBank:ApiKey"] 
    ?? throw new InvalidOperationException("UpBank:ApiKey not found in user secrets. Run: dotnet user-secrets set \"UpBank:ApiKey\" \"your-api-key\"");

var up = new UpApi(apiKey);

// ============================================
// UTILITY ENDPOINTS
// ============================================

Console.WriteLine("=== GetPingAsync ===");
var ping = await up.GetPingAsync();
Console.WriteLine($"Success: {ping.Success}");
if (ping.Success)
{
    Console.WriteLine($"Status: {ping.Response.Meta.StatusEmoji}");
    Console.WriteLine($"Id: {ping.Response.Meta.Id}");
}

// ============================================
// ACCOUNTS ENDPOINTS
// ============================================

Console.WriteLine();
Console.WriteLine("=== GetAccountsAsync ===");
var accounts = await up.GetAccountsAsync();
Console.WriteLine($"Success: {accounts.Success}");

if (accounts.Success)
{
    foreach (var account in accounts.Response.Data)
    {
        Console.WriteLine(
            $"  {account.Attributes.DisplayName} ({account.Attributes.AccountType} {account.Attributes.OwnershipType}): ${account.Attributes.Balance.Value} {account.Attributes.Balance.CurrencyCode}");
    }
}

Console.WriteLine();
Console.WriteLine("=== GetAccountsAsync (filtered by Saver) ===");
var saverAccounts = await up.GetAccountsAsync(accountType: AccountType.Saver);
Console.WriteLine($"Success: {saverAccounts.Success}");
if (saverAccounts.Success)
{
    Console.WriteLine($"Saver accounts count: {saverAccounts.Response.Data.Count}");
    foreach (var account in saverAccounts.Response.Data)
    {
        Console.WriteLine($"  {account.Attributes.DisplayName}: ${account.Attributes.Balance.Value}");
    }
}

Console.WriteLine();
Console.WriteLine("=== GetAccountAsync ===");
var firstAccountId = accounts.Response.Data.First().Id;
var singleAccount = await up.GetAccountAsync(firstAccountId);
Console.WriteLine($"Success: {singleAccount.Success}");
if (singleAccount.Success)
{
    Console.WriteLine($"Account: {singleAccount.Response.Data.Attributes.DisplayName}");
    Console.WriteLine($"Balance: ${singleAccount.Response.Data.Attributes.Balance.Value}");
}

// ============================================
// ATTACHMENTS ENDPOINTS (NEW)
// ============================================

Console.WriteLine();
Console.WriteLine("=== GetAttachmentsAsync ===");
var attachments = await up.GetAttachmentsAsync();
Console.WriteLine($"Success: {attachments.Success}");
if (attachments.Success)
{
    Console.WriteLine($"Attachments count: {attachments.Response.Data.Count}");
    foreach (var attachment in attachments.Response.Data)
    {
        Console.WriteLine($"  {attachment.Id}: {attachment.Attributes.FileExtension} ({attachment.Attributes.FileContentType})");
    }
}

// ============================================
// CATEGORIES ENDPOINTS
// ============================================

Console.WriteLine();
Console.WriteLine("=== GetCategoriesAsync ===");
var categories = await up.GetCategoriesAsync();
Console.WriteLine($"Success: {categories.Success}");
if (categories.Success)
{
    Console.WriteLine($"Categories count: {categories.Response.Data.Count}");
    // Show first 5 categories
    foreach (var category in categories.Response.Data.Take(5))
    {
        Console.WriteLine($"  {category.Id}: {category.Attributes.Name}");
    }
    if (categories.Response.Data.Count > 5)
    {
        Console.WriteLine($"  ... and {categories.Response.Data.Count - 5} more");
    }
}

Console.WriteLine();
Console.WriteLine("=== GetCategoryAsync ===");
if (categories.Success && categories.Response.Data.Any())
{
    var firstCategoryId = categories.Response.Data.First().Id;
    var singleCategory = await up.GetCategoryAsync(firstCategoryId);
    Console.WriteLine($"Success: {singleCategory.Success}");
    if (singleCategory.Success)
    {
        Console.WriteLine($"Category: {singleCategory.Response.Data.Id} - {singleCategory.Response.Data.Attributes.Name}");
    }
}

// ============================================
// TAGS ENDPOINTS
// ============================================

Console.WriteLine();
Console.WriteLine("=== GetTagsAsync ===");
var tags = await up.GetTagsAsync();
Console.WriteLine($"Success: {tags.Success}");
if (tags.Success)
{
    Console.WriteLine($"Tags count: {tags.Response.Data.Count}");
    foreach (var tag in tags.Response.Data.Take(5))
    {
        Console.WriteLine($"  {tag.Id}");
    }
}

// ============================================
// TRANSACTIONS ENDPOINTS
// ============================================

Console.WriteLine();
Console.WriteLine("=== GetTransactionsAsync (with date filter) ===");
var recentTransactions = await up.GetTransactionsAsync(
    pageSize: 3, 
    since: DateTime.UtcNow.AddDays(-7));
Console.WriteLine($"Success: {recentTransactions.Success}");
if (recentTransactions.Success)
{
    Console.WriteLine($"Transactions in last 7 days: {recentTransactions.Response.Data.Count}");
}

Console.WriteLine();
Console.WriteLine("=== GetTransactionsAsync (with status filter) ===");
var settledTransactions = await up.GetTransactionsAsync(
    pageSize: 3, 
    status: Up.NET.Api.Transactions.TransactionStatus.Settled);
Console.WriteLine($"Success: {settledTransactions.Success}");
if (settledTransactions.Success)
{
    Console.WriteLine($"Settled transactions: {settledTransactions.Response.Data.Count}");
}

Console.WriteLine();
Console.WriteLine("=== GetTransactionsAsync ===");
var transactions = await up.GetTransactionsAsync(pageSize: 5);
Console.WriteLine($"Success: {transactions.Success}");
if (transactions.Success)
{
    foreach (var transaction in transactions.Response.Data)
    {
        Console.WriteLine($"  {transaction.Id}:");
        Console.WriteLine($"    Description: {transaction.Attributes.Description}");
        Console.WriteLine($"    Amount: {transaction.Attributes.Amount.Value} {transaction.Attributes.Amount.CurrencyCode}");
        Console.WriteLine($"    Status: {transaction.Attributes.Status}");
        Console.WriteLine($"    IsCategorizable: {transaction.Attributes.IsCategorizable}");
        Console.WriteLine($"    TransactionType: {transaction.Attributes.TransactionType}");
        if (transaction.Attributes.CardPurchaseMethod != null)
        {
            Console.WriteLine($"    CardPurchaseMethod: {transaction.Attributes.CardPurchaseMethod.Method} (****{transaction.Attributes.CardPurchaseMethod.CardNumberSuffix})");
        }
        if (transaction.Attributes.Note != null)
        {
            Console.WriteLine($"    Note: {transaction.Attributes.Note.Text}");
        }
        if (transaction.Attributes.PerformingCustomer != null)
        {
            Console.WriteLine($"    PerformingCustomer: {transaction.Attributes.PerformingCustomer.DisplayName}");
        }
        if (transaction.Relationships.TransferAccount?.Data != null)
        {
            Console.WriteLine($"    TransferAccount: {transaction.Relationships.TransferAccount.Data.Id}");
        }
    }
}

Console.WriteLine();
Console.WriteLine("=== GetTransactionAsync ===");
if (transactions.Success && transactions.Response.Data.Any())
{
    var firstTransactionId = transactions.Response.Data.First().Id;
    var singleTransaction = await up.GetTransactionAsync(firstTransactionId);
    Console.WriteLine($"Success: {singleTransaction.Success}");
    if (singleTransaction.Success)
    {
        var tx = singleTransaction.Response.Data;
        Console.WriteLine($"Transaction: {tx.Attributes.Description}");
        Console.WriteLine($"Amount: {tx.Attributes.Amount.Value}");
        Console.WriteLine($"DeepLinkURL: {tx.Attributes.DeepLinkURL}");
    }
}

Console.WriteLine();
Console.WriteLine("=== GetTransactionsAsync (by account) ===");
var accountTransactions = await up.GetTransactionsAsync(firstAccountId, pageSize: 3);
Console.WriteLine($"Success: {accountTransactions.Success}");
if (accountTransactions.Success)
{
    Console.WriteLine($"Transactions for account {firstAccountId}:");
    foreach (var transaction in accountTransactions.Response.Data)
    {
        Console.WriteLine($"  {transaction.Attributes.Description}: {transaction.Attributes.Amount.Value}");
    }
}

// ============================================
// WEBHOOKS ENDPOINTS
// ============================================

Console.WriteLine();
Console.WriteLine("=== GetWebhooksAsync ===");
var webhooks = await up.GetWebhooksAsync();
Console.WriteLine($"Success: {webhooks.Success}");
if (webhooks.Success)
{
    Console.WriteLine($"Webhooks count: {webhooks.Response.Data.Count}");
    foreach (var webhook in webhooks.Response.Data)
    {
        Console.WriteLine($"  {webhook.Id}: {webhook.Attributes.Url}");
        Console.WriteLine($"    Description: {webhook.Attributes.Description}");
    }
}

// Test GetWebhookAsync if we have any webhooks
if (webhooks.Success && webhooks.Response.Data.Any())
{
    Console.WriteLine();
    Console.WriteLine("=== GetWebhookAsync ===");
    var firstWebhookId = webhooks.Response.Data.First().Id;
    var singleWebhook = await up.GetWebhookAsync(firstWebhookId);
    Console.WriteLine($"Success: {singleWebhook.Success}");
    if (singleWebhook.Success)
    {
        Console.WriteLine($"Webhook: {singleWebhook.Response.Data.Attributes.Url}");
    }

    Console.WriteLine();
    Console.WriteLine("=== GetWebhookLogsAsync ===");
    var webhookLogs = await up.GetWebhookLogsAsync(firstWebhookId);
    Console.WriteLine($"Success: {webhookLogs.Success}");
    if (webhookLogs.Success)
    {
        Console.WriteLine($"Logs count: {webhookLogs.Response.Data.Count}");
    }
}

// ============================================
// TAG MANAGEMENT (commented out - modifies data)
// ============================================

// Uncomment to test tag management:
// if (transactions.Success && transactions.Response.Data.Any())
// {
//     var testTransactionId = transactions.Response.Data.First().Id;
//     
//     Console.WriteLine();
//     Console.WriteLine("=== AddTagsToTransactionAsync ===");
//     var addTagResult = await up.AddTagsToTransactionAsync(testTransactionId, "test-tag");
//     Console.WriteLine($"Success: {addTagResult.Success}");
//     
//     Console.WriteLine();
//     Console.WriteLine("=== RemoveTagsFromTransactionAsync ===");
//     var removeTagResult = await up.RemoveTagsFromTransactionAsync(testTransactionId, "test-tag");
//     Console.WriteLine($"Success: {removeTagResult.Success}");
// }

// ============================================
// CATEGORIZATION (commented out - modifies data)
// ============================================

// Uncomment to test categorization:
// if (transactions.Success && transactions.Response.Data.Any() && categories.Success && categories.Response.Data.Any())
// {
//     var testTransactionId = transactions.Response.Data.First().Id;
//     var testCategoryId = categories.Response.Data.First().Id;
//     
//     Console.WriteLine();
//     Console.WriteLine("=== CategorizeTransactionAsync ===");
//     var categorizeResult = await up.CategorizeTransactionAsync(testTransactionId, testCategoryId);
//     Console.WriteLine($"Success: {categorizeResult.Success}");
//     
//     // To de-categorize, pass null:
//     // var decategorizeResult = await up.CategorizeTransactionAsync(testTransactionId, null);
// }

// ============================================
// WEBHOOK MANAGEMENT (commented out - modifies data)
// ============================================

// Uncomment to test webhook creation:
// Console.WriteLine();
// Console.WriteLine("=== CreateWebhookAsync ===");
// var newWebhook = await up.CreateWebhookAsync(new Up.NET.Api.Webhooks.WebhookInputResource
// {
//     Attributes = new Up.NET.Api.Webhooks.WebhookInputAttributes
//     {
//         Url = "https://example.com/webhook",
//         Description = "Test webhook"
//     }
// });
// Console.WriteLine($"Success: {newWebhook.Success}");
// if (newWebhook.Success)
// {
//     Console.WriteLine($"Webhook ID: {newWebhook.Response.Data.Id}");
//     Console.WriteLine($"Secret Key: {newWebhook.Response.Data.Attributes.SecretKey}");
//     
//     Console.WriteLine();
//     Console.WriteLine("=== PingWebhookAsync ===");
//     var pingResult = await up.PingWebhookAsync(newWebhook.Response.Data.Id);
//     Console.WriteLine($"Success: {pingResult.Success}");
//     
//     Console.WriteLine();
//     Console.WriteLine("=== DeleteWebhookAsync ===");
//     var deleteResult = await up.DeleteWebhookAsync(newWebhook.Response.Data.Id);
//     Console.WriteLine($"Success: {deleteResult.Success}");
// }

Console.WriteLine();
Console.WriteLine("=== All tests completed ===");

public partial class Program { }
