using Up.NET.Models;

namespace Up.NET.Api.Accounts;

public class AccountResource
{
    [Obsolete("Always `accounts`")]
    public string Type { get; set; }
    public string Id { get; set; }
    public AccountAttributes Attributes { get; set; }
    public AccountRelationships Relationships { get; set; }
    public SelfLink Links { get; set; }
}