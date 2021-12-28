using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Up.NET.Api.Transactions;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum TransactionStatus
{
    [EnumMember(Value = "HELD")]
    Held,
    [EnumMember(Value = "SETTLED")]
    Settled
}