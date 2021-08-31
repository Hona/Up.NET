using Up.NET.Api.Accounts;

namespace Up.NET.Api.Transactions
{
    public class HoldInfo
    {
        public MoneyObject Amount { get; set; }
        public MoneyObject ForeignAmount { get; set; }
    }
}