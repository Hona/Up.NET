using Up.NET.Api;

var up = new UpApi("");

Console.WriteLine("-- GetAccountsAsync --");
var accounts = await up.GetAccountsAsync();

Console.WriteLine(accounts.Success);

if (accounts.Success)
{
    foreach (var account in accounts.Response.Data)
    {
        Console.WriteLine(
            $"{account.Attributes.DisplayName} ({account.Attributes.AccountType} {account.Attributes.OwnershipType}): ${account.Attributes.Balance.Value} {account.Attributes.Balance.CurrencyCode}");
        Console.WriteLine($" - {account.Id}");
    }
}

while (accounts.Response.Links.HasNext)
{
    Console.WriteLine("Has another page");
    accounts = await accounts.Response.GetNextPageAsync();
    
    if (accounts.Success)
    {
        foreach (var account in accounts.Response.Data)
        {
            Console.WriteLine(
                $"{account.Attributes.DisplayName} ({account.Attributes.AccountType} {account.Attributes.OwnershipType}): ${account.Attributes.Balance.Value} {account.Attributes.Balance.CurrencyCode}");
            Console.WriteLine($" - {account.Id}");
        }
    }
}


Console.WriteLine();
Console.WriteLine("-- GetAccountAsync --");

var searchedAccount = await up.GetAccountAsync(accounts.Response.Data.First().Id);

Console.WriteLine(searchedAccount.Success);
Console.WriteLine("$" + searchedAccount.Response.Data.Attributes.Balance.ValueInBaseUnits / 100);

Console.WriteLine();
Console.WriteLine("-- GetPingAsync --");

var ping = await up.GetPingAsync();
Console.WriteLine(ping.Success);
if (ping.Success)
{
    Console.WriteLine(ping.Response.Meta.StatusEmoji);
    Console.WriteLine(ping.Response.Meta.Id);
}

Console.WriteLine();
Console.WriteLine("-- GetCategoriesAsync --");

var categories = await up.GetCategoriesAsync();
Console.WriteLine(categories.Success);
if (categories.Success)
{
    foreach (var category in categories.Response.Data)
    {
        Console.WriteLine($"{category.Id}: {category.Attributes.Name}");
    }
}

Console.WriteLine();
Console.WriteLine("-- GetTagsAsync --");

var tags = await up.GetTagsAsync();
Console.WriteLine(tags.Success);
if (tags.Success)
{
    foreach (var tag in tags.Response.Data)
    {
        Console.WriteLine(tag.Id);
    }
}

Console.WriteLine();
Console.WriteLine("-- GetTransactionsAsync --");

var transactions = await up.GetTransactionsAsync();

Console.WriteLine(transactions.Success);
if (transactions.Success)
{
    foreach (var transaction in transactions.Response.Data)
    {
        Console.WriteLine(
            $"{transaction.Id}: '{transaction.Relationships.Account.Data.Id}' {transaction.Attributes.Amount.Value} {transaction.Attributes.Message}");
    }
}

Console.WriteLine();
Console.WriteLine("-- GetTransactionsAsync.GetNextPageAsync --");

var nextPageOfTransactions = await transactions.Response.GetNextPageAsync();

Console.WriteLine(nextPageOfTransactions.Success);
if (nextPageOfTransactions.Success)
{
    foreach (var transaction in nextPageOfTransactions.Response.Data)
    {
        Console.WriteLine(
            $"{transaction.Id}: '{transaction.Relationships.Account.Data.Id}' {transaction.Attributes.Amount.Value} {transaction.Attributes.Message}");
    }
}


Console.ReadLine();