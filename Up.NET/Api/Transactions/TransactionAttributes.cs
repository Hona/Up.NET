using Up.NET.Api.Accounts;

namespace Up.NET.Api.Transactions;

public class TransactionAttributes
{
    public TransactionStatus Status { get; set; }
    public string RawText { get; set; }
    public string Description { get; set; }
    public string Message { get; set; }
    public HoldInfo HoldInfo { get; set; }
    public RoundUp RoundUp { get; set; }
    public Cashback Cashback { get; set; }
    public MoneyObject Amount { get; set; }
    public MoneyObject ForeignAmount { get; set; }
    public DateTime? SettledAt { get; set; }
    public DateTime CreatedAt { get; set; }
}