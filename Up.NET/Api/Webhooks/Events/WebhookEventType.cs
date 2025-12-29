using System.Text.Json.Serialization;

namespace Up.NET.Api.Webhooks.Events;

[JsonConverter(typeof(JsonStringEnumConverter<WebhookEventType>))]
public enum WebhookEventType
{
    [JsonStringEnumMemberName("TRANSACTION_CREATED")]
    TransactionCreated,

    [JsonStringEnumMemberName("TRANSACTION_SETTLED")]
    TransactionSettled,

    [JsonStringEnumMemberName("TRANSACTION_DELETED")]
    TransactionDeleted,

    [JsonStringEnumMemberName("PING")]
    Ping
}
