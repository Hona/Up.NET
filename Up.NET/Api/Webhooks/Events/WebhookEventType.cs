using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Up.NET.Api.Webhooks.Events;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum WebhookEventType
{
    [EnumMember(Value = "TRANSACTION_CREATED")]
    TransactionCreated,
    [EnumMember(Value = "TRANSACTION_SETTLED")]
    TransactionSettled,
    [EnumMember(Value = "TRANSACTION_DELETED")]
    TransactionDeleted,
    [EnumMember(Value = "PING")]
    Ping
}