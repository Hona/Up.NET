using System.Text.Json.Serialization;

namespace Up.NET.Api.Webhooks.Logs;

[JsonConverter(typeof(JsonStringEnumConverter<WebhookDeliveryStatus>))]
public enum WebhookDeliveryStatus
{
    [JsonStringEnumMemberName("DELIVERED")]
    Delivered,

    [JsonStringEnumMemberName("UNDELIVERABLE")]
    Undeliverable,

    [JsonStringEnumMemberName("BAD_RESPONSE_CODE")]
    BadResponseCode
}
