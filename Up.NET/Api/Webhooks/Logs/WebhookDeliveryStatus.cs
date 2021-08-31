using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Up.NET.Api.Webhooks.Logs
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum WebhookDeliveryStatus
    {
        [EnumMember(Value = "DELIVERED")]
        Delivered,
        [EnumMember(Value = "UNDELIVERABLE")]
        Undeliverable,
        [EnumMember(Value = "BAD_RESPONSE_CODE")]
        BadResponseCode
    }
}