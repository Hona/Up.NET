using Up.NET.Api.Accounts;
using Up.NET.Api.Transactions;

namespace Up.NET;

internal static class EnumExtensions
{
    public static string ToApiString(this AccountType value) => value switch
    {
        AccountType.Saver => "SAVER",
        AccountType.Transactional => "TRANSACTIONAL",
        AccountType.HomeLoan => "HOME_LOAN",
        _ => value.ToString().ToUpperInvariant()
    };

    public static string ToApiString(this OwnershipType value) => value switch
    {
        OwnershipType.Individual => "INDIVIDUAL",
        OwnershipType.Joint => "JOINT",
        _ => value.ToString().ToUpperInvariant()
    };

    public static string ToApiString(this TransactionStatus value) => value switch
    {
        TransactionStatus.Held => "HELD",
        TransactionStatus.Settled => "SETTLED",
        _ => value.ToString().ToUpperInvariant()
    };
}
