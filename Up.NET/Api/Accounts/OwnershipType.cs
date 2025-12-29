using System.Text.Json.Serialization;

namespace Up.NET.Api.Accounts;

[JsonConverter(typeof(JsonStringEnumConverter<OwnershipType>))]
public enum OwnershipType
{
    [JsonStringEnumMemberName("INDIVIDUAL")]
    Individual,

    [JsonStringEnumMemberName("JOINT")]
    Joint
}
