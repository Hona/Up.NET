<div align="center">

# Up.NET

[![Nuget](https://img.shields.io/nuget/v/HUp.NET)](https://www.nuget.org/packages/HUp.NET)

<img alt="Up API" src="https://github.com/up-banking/api/raw/master/assets/up-api.jpg" />

__.NET API wrapper for [Up Bank](https://up.com.au)__

</div>

## Installation

```bash
dotnet add package HUp.NET
```

## Quick Start

```csharp
using Up.NET.Api;

var upApi = new UpApi("your-personal-access-token");

// Verify your token
var ping = await upApi.GetPingAsync();
Console.WriteLine($"Authenticated as: {ping.Data.Meta.Id}");

// List accounts
var accounts = await upApi.GetAccountsAsync();
foreach (var account in accounts.Data.Data)
{
    Console.WriteLine($"{account.Attributes.DisplayName}: {account.Attributes.Balance.Value}");
}

// Get recent transactions
var transactions = await upApi.GetTransactionsAsync(pageSize: 10);
foreach (var tx in transactions.Data.Data)
{
    Console.WriteLine($"{tx.Attributes.Description}: {tx.Attributes.Amount.Value}");
}
```

## API Coverage

Full coverage of the [Up API](https://developer.up.com.au) (v1).

| Endpoint | Method |
|----------|--------|
| **Accounts** | `GetAccountsAsync()`, `GetAccountAsync(id)` |
| **Transactions** | `GetTransactionsAsync()`, `GetTransactionAsync(id)`, `GetTransactionsAsync(accountId)` |
| **Categories** | `GetCategoriesAsync()`, `GetCategoryAsync(id)`, `CategorizeTransactionAsync()` |
| **Tags** | `GetTagsAsync()`, `AddTagsToTransactionAsync()`, `RemoveTagsFromTransactionAsync()` |
| **Attachments** | `GetAttachmentsAsync()`, `GetAttachmentAsync(id)` |
| **Webhooks** | `GetWebhooksAsync()`, `GetWebhookAsync(id)`, `CreateWebhookAsync()`, `DeleteWebhookAsync()`, `PingWebhookAsync()`, `GetWebhookLogsAsync()` |
| **Utility** | `GetPingAsync()` |

## Pagination

All paginated responses include a `GetNextPageAsync()` helper:

```csharp
var transactions = await upApi.GetTransactionsAsync(pageSize: 50);

while (transactions.Data?.Links?.Next != null)
{
    // Process current page
    foreach (var tx in transactions.Data.Data)
    {
        Console.WriteLine(tx.Attributes.Description);
    }
    
    // Fetch next page
    transactions = await transactions.Data.GetNextPageAsync(upApi);
}
```

## Filtering

```csharp
// Filter accounts by type
var savers = await upApi.GetAccountsAsync(accountType: AccountType.Saver);

// Filter transactions by date range and status
var settled = await upApi.GetTransactionsAsync(
    status: TransactionStatus.Settled,
    since: DateTime.Now.AddDays(-30),
    until: DateTime.Now
);

// Filter by category or tag
var groceries = await upApi.GetTransactionsAsync(category: "groceries");
var tagged = await upApi.GetTransactionsAsync(tag: "holiday");
```

## Getting Your API Token

1. Download the [Up app](https://up.com.au) and create an account
2. Get your Personal Access Token at https://api.up.com.au

## Links

* [Up Website](https://up.com.au)
* [API Documentation](https://developer.up.com.au)
* [API Issues and Support](https://github.com/up-banking/api/issues)

## License

MIT
