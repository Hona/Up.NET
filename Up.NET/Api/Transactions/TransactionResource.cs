using Up.NET.Models;

namespace Up.NET.Api.Transactions;

public class TransactionResource
{
    [Obsolete("Always `transactions`")] public string Type { get; set; }

    public string Id { get; set; }

    public TransactionAttributes Attributes { get; set; }
    public TransactionRelationships Relationships { get; set; }
    public SelfLink Links { get; set; }
}