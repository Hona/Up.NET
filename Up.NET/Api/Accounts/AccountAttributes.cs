namespace Up.NET.Api.Accounts;

public class AccountAttributes
{
    public string DisplayName { get; set; }
    public AccountType AccountType { get; set; }
    public OwnershipType OwnershipType { get; set; }
    public MoneyObject Balance { get; set; }
    public DateTime CreatedAt { get; set; }
}