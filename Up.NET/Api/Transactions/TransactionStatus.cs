using System.Text.Json.Serialization;

namespace Up.NET.Api.Transactions;

[JsonConverter(typeof(JsonStringEnumConverter<TransactionStatus>))]
public enum TransactionStatus
{
    [JsonStringEnumMemberName("HELD")]
    Held,

    [JsonStringEnumMemberName("SETTLED")]
    Settled
}
