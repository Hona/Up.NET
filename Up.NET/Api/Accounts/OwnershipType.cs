using System.Text.Json.Serialization;

namespace Up.NET.Api.Accounts;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum OwnershipType
{
    Individual,
    Joint
}