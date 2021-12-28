using Up.NET.Api.Accounts;

namespace Up.NET.Api.Transactions;

public class Cashback
{
    public string Description { get; set; }
    public MoneyObject Amount { get; set; }
}